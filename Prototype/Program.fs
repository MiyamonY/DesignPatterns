// Learn more about F# at http://fsharp.org
module Prototype

open System

[<AbstractClass>]
type Product() =
    abstract Use: string -> string
    abstract CreateClone: unit -> Product


type Manager<'a>() =
    let mutable showcase = Map([])

    member _.Register(name:string, product:Product) =
        showcase <- showcase.Add(name, product)

    member _.Create(name:string) =
        let p = showcase.TryFind(name).Value
        p.CreateClone()


type MessageBox(decorator:char) =
    inherit Product()

    override _.Use(msg:string) =
        let n = 4 + String.length msg
        let line = String.init n (fun _ -> string decorator) + "\n"
        line + (sprintf "%c %s %c\n" decorator msg decorator) + line

    override this.CreateClone() =
        this.MemberwiseClone() :?> Product


type UnderlinePen(underline: char) =
    inherit Product()

    override _.Use(msg:string) =
        let n = String.length msg
        let underline = String.init n (fun _ -> string underline) + "\n"
        msg + "\n" + underline

    override this.CreateClone() =
        this.MemberwiseClone() :?> Product


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
