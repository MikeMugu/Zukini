using Coypu;
using System;
using Zukini.Pages;

namespace IContactPro.Test.Pages
{
    public class LoginPage : BasePage
    {
        #region Page Elements

        public ElementScope Username { get { return Browser.FindField("Username"); } }
        public ElementScope Password { get { return Browser.FindField("Password"); } }
        public ElementScope SubmitButton { get { return Browser.FindXPath("//*[@id=\"divSubmitVisible\"]/input"); } }
        public ElementScope ForgotPasswordLink { get { return Browser.FindLink("Forgot your password?"); } }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="browserSession">An initialized instance of the BrowserSession.</param>
        public LoginPage(BrowserSession browserSession) 
            : base(browserSession)
        {
        }

        /// <summary>
        /// Asserts that we are on the Login page.
        /// </summary>
        public void AssertCurrentPage()
        {
            AssertCurrentPage("Login", ForgotPasswordLink.Exists());
        }

        /// <summary>
        /// Performs login steps
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void Login(string username, string password)
        {
            Username.FillInWith(username);
            Password.FillInWith(password);
            SubmitButton.Click();
        }

    }
}
