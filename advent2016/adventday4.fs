﻿module adventday4

open utilities
open System.Text.RegularExpressions
open System.Linq

type room = { 
    name : string
    id : int
    checksum : string 
}

let genChecksum (name : string) =
    name.Replace("-", "").ToCharArray()
    |> Seq.groupBy (fun x -> x)
    |> Map.ofSeq
    |> Map.map (fun k v -> Seq.length v)
    |> (fun x -> x.OrderByDescending(fun y -> y.Value).ThenBy(fun y -> y.Key))
    |> Seq.take 5
    |> Seq.map (fun x -> x.Key.ToString())
    |> String.concat ""

let parse input =
    let m = Regex("([a-z\-]*)([0-9]*)(\[[a-z]*\])").Match(input)
    let groups = List.tail [ for x in m.Groups -> x.Value ] |> List.toArray
    let name = groups.[0]
    let id = groups.[1]
    let checksum = groups.[2].Replace("[", "").Replace("]", "")

    { name = name; id = (parseInt id); checksum = checksum }

let isReal room = 
    room.checksum = (genChecksum room.checksum)

let decrypt room =
    let shiftBy = room.id % 26

    let shift (letter : char) =
        match letter with
        | '-' | ' ' -> ' '
        | _ -> 
            let cur = (int letter) + shiftBy
            if cur > 122 then char (cur - 26) else char cur

    let decryptedName = room.name.ToCharArray() 
                        |> Array.toSeq
                        |> Seq.map shift 
                        |> Seq.map (fun x -> x.ToString())
                        |> String.concat ""

    { room with name = decryptedName }


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

let roomSum x = realRooms x |> realRoomSum