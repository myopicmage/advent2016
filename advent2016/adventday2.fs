module adventday2

type move = Up | Down | Left | Right

let keypad = [|
    [|  0;   0; 5; 0; 0 |]
    [|  0;  10; 6; 2; 0 |]
    [| 13;  11; 7; 3; 1 |]
    [|  0;  12; 8; 4; 0 |]
    [|  0;   0; 9; 0; 0 |]
|]

let access (x, y) = keypad.[x].[y]

let parseDir = function
    | 'U' -> Up
    | 'D' -> Down
    | 'L' -> Left
    | 'R' -> Right
    | _ -> 
        printfn "Error!"
        Up

let makeLineList (input : string) =
    input.Split([|System.Environment.NewLine|], System.StringSplitOptions.None)
    |> Array.toList
    |> List.map (fun x -> x.ToCharArray() |> Array.map parseDir |> Array.toList)


let shift dir (x, y) =
    match dir with
    | Up -> 
        if y = 4 || (access (x, y + 1)) = 0 then
            x, y
        else
            x, y + 1
    | Down ->
        if y = 0 || (access (x, y - 1)) = 0 then
            x, y
        else
            x, y - 1
    | Left -> 
        if x = 0 || (access (x - 1, y)) = 0 then
            x, y
        else
            x - 1, y
    | Right -> 
        if x = 4 || (access (x + 1, y)) = 0 then
            x, y
        else
            x + 1, y

let rec parseLine line (x, y) =
    match line with
    | [] -> (x, y)
    | head :: tail -> parseLine tail (shift head (x, y))

let encode (x, y) = 
    match keypad.[x].[y] with
    | a when a < 10 -> a.ToString()
    | 10 -> "A"
    | 11 -> "B"
    | 12 -> "C"
    | 13 -> "D"
    | _ -> "error"

let genCode lines = 
    let parsed = makeLineList lines 

    let mutable codes = []
    let mutable prev = (0, 2)

    for line in parsed do
        prev <- parseLine line prev
        codes <- List.append codes [prev]

    codes |> List.map encode