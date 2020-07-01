// Learn more about F# at http://fsharp.org
module Builder

open System

[<AbstractClass>]
type Builder() =
    abstract MakeTitle: string -> unit
    abstract MakeSubTitle: string -> unit
    abstract MakeList: string list -> unit
    abstract Close: unit -> unit
    abstract Buffer:string with get

type Director(builder: Builder) =
    member _.Construct() =
        builder.MakeTitle("Greeting")
        builder.MakeSubTitle("By noon")
        builder.MakeList(["Good morning"; "Hello"])
        builder.MakeSubTitle("Evening")
        builder.MakeList(["Good evening";"Good night"])
        builder.Close()

type MdBuilder() =
    inherit Builder()

    let mutable buf = []

    override _.MakeTitle(title: string) =
        buf <- sprintf "# %s" title :: buf

    override _.MakeSubTitle(subtitle: string) =
        buf <- sprintf "## %s" subtitle :: buf

    override _.MakeList(list: string list) =
        buf <- (List.map (sprintf "- %s") list |> String.concat "\n") :: buf

    override _.Close() = ()

    override _.Buffer with get () =
        List.rev buf |> String.concat "\n"


type HtmlBuilder() =
    inherit Builder()
    let mutable buf = []

    override _.MakeTitle(title: string)=
        buf <- sprintf "<h1>%s</h1>" title:: buf

    override _.MakeSubTitle(subtitle: string) =
        buf <- sprintf "<h2>%s</h2>" subtitle ::buf

    override _.MakeList(list :string list) =
        buf <- (List.map (sprintf "\t<ul>%s</ul>") list |> String.concat "\n" |> sprintf "<li>\n%s\n</li>")::buf

    override _.Close() = ()

    override _.Buffer with get ()=
        List.rev buf |> String.concat "\n"


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
