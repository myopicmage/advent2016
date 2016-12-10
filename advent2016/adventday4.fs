module adventday4

open utilities
open System.Text.RegularExpressions
open System.Linq

type room = { 
    name : string
    id : int
    checksum : string 
}

let genChecksum (name : string) =
    let a = name.Replace("-", "").ToCharArray()
            |> Seq.groupBy (fun x -> x)
            |> Map.ofSeq
            |> Map.map (fun k v -> Seq.length v)

    a.OrderByDescending((fun x -> x.Value)).ThenBy((fun x -> x.Key)) 
    |> Seq.take 5
    |> Seq.map (fun x -> x.Key.ToString())
    |> String.concat ""

let parse input =
    let regex = "([a-z\-]*)([0-9]*)(\[[a-z]*\])"
    let m = Regex(regex).Match(input)
    let groups = List.tail [ for x in m.Groups -> x.Value ] |> List.toArray
    let name = groups.[0]
    let id = groups.[1]
    let checksum = groups.[2].Replace("[", "").Replace("]", "")

    { name = name; id = (parseInt id); checksum = checksum }

let isReal room = room.checksum = (genChecksum room.checksum)

let decrypt room =
    let shiftBy = room.id % 26

    let shift (letter : char) =
        match letter with
        | '-' -> ' '
        | ' ' -> ' '
        | _ -> 
            let cur = (int letter) + shiftBy
            if cur > 122 then char (cur - 26) else char cur

    let decryptedName = room.name.ToCharArray() 
                        |> Array.toSeq
                        |> Seq.map shift 
                        |> Seq.map (fun x -> x.ToString())
                        |> String.concat ""

    { name = decryptedName; id = room.id; checksum = room.checksum }


let realRooms text =
    splitOnNewLine text 
    |> Array.toSeq
    |> Seq.map parse
    |> Seq.where isReal

let realRoomSum rooms =
    rooms
    |> Seq.map (fun x -> x.id)
    |> Seq.sum

let decryptedRooms text = 
    splitOnNewLine text
    |> Array.toSeq
    |> Seq.map parse
    |> Seq.map decrypt