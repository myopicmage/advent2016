module utilities

open System

let splitOnNewLine (input : string) =
    input.Split([|Environment.NewLine|], StringSplitOptions.None) 

let parseInt input = Int32.Parse(input)

let (|Int|_|) str =
   match Int32.TryParse(str) with
   | (true, int) -> Some(int)
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