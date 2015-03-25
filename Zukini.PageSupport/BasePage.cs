using OpenQA.Selenium;
using System;

namespace Zukini.PageSupport
{
    public class BasePage
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="driver">An initialized instance of the WebDriver.</param>
        /// <exception cref="System.ArgumentNullException">driver</exception>
        public BasePage(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }

            _driver = driver;
        }

        /// <summary>
        /// Provides access to the WebDriver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        protected IWebDriver Driver
        {
            get { return _driver; }
        }

        /// <summary>
        /// Asserts that we are on the proper current page by searching for an element 
        /// as supplied in the findBy parameter.
        /// </summary>
        /// <param name="pageName">Name of the page to find (for messaging purposes.</param>
        /// <param name="findBy">A <c>By</c> parameter used to find an element that is specific to the page.</param>
        /// <exception cref="Zukini.PageSupport.CurrentPageException"></exception>
        protected void AssertCurrentPage(string pageName, By findBy)
        {
            try
            {
                Driver.FindElement(findBy);
            }
            catch(NoSuchElementException)
            {
                throw new CurrentPageException(String.Format("'{0}' is not the current page.", pageName));
            }
        }
    }
}

