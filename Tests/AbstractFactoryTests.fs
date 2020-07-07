module AbstractFactoryTest

open System
open Xunit
open FsUnit
open AbstractFactory

[<Fact>]
let ``ListFactory's Assembly name is qualifiedName``() =
    typedefof<ListFactory>.AssemblyQualifiedName |> should startWith "AbstractFactory+ListFactory"

[<Fact>]
let ``Factory creates ListFactory``() =
    let factory = Factory.GetFactory("AbstractFactory+ListFactory")

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
    true |> should be True

[<Fact>]
let ``Factory creates TableFactory`` () =
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
    page.Output()

    true |> should be True
