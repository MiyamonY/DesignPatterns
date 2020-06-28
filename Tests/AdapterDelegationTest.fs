module AdapterDelegationTest

open System
open Xunit
open FsUnit
open Adapter.Delegation

[<Fact>]
let ``Banner show weak`` () =
    let s = ShowBanner("Hello")
    s.ShowWeak() |> should equal "(Hello)"
    s.ShowStrong() |> should equal "*Hello*"
