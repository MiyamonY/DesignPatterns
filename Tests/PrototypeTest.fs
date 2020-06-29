module PrototypeTest

open System
open Xunit
open FsUnit
open Prototype

[<Fact>]
let ``Prototype clone instance`` () =
    let mAster = MessageBox('*')
    let upTilde = UnderlinePen('~')

    let manager = Manager()
    manager.Register("aster", mAster)
    manager.Register("tilde", upTilde)

    mAster.Use("Hello world") |> should equal (manager.Create("aster").Use("Hello world"))

    let upTilde2 = upTilde.CreateClone()
    upTilde.Use("Hello world") |> should equal (manager.Create("tilde").Use("Hello world"))
