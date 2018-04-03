

let printTitles titles = 
    titles
    |> List.iter (fun t -> 
        let text, score = t
        printfn "(%d) %s" score text)
    printfn "---"


[<EntryPoint>]
let main argv = 
    Reddit.GetTopProgrammingTitles() |> printTitles
    Reddit.GetNewProgrammingTitles() |> printTitles

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
