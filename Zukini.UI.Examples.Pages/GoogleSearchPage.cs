using Coypu;
using Zukini.UI.Pages;

namespace Zukini.UI.Examples.Pages
{
    public class GoogleSearchPage : BasePage<GoogleSearchPage>
    {
        public GoogleSearchPage(BrowserSession browser) : base(browser) {}

        public ElementScope SearchTextBox => _.FindField("q");
        public ElementScope SearchButton  => _.FindButton("Search", Options.First);
        
        public override bool IsLoaded()
        {
            return SearchTextBox.Exists();
        }
    }
}
