// Learn more about F# at http://fsharp.org
module AbstractFactory

open System

[<AbstractClass>]
type Item(caption:string) =
    abstract MakeHTML : unit -> string

[<AbstractClass>]
type Link(caption:string, url:string) =
    inherit Item(caption)

[<AbstractClass>]
type Tray(caption:string) =
    inherit Item(caption)
    let mutable tray = []

    member internal _.Tray with get () = tray |> List.rev

    member _.Add(item:Item) =
        tray <- item::tray

[<AbstractClass>]
type Page(tite:string, author:string) =
    let mutable tray = []

    member internal _.Tray with get() = tray |> List.rev

    member _.Add(item:Item) =
        tray <- item::tray

    abstract MakeHTML: unit -> string

    member this.Output() = this.MakeHTML()

[<AbstractClass>]
type Factory() =
    static member GetFactory(name:string) =
        let ty = Type.GetType(name)
        Activator.CreateInstance(ty) :?> Factory

    abstract member CreateLink: string * string  -> Link
    abstract member CreateTray: string -> Tray
    abstract member CreatePage: string * string -> Page

type ListLink(caption:string, url:string) =
    inherit Link(caption, url)

    override _.MakeHTML() =
        sprintf "<li><a href=\"%s\">%s</a></li>\n" url caption

type ListTray(caption:string) =
    inherit Tray(caption)

    override this.MakeHTML() =
        sprintf """<li>%s<ul>%s</ul></li>""" caption
        <| (List.map (fun (item:Item) -> item.MakeHTML()) this.Tray |> String.concat "\n")

type ListPage(title:string, author:string) =
    inherit Page(title, author)

    override this.MakeHTML() =
        sprintf """<html lang="ja">
  <head>
    <title>%s</title>
  </head>
  <body>
    <h1>%s</h1>
    <ul>
%s
    </ul>
    <hr>
    <address>%s</addrres>
  </body>
</html>""" title title (List.map (fun (item:Item) -> item.MakeHTML()) this.Tray |> String.concat "\n") author

type ListFactory() =
    inherit Factory()

    override _.CreateLink(caption:string, url:string) = ListLink(caption, url) :> Link

    override _.CreateTray(caption:string) = ListTray(caption) :> Tray

    override _.CreatePage(title:string, author:string) = ListPage(title, author) :> Page


type TableLink(caption:string, url:string) =
    inherit Link(caption, url)

    override _.MakeHTML() =
        sprintf """<td><a href="%s">%s</a></td>""" url caption

type TableTray(caption:string) =
    inherit Tray(caption)

    override this.MakeHTML() =
        sprintf """<td><table witdh="100%%" boarder="1"><tr><td><b>%s</b></td></tr><tr>%s</tr></table></td>""" caption <| (List.map (fun (item:Item) -> item.MakeHTML()) this.Tray |> String.concat "\n")


type TablePage(title: string, author:string) =
    inherit Page(title, author)

    override this.MakeHTML() =
        sprintf """<html lang="ja">
  <head>
    <title>%s</title>
  </head>
  <body>
    <h1>%s</h1>
    <table width="80%%" boarder="3">
%s
    </table>
    <hr>
    <address>%s</addrres>
  </body>
</html>""" title title (List.map (fun (item:Item) -> item.MakeHTML()) this.Tray |> String.concat "\n") author

type TableFactory() =
    inherit Factory()
    override _.CreateLink(caption:string, url:string) = TableLink(caption, url) :> Link
    override _.CreateTray(caption:string) = TableTray(caption) :> Tray
    override _.CreatePage(title:string, author:string) = TablePage(title, author) :> Page

[<EntryPoint>]
let main argv =
    // let factory = Factory.GetFactory("AbstractFactory+ListFactory")
    let factory = Factory.GetFactory("AbstractFactory+TableFactory")

    let asahi = factory.CreateLink("asahi", "http://www.asahi.com/")
    let yomiuri = factory.CreateLink("yomiuri", "http://www.yomiuri.co.jp/")
    let yahoo = factory.CreateLink("Yahoo!", "http://www.yahoo.com/")
    let yahooJa = factory.CreateLink("Yahoo! Japan", "http://www.yahoo.co.jp/")
    let excite = factory.CreateLink("excite", "http://www.exite.com")
    let google = factory.CreateLink("google", "http://www.google.co.jp")

    let trayNews = factory.CreateTray("News")
    trayNews.Add(asahi :> Item)
    trayNews.Add(yomiuri :> Item)

    let trayYahoo = factory.CreateTray("Yahoo!")
    trayYahoo.Add(yahoo :> Item)
    trayYahoo.Add(yahooJa :> Item)

    let traySearch = factory.CreateTray("Search Engine")
    traySearch.Add(trayYahoo :> Item)
    traySearch.Add(excite :> Item)
    traySearch.Add(google :> Item)

    let page = factory.CreatePage("LinkPage", "ymiaymoto")
    page.Add(trayNews :> Item)
    page.Add(traySearch :> Item)
    page.Output() |> printfn "%s"
    0
