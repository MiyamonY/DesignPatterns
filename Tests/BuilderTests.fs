module BuilderTest

open System
open Xunit
open FsUnit
open Builder

[<Fact>]
let ``MdBuilder create md text`` () =
    let builder = MdBuilder()
    let d = Director(builder)
    d.Construct()
    builder.Buffer |> should equal "# Greeting
## By noon
- Good morning
- Hello
## Evening
- Good evening
- Good night"

[<Fact>]
let ``HtmlBulder create html text`` () =
    let builder = HtmlBuilder()
    let d = Director(builder)
    d.Construct()
    builder.Buffer |> should equal "<h1>Greeting</h1>
<h2>By noon</h2>
<li>
\t<ul>Good morning</ul>
\t<ul>Hello</ul>
</li>
<h2>Evening</h2>
<li>
\t<ul>Good evening</ul>
\t<ul>Good night</ul>
</li>"
