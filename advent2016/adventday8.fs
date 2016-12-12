module adventday8

open utilities
open System.Text.RegularExpressions

let matchGroup regex str =
    let r = Regex(regex)
    
    match r.IsMatch(str) with
    | true -> 
        let l = List.tail [ for x in r.Match(str).Groups -> x.Value ] |> List.toArray 
        Some(int(l.[0]), int(l.[1]))
    | false -> None

let (|Rect|_|) str = matchGroup "rect ([0-9]+)x([0-9]+)" str

let (|RotateRow|_|) str = matchGroup "rotate row y=([0-9]+) by ([0-9]+)" str

let (|RotateCol|_|) str = matchGroup "rotate column x=([0-9]+) by ([0-9]+)" str

let printScreen screen =
    for y in screen do
        for x in y do
            if x then printf "#" else printf "."
        printfn ""

let rect (screen : bool [] []) sizeX sizeY =
    for y in 0 .. (sizeY - 1) do
        for x in 0 .. (sizeX - 1) do
            screen.[y].[x] <- true

    screen

let rotateRow (screen : bool [] []) y b = 
    let rec shift (s : bool [] []) times = 
        match times with
        | 0 -> s
        | _ -> 
            let mutable row = s.[y]
            let mutable newRow = Array.create row.Length false

            for i = 0 to (row.Length - 1) do
                let prev = if i - 1 >= 0 then i - 1 else row.Length - 1

                Array.set newRow i row.[prev]

            Array.set s y newRow
            shift s (times - 1)

    shift screen b
   
let rotateColumn (screen : bool [] []) x b = 
    let rec shift (s : bool [] []) times =
        match times with
        | 0 -> s
        | _ -> 
            let mutable column = [ for row in s -> row.[x] ]
            let mutable newCol = Array.create column.Length false

            for i = 0 to (column.Length - 1) do
                let prev = if i - 1 >= 0 then i - 1 else column.Length - 1

                Array.set newCol i column.[prev]

            for i = 0 to (column.Length - 1) do
                Array.set s.[i] x newCol.[i]

            shift s (times - 1)

    shift screen b

let parse screen line =
    match line with
    | Rect (x, y) -> rect screen x y
    | RotateRow (y, b) -> rotateRow screen y b
    | RotateCol (x, b) -> rotateColumn screen x b
    | _ -> 
        printfn "unknown command: %A" line
        screen

let litPixels text = 
    let directions = text |> splitOnNewLine |> Array.toList

    let screen = Array.init 6 (fun x -> Array.create 50 false)

    let processed = 
        List.fold (fun scr line -> parse scr line) screen directions 
        |> Array.map (fun x -> x |> Array.toList) 
        |> Array.toList

    printScreen processed

    processed 
    |> List.collect (fun x -> x) 
    |> List.where (fun x -> x) 
    |> List.length