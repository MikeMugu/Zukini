using Coypu;
using Zukini.UI.Pages;

public class GoogleSearchPage : BasePage
{
    public GoogleSearchPage(BrowserSession browser)
        : base(browser)
    {
    }

    // Page Fields defined as simple getters. When accessed, each field will be found on screen, on demand
    public ElementScope SearchTextBox { get { return Browser.FindField("q"); } }
    public ElementScope SearchButton { get { return Browser.FindButton("Search"); } }

}
