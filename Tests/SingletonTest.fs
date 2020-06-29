module SingletonTest

open System
open Xunit
open FsUnit
open Singleton

let ``Singleton returns same instance`` () =
    let o1 = Singleton.GetInstance()
    let o2 = Singleton.GetInstance()

    o1 |> should equal o2
