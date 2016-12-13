module adventday6

open utilities

let common (s : char seq) most =
    s
    |> Seq.map (fun x -> x.ToString())
    |> Seq.groupBy (fun x -> x)
    |> Map.ofSeq
    |> Map.map (fun k v -> Seq.length v)
    |> (fun x -> if most then 
                    x |> Seq.sortByDescending (fun y -> y.Value)
                 else
                    x |> Seq.sortBy (fun y -> y.Value ))
    |> Seq.head
    |> (fun x -> x.Key)

let correctError input most =
    splitOnNewLine input
    |> Array.toSeq
    |> Seq.map (fun x -> x.ToCharArray())
    |> Seq.collect (fun x -> x |> Seq.mapi (fun index element -> (index, element)))
    |> Seq.groupBy (fst)
    |> Seq.map (fun (index, element) -> element |> Seq.map snd)
    |> Seq.map (fun x -> common x most)
    |> String.concat ""

let correctErrors input =
    (sprintf "corrected code (most common): %A" (correctError input true)) + (sprintf "\ncorrected code (least common): %A" (correctError input false))