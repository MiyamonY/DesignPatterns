module FactoryMethodTest

open System
open Xunit
open FsUnit
open FactoryMethod

[<Fact>]
let ``Create IDCards`` () =
    let factory = IDCardFactory()
    let card1 = factory.Create("aaa")
    let card2 = factory.Create("bbb")
    card1.Use() |> should equal "aaa used card"
    card2.Use() |> should equal "bbb used card"
