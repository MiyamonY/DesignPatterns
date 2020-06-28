module AdapterInheritanceTest

open System
open Xunit
open FsUnit
open Adapter.Inheritance

[<Fact>]
let ``Banner show weak`` () =
    let s = ShowBanner("Hello")
    s.ShowWeak() |> should equal "(Hello)"
    s.ShowStrong() |> should equal "*Hello*"
