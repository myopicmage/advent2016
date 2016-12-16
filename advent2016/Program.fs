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
open adventday10

[<EntryPoint>]
let main argv = 
    let mutable choice = ""

    let keepGoing c = if c = "quit" || c = "q" then false else true

    while keepGoing choice do
        printfn "Available: Days 1-9"
        printfn "Choose a day (quit to quit): "

        choice <- Console.ReadLine()

        match choice with
        | "1" -> day 1 navigate 
        | "2" -> day 2 genCode 
        | "3" -> day 3 howMany 
        | "4" -> dayWithWrite 4 roomSum decryptedRooms
        | "5" -> dayWithLabel 5 getCode "This one will take a while!"
        | "6" -> day 6 correctErrors
        | "7" -> day 7 both
        | "8" -> day 8 litPixels
        | "9" -> dayWithLabel 9 getLength "WARNING: this took 3+ hours to run on my machine"
        | "10" -> day 10 whichBot
        | "quit" | "q" -> printfn "Bye!"
        | _ -> printfn "Unknown choice :("

        printfn "Press any key to continue "
        Console.ReadLine() |> ignore
    0 // return an integer exit code