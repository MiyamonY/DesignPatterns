// Learn more about F# at http://fsharp.org
module Bridge

open System

type DisplayImpl =
    abstract member RawOpen: unit -> unit
    abstract member RawPrint: unit -> string
    abstract member RawClose: unit -> unit

type Display(impl: DisplayImpl) =
    member _.Open() = impl.RawOpen ()
    member _.Print(s) = impl.RawPrint ()
    member _.Close() = impl.RawClose ()

    member _.Show (s) =
        impl.RawOpen()
        let s = impl.RawPrint()
        impl.RawClose()
        s

type CountDisplay(impl:DisplayImpl) =
    inherit Display(impl)
    member this.ShowMultiple(time: int) =
        this.Open()
        let ss = this.Show() |> List.replicate time
        this.Close()
        ss

type StringDisplayImpl(msg: string) =
    interface DisplayImpl with
        member this.RawOpen() = ()
        member this.RawPrint() = msg
        member this.RawClose() = ()

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
