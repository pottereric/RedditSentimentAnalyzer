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

let programmingSubreddits = 
    [
        "python"; //210k users
        "javascript"; //190k users
        "java"; //80.3k users
        "cpp"; //58.3k users
        "php"; //57.7k users
        "csharp"; //50.8k users
        "golang"; //38.8k users
        "ruby"; //38.0k users
        "rust"; //32.6k users
        "c_programming"; //32.0k users
        "swift"; //31.2k users
        "haskell"; //30.5k users
        "sql"; //21.1k users
        "scala"; //15.7k users
        "rstats"; //15.2k users
        "latex"; //13.7k users
        "matlab"; //13.7k users
        "clojure"; //13.2k users
        "lisp"; //12.5k users
        "perl"; //10.8k users
        "elixir"; //9.3k users
        "kotlin"; //8.0k users
        "erlang"; //6.1k users
        "elm"; //5.9k users
        "lua"; //5.6k users
        "objectivec"; //5.6k users
        "asm"; //4.6k users
        "scheme"; //4.5k users
        "fsharp"; //4.2k users
        "ocaml"; //4.0k users
        "julia"; //3.8k users
        "mathematica"; //3.7k users
        "visualbasic"; //3.6k users
        "racket"; //2.8k users
        "coffeescript"; //2.4k users
        "dartlang"; //2.3k users
        "d_language"; //2.1k users
        "gpgpu"; //2.0k users
        "shell"; //2.0k users
        "groovy"; //1.8k users
        "prolog"; //1.8k users
        "fortran"; //1.6k users
    ]

