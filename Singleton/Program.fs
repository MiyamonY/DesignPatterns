// Learn more about F# at http://fsharp.org
module Singleton

open System

type Singleton private () =
    static let singleton = Singleton()

    static member GetInstance() =
        singleton

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
