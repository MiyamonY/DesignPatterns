module TemplateMethodTest

open System
open Xunit
open FsUnit
open TemplateMethod

[<Fact>]
let ``Display Char`` () =
    let cDisplay = CharDisplay('a') :> AbstractDisplay
    cDisplay.Display()
    cDisplay.Buffer |> should equal "<<aaaaa>>"

[<Fact>]
let ``Display String`` () =
    let sDisplay = StringDisplay("Hello, World") :> AbstractDisplay
    sDisplay.Display()
    let expected = "----------------
| Hello, World |
| Hello, World |
| Hello, World |
| Hello, World |
| Hello, World |
----------------
"
    sDisplay.Buffer |> should equal expected
