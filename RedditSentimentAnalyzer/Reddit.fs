module Reddit

open FSharp.Data

type RedditProvider = JsonProvider<"SampleData\SampleRedditData.json">

let GetTitles subreddit sort = 
    let redditUrl = sprintf "%s/%s/%s.json" "https://www.reddit.com/r" subreddit sort
    let json = RedditProvider.Load(redditUrl)
    //let json = RedditProvider.GetSample()
    json.Data.Children 
    |> Seq.toList
    |> List.map (fun p -> (p.Data.Title, p.Data.Score))

let GetTopProgrammingTitles _ =
    GetTitles "programming" "top"

let GetHotProgrammingTitles _ =
    GetTitles "programming" "hot"

let GetNewProgrammingTitles _ =
    GetTitles "programming" "new"

