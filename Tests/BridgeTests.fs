module BridgeTests

open System
open Xunit
open FsUnit
open Bridge

[<Fact>]
let ``Display displays message`` () =
    let display = Display(StringDisplayImpl("hello"))

    display.Show()
    |> should equal "hello"

[<Fact>]
let ``CountDisplay displays multiple messages`` () =
    let cdisplay = CountDisplay(StringDisplayImpl("hello"))

    cdisplay.ShowMultiple(3)
    |> should equal ["hello"; "hello"; "hello"]
