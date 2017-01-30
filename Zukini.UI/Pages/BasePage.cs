using Coypu;
using System;

namespace Zukini.UI.Pages
{
    public class BasePage
    {
        private readonly BrowserSession _browser;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="driver">An initialized instance of the WebDriver.</param>
        /// <exception cref="System.ArgumentNullException">driver</exception>
        public BasePage(BrowserSession browser)
        {
            if (browser == null)
            {
                throw new ArgumentNullException("driver");
            }

            _browser = browser;
        }

        /// <summary>
        /// Provides access to the BrowserSession (as provided by Coypu).
        /// </summary>
        protected BrowserSession Browser
        {
            get { return _browser; }
        }

        /// <summary>
        /// Asserts that we are on the proper current page by searching for an element 
        /// as supplied in the findBy parameter.
        /// </summary>
        /// <param name="pageName">Name of the page to find (for messaging purposes.</param>
        /// <param name="condition"><c>true</c> if the page exists, or <c>false</c> if the page assertion fails.</param>
        /// <exception cref="Zukini.PageSupport.CurrentPageException"></exception>
        protected void AssertCurrentPage(string pageName, bool condition)
        {
            if (!condition)
            {
                throw new CurrentPageException(String.Format("'{0}' is not the current page. Actual page was '{1}'", pageName, Browser.Location));
            }
        }
    }
}

