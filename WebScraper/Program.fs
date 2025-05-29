namespace WebScraper

// https://www.compositional-it.com/news-blog/web-scraping-with-f/

module Main =

    [<EntryPoint>]
    let main _args =

        printf "Enter the product to scrape: "
        let productToSearch = System.Console.ReadLine()

        printf $"\nStarting web scrape for %s{productToSearch}...\n"
        do
            EbayUK.Instructor.LoadAndSaveToCsvFileA productToSearch "ebay.csv"
            |> Async.RunSynchronously

        printf $"Completed web scrape for {productToSearch}.\n"
        0
