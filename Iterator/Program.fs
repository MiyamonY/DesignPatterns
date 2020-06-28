// Learn more about F# at http://fsharp.org
module DesignPatterns

open System

type IIterator<'a> =
    abstract HasNext: unit -> bool
    abstract Next: unit -> 'a

type IAggregate<'a> =
    abstract Iterator: unit -> IIterator<'a>

type Book(name:String) =
    member _.Name with get() = name

type BookShelfIterator(bookShelf:BookShelf) =
    let mutable index = 0

    interface IIterator<Book> with
        member _.HasNext() = index < bookShelf.Length
        member _.Next() =
            let book = bookShelf.GetBookAt(index)
            index <- index + 1
            book

and BookShelf() =
    let mutable books = []

    interface IAggregate<Book> with
        member this.Iterator() = BookShelfIterator(this) :> IIterator<Book>

    member _.Length with get() = List.length books

    member this.Iterator() = (this :> IAggregate<Book>).Iterator()

    member _.Add(book:Book) =
        books <- List.append books [book]

    member _.GetBookAt(index:int) =
        List.item index books


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
