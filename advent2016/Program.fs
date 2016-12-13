open System
open utilities
open adventday1
open adventday2
open adventday3
open adventday4
open adventday5
open adventday6
open adventday7
open adventday8
open adventday9

[<EntryPoint>]
let main argv = 
    let mutable choice = ""

    let keepGoing c = if c = "quit" || c = "q" then false else true

    while keepGoing choice do
        printfn "Available: Days 1-9"
        printfn "Choose a day (quit to quit): "

        choice <- Console.ReadLine()

        match choice with
        | "1" ->
            printfn "day 1"
            let def = @"D:\adventinput\day1.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input1 = tryReadFile (if loc <> "" then loc else def)

            match input1 with
            | Some(x) -> printfn "%A" (navigate x)
            | None -> printfn "uh oh, error :("
        | "2" -> 
            printfn "day 2"
            let def = @"D:\adventinput\day2.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> printfn "code: %A" (genCode x)
            | None -> printfn "could not read file"
        | "3" -> 
            printfn "day 3"
            let def = @"D:\adventinput\day3.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> printfn "how many? %A" (howMany x)
            | None -> printfn "could not read file"
        | "4" -> 
            printfn "day 4"
            let def = @"D:\adventinput\day4.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> 
                printfn "sum of real rooms: %A" (realRooms x |> realRoomSum)
                printfn "write to? "
                let o = Console.ReadLine()
                writeToFile (decryptedRooms x) (if o <> "" then o else @"D:\adventinput\day4out.txt")
            | None -> printfn "could not read file"
        | "5" ->
            printfn "day 5"
            printfn "This one will take a while!"
            let def = "ojvtpuvg"
            printfn "What's your code? (enter for default %A)" def
            let i = Console.ReadLine()
            let code = getCode (if i <> "" then i else def)
            printfn "code: %A" code
        | "6" -> 
            printfn "day 6"
            let def = @"D:\adventinput\day6.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> 
                printfn "corrected code (most common): %A" (correctError x true)
                printfn "corrected code (least common): %A" (correctError x false)
            | None -> printfn "could not read file"
        | "7" ->
            printfn "day 7"
            let def = @"D:\adventinput\day7.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> 
                printfn "tls: %A" (tls x)
                printfn "ssl: %A" (ssl x)
            | None -> printfn "could not read file"
        | "8" -> 
            printfn "day 8"
            let def = @"D:\adventinput\day8.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> 
                printfn "how many: %A" (litPixels x)
            | None -> printfn "could not read file"
        | "9" -> 
            printfn "day 9"
            printfn "WARNING: this took 3+ hours to run on my machine"
            let def = @"D:\adventinput\day9.txt"
            printfn "Input (default %A): " def
            let loc = Console.ReadLine()
            let input = tryReadFile (if loc <> "" then loc else def)

            match input with
            | Some(x) -> 
                printfn "decompressed length: %A" (getLength x)
            | None -> printfn "could not read file"
        | "quit" | "q" -> 
            printfn "Bye!"
        | _ -> 
            printfn "Unknown choice :("

        printfn "Press any key to continue "
        Console.ReadLine() |> ignore
    0 // return an integer exit code