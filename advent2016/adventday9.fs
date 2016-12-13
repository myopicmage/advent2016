module adventday9

open System.Text.RegularExpressions
open Microsoft.FSharp.Collections.Array.Parallel

let parseMarker marker =
    let r = Regex("\(([0-9]+)x([0-9]+)\)")
    let l = List.tail [ for x in r.Match(marker).Groups -> x.Value ] |> List.toArray 
    int(l.[0]), int(l.[1]), r.Match(marker).Length

let getLength string =
    let rec runMarker size times (input : string) = 
        let data = input.Substring(0, size)

        [|1 .. times|] |> Array.Parallel.map (fun _ -> parse data (bigint 0)) |> Array.sum, input.Substring(size)

    and parse (input : string) (output : bigint) =
        match input with
        | x when x.Length = 0 -> output
        | x ->
            match x.[0] with
            | '(' -> 
                let marker = x.Substring(0, (x.IndexOf(')') + 1))
                let size, times, length = parseMarker marker
                let head, tail = runMarker size times (x.Substring(length))
                parse tail (output + head)
            | _ -> 
                parse (x.Substring(1)) (output + bigint 1)

    parse string (bigint 0)