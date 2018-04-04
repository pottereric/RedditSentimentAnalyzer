

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

type RedditTitleSentiment = 
    { Votes: int
      Title: string
      Sentiment: float option
    }

let getSentimentAndTitlesForsubreddit subreddit sort =
    let titles = Reddit.GetTitles subreddit sort
    let inputs = titles |> convertTitlesToInputs

    let client = FognitiveServices.Text.Client.create Config.subscriptionKey Config.azureRegion
    let result = FognitiveServices.Text.Client.sentimentAnalysis client inputs

    let sentiments = result.Documents |> Seq.map(fun d -> d.Id, (d.Score |> Option.ofNullable)) |> List.ofSeq
    let combinedLists = List.zip3 titles inputs sentiments
    combinedLists |> List.map(fun item -> 
        let (text, score),(lang, id, title), (id2, sent) = item
        {Votes = score; Title = text; Sentiment = sent}
    )

let printNegativeTitles redditSentimentData =
    redditSentimentData 
    |> List.where(fun item -> item.Sentiment.Value < 0.5)
    |> List.sortBy (fun item -> item.Sentiment)
    |> List.iter (fun item -> printfn "(%d) - (%f) %s" item.Votes item.Sentiment.Value item.Title )

let printNegativeTitlesForLanguageSubreddits _ =
    Reddit.programmingSubreddits
    |> List.iter (fun subreddit ->
        printfn "== %s ==" subreddit
        getSentimentAndTitlesForsubreddit subreddit "top"
        |> printNegativeTitles
        printfn ""
    )

[<EntryPoint>]
let main argv = 
    //Reddit.GetTopProgrammingTitles() |> printTitles
    //Reddit.GetNewProgrammingTitles() |> printTitles

    //let redditSentimentData = getSentimentAndTitlesForsubreddit "programming" "top"
    //let redditSentimentData = getSentimentAndTitlesForsubreddit "programming" "new"
    //let redditSentimentData = getSentimentAndTitlesForsubreddit "programming" "new"

    //printNegativeTitles redditSentimentData

    printNegativeTitlesForLanguageSubreddits()


    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
