using System;
using Coypu;

namespace CCC_Infrastructure.ViewFactory
{
    public abstract class BasePage<TPage> : BaseView<TPage> where TPage : BasePage<TPage>
    {
        private BrowserSession _browser;

        /// <summary>
        /// This is browser to operate.
        /// </summary>
        protected virtual BrowserSession Browser => _browser;

        public BasePage(BrowserSession browser) : base(browser)
        {
            _browser = browser;
        }
        
        protected virtual void AssertCurrentPage(string pageName, bool condition)
        {
            if (!condition)
                throw new Exception($"Expected page '{pageName}' does not pass the assert requirement.");
        }
    }
}
