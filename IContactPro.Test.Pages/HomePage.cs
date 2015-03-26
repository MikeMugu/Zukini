using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.Pages;

namespace IContactPro.Test.Pages
{
    public class HomePage : BasePage
    {
        #region Page Elements

        public ElementScope MostRecentSendLabel { get { return Browser.FindId("MostRecentSend"); } }

        #endregion

        public HomePage(BrowserSession browserSession) 
            : base(browserSession)
        {
        }

        public void AssertCurrentPage()
        {
            AssertCurrentPage("Home", MostRecentSendLabel.Exists());
        }
    }
}
