module adventday10

open utilities
open System.Text.RegularExpressions

type bot = {
    id : int
    low : int
    high : int
}

type output = {
    id : int
    values : int list
}

type targettype = Bot | Output

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

let (|BotGive|_|) str = matchGroup5 "bot ([0-9]+) gives low to (output|bot) ([0-9]+) and high to (output|bot) ([0-9]+)" str

let giveVal v bot =
    if bot.low = -1 && bot.high = -1 then
        { bot with low = v }
    else if v > bot.low && v < bot.high then
        { bot with low = v; }
    else if v > bot.high then
        { bot with low = bot.high; high = v }
    else if v < bot.low then
        { bot with low = v; high = bot.low }
    else 
        bot

let tryFindOutputById id list : output option = List.tryFind (fun x -> x.id = id) list
let filterOutputById id list : output list = List.filter (fun x -> x.id <> id) list
let tryFindBotById id list : bot option = List.tryFind (fun x -> x.id = id) list
let filterBotById id list : bot list = List.filter (fun x -> x.id <> id) list

let addBot value id bots = bots @ [{ id = id; low = value; high = -1; }]

let giveToBot id value (bots : bot list) =
    let bot = tryFindBotById id bots

    match bot with
    | Some x -> (filterBotById id bots) @ [(giveVal value x)]
    | None -> bots @ [{ id = id; low = value; high = -1 }]

let giveToOutput id value (outputs : output list) =
    let output = tryFindOutputById id outputs

    match output with
    | Some x -> (filterOutputById id outputs) @ [{ x with values = x.values @ [value] }]
    | None -> outputs @ [{ id = id; values = [value] }]

let runCommand command (robots : bot list) (outs : output list) =
    let run cmd (bot : bot) bots outputs =
        let mutable b = bots
        let mutable o = outputs

        if cmd.botid = bot.id then
            if bot.low > -1 && bot.high > -1 then
                if cmd.lowtarget = Output then 
                    b <- (filterBotById cmd.botid bots) @ [{ bot with low = -1 }]
                    o <- (giveToOutput cmd.lowid bot.low outputs)
                else 
                    (filterBotById cmd.botid (giveToBot cmd.lowid bots)) @ [{ bot with low = -1 }], outputs

                if cmd.hightarget = Output then 
                    (filterBotById cmd.botid bots) @ [{ bot with high = -1 }], (giveToOutput cmd.highid bot.high outputs)
                else
                    (filterBotById cmd.botid (giveToBot cmd.highid bots)) @ [{ bot with high = -1 }], outputs
            else
                bots, outputs
        else
            bots, outputs

    List.map (fun x -> run command x robots outs) robots


//let addCommand (newCmd : instruction) (direction : instruction list) (robots : bot list) = 
//    

let parse line = 
    match line with
    | Value (value, bot) -> printfn "value %A goes to bot %A" value bot
    | BotGive (bot1, target1type, target1, target2type, target2) -> printfn "bot %A gives low to %A %A and high to %A %A" bot1 target1type target1 target2type target2 
    | _ -> printfn "unknown command! %A" line

let whichBot text =
    let parsed = text |> splitOnNewLine |> Array.map parse

    5