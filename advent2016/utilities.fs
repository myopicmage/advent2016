module utilities

open System

let splitOnNewLine (input : string) =
    input.Split([|Environment.NewLine|], StringSplitOptions.None) 

let parseInt input = Int32.Parse(input)

let (|Int|_|) str =
   match Int32.TryParse(str) with
   | (true, int) -> Some(int)
   | _ -> None

let tryReadFile file =
    try
        Some(IO.File.ReadAllText(file))
    with
        | _ -> None

let nicePrint list =
    for i in list do
        printfn "%A" i

let nicePrintWait list =
    for i in list do
        printfn "%A" i
        Console.ReadLine() |> ignore

let netpipe f x y = f(x, y) 

let writeToFile list fileName =
    let processed = 
        list 
        |> Seq.map (fun x -> sprintf "%A" x) 
        |> Seq.toArray

    IO.File.WriteAllLines(fileName, processed)

    printfn "Wrote all to %A" fileName

let strList (str : string) =
    str.ToCharArray()
    |> Array.map (fun x -> x.ToString())
    |> Array.toList

let day num f =
    printfn "Day %A" num
    let def = sprintf @"D:\adventinput\day%A.txt" num
    printfn "Input (default %A): " def
    let loc = Console.ReadLine()
    let input = tryReadFile (if loc <> "" then loc else def)

    match input with
    | Some(x) -> printfn "solution: %A" (f x)
    | None -> printfn "could not read file"

let dayWithLabel num f label =
    printfn "day %A" num
    printfn "%A" label
    let def = sprintf @"D:\adventinput\day%A.txt" num
    printfn "Input (default %A): " def
    let loc = Console.ReadLine()
    let input = tryReadFile (if loc <> "" then loc else def)

    match input with
    | Some(x) -> printfn "solution: %A" (f x)
    | None -> printfn "could not read file"
    
let dayWithWrite num f1 f2 =
    printfn "day %A" num
    let def = sprintf @"D:\adventinput\day%A.txt" num
    printfn "Input (default %A): " def
    let loc = Console.ReadLine()
    let input = tryReadFile (if loc <> "" then loc else def)

    match input with
    | Some(x) -> 
        printfn "sum of real rooms: %A" (f1 x)
        printfn "write to? "
        let o = Console.ReadLine()
        writeToFile (f2 x) (if o <> "" then o else @"D:\adventinput\day4out.txt")
    | None -> printfn "could not read file"