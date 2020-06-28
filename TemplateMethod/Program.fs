// Learn more about F# at http://fsharp.org
module TemplateMethod

open System

[<AbstractClass>]
type AbstractDisplay() =
    abstract Open: unit -> unit
    abstract Close: unit -> unit
    abstract Print: unit -> unit
    abstract Buffer : string with get

    member this.Display() =
        this.Open()
        List.iter (fun _ -> this.Print()) [1..5]
        this.Close()

type CharDisplay(c :char) =
    inherit AbstractDisplay()

    let mutable buffer = ""
    override _.Open() =
         buffer <- buffer + "<<"

    override _.Close() =
        buffer <- buffer + ">>"

    override _.Print() =
        buffer <- buffer + string c

    override _.Buffer = buffer

type StringDisplay(s:string) =
    inherit AbstractDisplay()

    let mutable buffer = ""

    member _.line() =
        String.replicate (4+ String.length s) "-"

    override this.Open() =
        buffer <- buffer + this.line() + "\n"

    override this.Close() =
        buffer <- buffer + this.line() + "\n"

    override _.Print() =
        buffer <- buffer + sprintf "| %s |\n" s

    override _.Buffer = buffer

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
