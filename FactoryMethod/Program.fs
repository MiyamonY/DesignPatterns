// Learn more about F# at http://fsharp.org
module FactoryMethod

open System

[<AbstractClass>]
type Product<'a>() =
    abstract Use : unit -> string

[<AbstractClass>]
type Factory<'a>() =
    abstract CreateProduct: string -> Product<'a>
    abstract RegisterProduct: Product<'a> -> unit

    member this.Create(owner:string) =
        let p = this.CreateProduct(owner)
        this.RegisterProduct(p)
        p

type IDCard(owner:string) =
    inherit Product<IDCard>()

    override _.Use() =
        sprintf "%s used card" owner

    member _.Owner with get() = owner

type IDCardFactory() =
    inherit Factory<IDCard>()

    let mutable owners = []

    override _.CreateProduct(name:string) =
        IDCard(name) :> Product<IDCard>

    override _.RegisterProduct(product:Product<IDCard>) =
        owners <- (product :?> IDCard).Owner::owners

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
