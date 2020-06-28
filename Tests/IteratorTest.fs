module Tests

open System
open Xunit
open FsUnit
open DesignPatterns

[<Fact>]
let ``Bookshelf has no book`` () =
    let bookShelf = BookShelf()
    let it = bookShelf.Iterator()
    it.HasNext() |> should equal false

[<Fact>]
let ``Bookshelf has some books`` () =
    let bookShelf = BookShelf()
    let bookName = ["Abc"; "Bcd"; "Cde"]
    bookName |> List.iter (fun name -> bookShelf.Add(Book(name)))

    let it = bookShelf.Iterator()
    bookName |> List.iter (fun name ->
                           it.HasNext() |> should equal true
                           it.Next().Name |>  should equal name)

    it.HasNext() |> should equal false

[<Fact>]
let ``Bookshelf can hav multipe iterator`` () =
    let bookShelf = BookShelf()
    let bookName = ["Abc"; "Bcd"; "Cde"]
    bookName |> List.iter (fun name -> bookShelf.Add(Book(name)))

    let it1 = bookShelf.Iterator()
    let it2 = bookShelf.Iterator()
    it1.Next() |> ignore; it1.Next() |> ignore
    it2.Next().Name |> should equal "Abc"
