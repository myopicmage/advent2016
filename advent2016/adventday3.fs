module adventday3

open System

let intSplit (line : string) =
    line.Trim()
        .Split(' ')
        |> Array.map (fun x -> x.Trim())
        |> Array.where (fun x -> not (String.IsNullOrWhiteSpace(x)))
        |> Array.map (fun x -> System.Int32.Parse(x))

let isTriangle (triplet : int array) = 
    (triplet.[0] + triplet.[1]) > triplet.[2] 
    && (triplet.[1] + triplet.[2] > triplet.[0]) 
    && (triplet.[2] + triplet.[0] > triplet.[1])

let howMany (triangles : string) =
    let input = 
        triangles.Split([|System.Environment.NewLine|], System.StringSplitOptions.None) 
        |> Array.map intSplit

    let col1 = input |> Array.map (fun x -> x.[0])
    let col2 = input |> Array.map (fun x -> x.[1])
    let col3 = input |> Array.map (fun x -> x.[2])

    Array.concat [| col1; col2; col3 |]
    |> Array.toSeq
    |> Seq.chunkBySize 3
    |> Seq.where isTriangle
    |> Seq.length