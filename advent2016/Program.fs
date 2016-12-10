open System
open utilities
open adventday1
open adventday2
open adventday3
open adventday4
open adventday5

[<EntryPoint>]
let main argv = 
//    let input1 = IO.File.ReadAllText(@"D:\adventinput\day1.txt")
//    let input2 = IO.File.ReadAllText(@"D:\adventinput\day2.txt")
//    let input3 = IO.File.ReadAllText(@"D:\adventinput\day3.txt")
//    let input4 = IO.File.ReadAllText(@"D:\adventinput\day4.txt")
//
//    printfn "day 1"
//    let testDistance = navigate input1
//    printfn "%A" testDistance
//
//    printfn "day 2"
//    let code = genCode input2
//    printfn "code: %A" code
//
//    printfn "day 3"
//    let triangles = howMany input3
//    printfn "how many? %A" triangles
//
//    printfn "day 4"
//    let realSum = realRooms input4 |> realRoomSum
//    printfn "sum of real rooms: %A" realSum
//
//    writeToFile (decryptedRooms input4) @"D:\adventinput\day4out.txt"

    printfn "day 5"
    let code = getCode "ojvtpuvg"
    printfn "code: %A" code

    Console.ReadLine() |> ignore
    0 // return an integer exit code
