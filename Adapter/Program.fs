// Learn more about F# at http://fsharp.org
namespace Adapter

open System

module Inheritance =
    type IShow =
        abstract ShowWeak: unit -> string
        abstract ShowStrong: unit -> string

    type Banner(input:string) =
        member _.showWithParen() = sprintf "(%s)" input
        member _.showWithAster() = sprintf "*%s*" input

    type ShowBanner(input:string) =
        inherit Banner(input)

        interface IShow with
            member this.ShowWeak() = this.showWithParen()
            member this.ShowStrong() = this.showWithAster()

        member this.ShowWeak() = (this :> IShow).ShowWeak()
        member this.ShowStrong() = (this :> IShow).ShowStrong()

module Delegation =
    [<AbstractClass>]
    type Show() =
        abstract member ShowWeak: unit -> string
        abstract member ShowStrong: unit -> string

    type Banner(input:string) =
        member _.showWithParen() = sprintf "(%s)" input
        member _.showWithAster() = sprintf "*%s*" input

    type ShowBanner(input:string) =
        inherit Show()

        let banner = Banner(input)

        override _.ShowWeak() = banner.showWithParen()
        override _.ShowStrong() = banner.showWithAster()
