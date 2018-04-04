

let printTitles titles = 
    titles
    |> List.iter (fun t -> 
        let text, score = t
        printfn "(%d) %s" score text)
    printfn "---"

let convertTitlesToInputs titles =
    titles |> List.mapi (fun i t -> 
        let text, score = t
        "en", i.ToString(), text)

let getSentimentAndTitlesForsubreddit subreddit sort =
    let titles = Reddit.GetTitles subreddit sort
    let inputs = titles |> convertTitlesToInputs

    let client = FognitiveServices.Text.Client.create Config.subscriptionKey Config.azureRegion
    let result = FognitiveServices.Text.Client.sentimentAnalysis client inputs

    let sentiments = result.Documents |> Seq.map(fun d -> d.Id, d.Score) |> List.ofSeq
    List.zip3 titles inputs sentiments


[<EntryPoint>]
let main argv = 
    //Reddit.GetTopProgrammingTitles() |> printTitles
    //Reddit.GetNewProgrammingTitles() |> printTitles

    //let redditSentimentData = getSentimentAndTitlesForsubreddit "programming" "top"
    let redditSentimentData = getSentimentAndTitlesForsubreddit "programming" "new"
    redditSentimentData |> List.iter (fun item ->
        let (text, score),(lang, id, title), (id2, sent) = item
        //printfn "(%d) - (%f) [%s,%s] %s : %s" score sent.Value id id2 text title
        printfn "(%d) - (%f) %s" score sent.Value title
    )



    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
