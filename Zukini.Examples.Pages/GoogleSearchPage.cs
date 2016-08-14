using Coypu;
using Zukini.Pages;

namespace Zukini.Examples.Pages
{
    public class GoogleSearchPage : BasePage
    {
        public GoogleSearchPage(BrowserSession browser)
            : base(browser)
        { 
        }

        public ElementScope SearchTextBox { get { return Browser.FindField("q"); } }
        public ElementScope SearchButton { get { return Browser.FindButton("Search"); } }
    }
}
