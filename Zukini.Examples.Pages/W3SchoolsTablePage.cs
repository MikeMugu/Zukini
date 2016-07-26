using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.Pages;

namespace Zukini.Examples.Pages
{
    public class W3SchoolsTablePage : BasePage
    {
        private const string PageTitle = "HTML table tag";

        public ElementScope BrowserReferenceTable { get { return Browser.FindCss(".browserref"); } }
        public ElementScope TopTextDiv { get { return Browser.FindCss(".toptext"); } }

        public W3SchoolsTablePage(BrowserSession browserSession)
            : base(browserSession)
        {
        }

        public void AssertCurrentPage()
        {
            AssertCurrentPage("W3Schools Table", Browser.Title == PageTitle);
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
