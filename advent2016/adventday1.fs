module adventday1

open System

type point = {
    x : int
    y : int
}

type direction = L | R

type instruction = {
    direction : direction
    distance : int
}

let parse x = Int32.Parse(x)

let makeDir = function
    | 'L' -> L
    | 'R' -> R
    | _ -> L

let makelist (input : string) =
    input
        .Replace(" ", "")
        .Split([|','|])
        |> Array.toList
        |> List.map (fun x -> { direction = (makeDir x.[0]); distance = parse(x.Substring(1)) })


let navigate input =
    let instructions = makelist input
    let mutable dir = (0, 1)
    let mutable x = 0
    let mutable y = 0
    let mutable found = false
    let mutable visited = []

    let changeDir = function
        | L -> 
            let a, b = dir
            (-b, a)
        | R -> 
            let a, b = dir
            (b, -a)
        
    for i in instructions do
        dir <- changeDir i.direction

        for _ in 1 .. i.distance do 
            let a, b = dir
            x <- x + a
            y <- y + b

            if not found && List.exists (fun point -> point.x = x && point.y = y) visited then
                printfn "visited twice: x: %A y: %A" x y
                printfn "Blocks away: %A" (Math.Abs(x) + Math.Abs(y))
                found <- true
            else
                visited <- List.append visited [{ x = x; y = y }]

    Math.Abs(x) + Math.Abs(y)