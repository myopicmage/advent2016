module adventday7

open utilities
open System.Text.RegularExpressions

let split (ip : string) =
    let r = "(\[[^\]]*\])|([a-z]*)"
    let matches = Regex(r).Matches(ip)

    let p (str : string) =
        if str.Contains("[") then
            (str.Replace("[", "").Replace("]", ""), true)
        else
            (str, false)

    [ for x in matches do if x.Value <> "" then yield p x.Value ]

let tlsAllowed (ip : string) =
    let pieces = split ip
    
    let rec valid (str : string list) =
        match str with
        | head :: tail when tail.Length >= 3 ->
            let two = List.head tail
            let three = tail.Item(1)
            let four = tail.Item(2)
            let a = head + two
            let b = four + three

            if a = b && head <> two then true else valid tail 
        | _ -> false

    let inside = 
        pieces 
        |> List.where (fun (_, y) -> y) 
        |> List.map (fun (x, _) -> valid (strList x))

    let outside = 
        pieces 
        |> List.where (fun (_, y) -> not y) 
        |> List.map (fun (x, _) -> valid (strList x))

    not (inside |> List.exists (fun x -> x = true)) && outside |> List.exists (fun x -> x = true)

let sslAllowed (ip : string) =
    let pieces = split ip

    let isTriplet (str : string) =
        let one = str.Chars(0)
        let two = str.Chars(1)
        let three = str.Chars(2)

        one = three && one <> two

    let inside = pieces |> List.where (fun (_, y) -> y) |> List.map (fun (x, _) -> x)
    let outside = pieces |> List.where (fun (_, y) -> not y) |> List.map (fun (x, _) -> x)

    let rec valid (str : string list) (inner : string list) = 
        match str with
        | head :: tail when tail.Length >= 2 ->
            let two = tail.Item(0)
            let three = tail.Item(1)
            let triplet = head + two + three

            if isTriplet triplet then
                let reversed = two + head + two

                if inner |> List.exists (fun x -> x.Contains(reversed)) then
                    true
                else
                    valid tail inner
            else
                valid tail inner
        | _ -> false

    outside |> List.map (fun x -> valid (strList x) inside) |> List.exists (fun x -> x)

let tls ips =
    ips
    |> splitOnNewLine
    |> Array.toSeq
    |> Seq.where tlsAllowed
    |> Seq.length

let ssl ips =
    ips
    |> splitOnNewLine
    |> Array.toSeq
    |> Seq.where sslAllowed
    |> Seq.length