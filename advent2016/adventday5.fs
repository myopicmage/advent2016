module adventday5

open utilities
open System.Security.Cryptography
open System.Text

let md5 = MD5.Create()

let hash (str : string) =
    str
    |> Encoding.UTF8.GetBytes
    |> md5.ComputeHash
    |> Seq.map (fun x -> x.ToString("x2"))
    |> String.concat ""

let getCode input =
    let mutable code = Array.init 8 (fun _ -> "-1")
    let mutable cur = 0

    while code |> Array.contains "-1" do
        let test = hash (input + cur.ToString())

        if test.Substring(0, 5) = "00000" then
            match test.Chars(5).ToString() with
            | Int i -> if i < 8 && code.[i] = "-1" then code.[i] <- test.Chars(6).ToString()
            | _ -> ()

        cur <- cur + 1

    code |> String.concat ""