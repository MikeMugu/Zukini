using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using Zukini.PageSupport;

namespace IContactPro.Test.Pages
{
    public class LoginPage : BasePage
    {
        private static string _forgotPasswordText = "Forgot your password?";

        #region Page Elements

        [FindsBy(How = How.Id, Using = "txtUserAccount")]
        public IWebElement UsernameTextBox;

        [FindsBy(How = How.Id, Using = "txtPassword")]
        public IWebElement PasswordTextBox;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"divSubmitVisible\"]/input")]
        public IWebElement SubmitButton;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="driver">An initialized instance of the WebDriver.</param>
        public LoginPage(IWebDriver driver) 
            : base(driver)
        {
        }

        /// <summary>
        /// Asserts that we are on the Login page.
        /// </summary>
        public void AssertCurrentPage()
        {
            AssertCurrentPage("Login", By.LinkText(_forgotPasswordText));
        }

        /// <summary>
        /// Performs login steps
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void Login(string username, string password)
        {
            UsernameTextBox.SendKeys(username);
            PasswordTextBox.SendKeys(password);
            SubmitButton.Click();
        }

    }
}
