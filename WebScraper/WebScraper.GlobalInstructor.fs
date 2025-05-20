namespace WebScraper

open FSharp.Data

[<RequireQualifiedAccess>]
module GlobalInstructor =
    
    type ProductSearchString = ProductSearchString of string
        with
            member private this.innerString =
                match this with
                | ProductSearchString str -> str
            
            member this.BuildSearchString (concatChar : string) =
                this.innerString.Split(' ')
                |> Array.map (fun item -> item.Trim())
                |> Array.filter (fun item -> item.Length > 0)
                |> String.concat concatChar
    
    
    type Item = {
        Description: string
        Price      : string
        LinkToItem : string
    }
    
    type Instructor = {
        WebsiteURL      : string
        ResultListClass : string
        DescriptionClass: string
        PriceClass      : string
        LinkToItemClass : string
        ProductConcatCharacter : string
    }
        with
            member private this.LoadA (product : ProductSearchString) =
                HtmlDocument.AsyncLoad(this.WebsiteURL + "/" + product.BuildSearchString(this.ProductConcatCharacter))
                
            member private this.ParseLoadedDocumentAL (product : ProductSearchString) =
                async {
                    let! response = this.LoadA product
                    return
                        response.CssSelect(this.ResultListClass)
                        |> List.map (fun item ->
                            {
                                Description = item.CssSelect(this.DescriptionClass) |> List.head |> _.InnerText()
                                Price = item.CssSelect(this.PriceClass) |> List.head |> _.InnerText()
                                LinkToItem = item.CssSelect(this.LinkToItemClass) |> List.head |> _.Attribute("href").Value()
                            }
                        )
                }
                
            member private this.saveToCSV_A (fileName : string) (items : Item list) =
                
                let headers = "Description\tPrice\tLinkToItem"
                
                let itemDateCsv = 
                    items
                    |> List.map (fun item -> $"{item.Description}\t{item.Price}\t{item.LinkToItem}")
                    |> String.concat "\n"
                    
                let csv = $"{headers}\n{itemDateCsv}"
                
                let sourceDirectory = System.IO.Directory.GetCurrentDirectory()
                let filePath = System.IO.Path.Combine(sourceDirectory, fileName)
                System.IO.File.WriteAllTextAsync (filePath, csv)
                |> Async.AwaitTask
                
            member this.LoadAndSaveToCsvFileA (productSearchString : string) (fileName : string) =
                async {
                    let! items = this.ParseLoadedDocumentAL (ProductSearchString productSearchString)
                    do! this.saveToCSV_A fileName items
                }
                

