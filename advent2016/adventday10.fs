module adventday10

open utilities
open System.Text.RegularExpressions

type targettype = Bot | Output

type bot = {
    id : int
    values : int list
    t_type : targettype
}

type instruction = {
    botid : int
    lowtarget : targettype
    lowid : int
    hightarget : targettype
    highid : int
}

let matchGroup2 regex str =
    let r = Regex(regex)
    
    match r.IsMatch(str) with
    | true -> 
        let l = List.tail [ for x in r.Match(str).Groups -> x.Value ] |> List.toArray 
        Some(int(l.[0]), int(l.[1]))
    | false -> None

let matchGroup5 regex str =
    let r = Regex(regex)
    
    match r.IsMatch(str) with
    | true -> 
        let l = List.tail [ for x in r.Match(str).Groups -> x.Value ] |> List.toArray 
        Some(int l.[0], l.[1], int l.[2], l.[3], int l.[4])
    | false -> None


let (|Value|_|) str = matchGroup2 "value ([0-9]+) goes to bot ([0-9]+)" str

let (|Give|_|) str = matchGroup5 "bot ([0-9]+) gives low to (output|bot) ([0-9]+) and high to (output|bot) ([0-9]+)" str

let tryFindById id t_type list = List.tryFind (fun x -> x.id = id && x.t_type = t_type) list
let filterById id t_type list = List.filter (fun x -> x.id <> id && x.t_type <> t_type) list

let giveVal id value t_type bots =
    match tryFindById id t_type bots with
    | Some x -> { x with values = value :: x.values } :: bots
    | None -> { id = id; values = [value]; t_type = t_type } :: bots

let runCommand cmd bots commands =
    let bot = tryFindById cmd.botid Bot bots

    match bot with
    | Some b ->
        if b.values.Length = 2 then
            let newbots = 
                bots
                |> giveVal cmd.highid (List.max b.values) cmd.hightarget
                |> giveVal cmd.lowid (List.min b.values) cmd.lowtarget
                |> filterById b.id Bot

            { b with values = [] } :: newbots, commands
        else
            bots, cmd :: commands
    | None -> bots, cmd :: commands

let parse line bots directions = 
    match line with
    | Value (value, bot) -> 
        giveVal bot value bots |> ignore
    | Give (bot1, target1type, target1, target2type, target2) -> 
        printfn "bot %A gives low to %A %A and high to %A %A" bot1 target1type target1 target2type target2 
    | _ -> 
        printfn "unknown command! %A" line

let whichBot text =
    let parsed = text |> splitOnNewLine |> Array.map parse

    5