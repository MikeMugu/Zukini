using Coypu;
using System;
using System.Linq;
using Zukini.UI.Pages;

namespace Zukini.UI.Examples.Pages
{
    public class W3SchoolsTablePage : BasePage<W3SchoolsTablePage>
    {
        private const string PageTitle = "HTML table tag";

        public ElementScope BrowserReferenceTable => _.FindCss(".browserref");
        public ElementScope TopTextDiv => _.FindCss(".toptext");
        private readonly IViewFactory _vF;

        public W3SchoolsTablePage(BrowserSession browserSession, IViewFactory vF) : base(browserSession)
        {
            _vF = vF;
        }

        public W3SchoolsTablePage AssertCurrentPage()
        {
            return AssertCurrentPage("W3Schools Table", Browser.Title == PageTitle);
        }

        public override bool IsLoaded()
        {
            return Browser.Title == PageTitle;
        }

        public bool IsBrowserSupported(string browserName)
        {
            BrowserName browser;
            if (!Enum.TryParse<BrowserName>(browserName, out browser))
            {
                throw new Exception(String.Format("Invalid browser name {0} supplied.", browserName));
            }
            
            // Get second row
            var row = BrowserReferenceTable.FindAllRows().ElementAt(1);

            // Get the cell we want using the cell index
            ElementScope cell = row.FindAllCells().ElementAt(((int)browser + 1));

            // If cell text == yes, the browser is supported
            return cell.Text.Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
