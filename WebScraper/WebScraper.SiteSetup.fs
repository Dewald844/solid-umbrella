namespace WebScraper

module EbayUK =
    
    let Instructor =
        {
            GlobalInstructor.ResultListClass  = ".s-item"
            GlobalInstructor.DescriptionClass = ".s-item__title"
            GlobalInstructor.PriceClass       = ".s-item__price"
            GlobalInstructor.LinkToItemClass  = ".s-item__link"
            GlobalInstructor.WebsiteURL       = "https://www.ebay.co.uk/sch/i.html?_nkw="
            GlobalInstructor.ProductConcatCharacter = "+"
        }
        

