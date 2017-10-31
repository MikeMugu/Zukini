using Coypu;
using Coypu.Actions;
using Coypu.Queries;
using System;
using System.Threading;

namespace Zukini.UI
{
    public static class BrowserSessionExtension
    {
        /// <summary>
        /// Scrolls the element into view.
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="element">The element.</param>
        public static void ScrollIntoView(this BrowserSession browserSession, ElementScope element)
        {
            browserSession.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// Uses javascript to click on an element. Unlike Selenium or Coypu clicks, this click
        /// does not have visibility constraits that would cause it to "click another/overlaying element"
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="element">The element to receive the click.</param>
        public static void JavaScriptClick(this BrowserSession browserSession, ElementScope element)
        {
            browserSession.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Disables the animations on the document.body by appending notransition as css style to the document head
        /// and appends the notransition class name to the document.body class.
        /// Persists until the page is refreshed or navigated to.
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        public static void DisableAnimations(this BrowserSession browserSession)
        {
            var style = ".notransition * { -webkit - transition: none !important; -moz - transition: none !important; -o - transition: none !important; -ms - transition: none !important; transition: none !important; }";
            var javascript = $"var style = document.createElement('style');style.innerHTML='{style}';document.head.append(style);document.body.className += ' notransition';";
            browserSession.ExecuteScript(javascript);
        }

        /// <summary>
        /// Gets local browser timeshift from UTC time.
        /// </summary>
        /// <param name="browserSession"></param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan GetBrowserTimezoneOffset(this BrowserSession browserSession)
        {
            int shift = Convert.ToInt16(browserSession.ExecuteScript("return new Date().getTimezoneOffset()"));
            return TimeSpan.FromMinutes(shift);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that allows the use of a bool Func until instead of a PredicateQuery
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="action">The action, typically a lambda expression like () => { ... }.</param>
        /// <param name="until">The until, typically a lambda expression like () => { ... }.</param>
        public static void TryUntil(this BrowserSession browserSession, Action action, Func<bool> until)
        {
            var browserAction = new LambdaBrowserAction(action, new Options()); // options can't be null
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            browserSession.TryUntil(browserAction, predicate);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that allows the use of a bool Func until instead of a PredicateQuery
        /// </summary>
        /// <remarks>
        /// This version of the TryUntil method allows the caller to override the options used for the action
        /// This version of the TryUntil method is not typically used, refer to:
        /// TryUntil(this BrowserSession browserSession, Action action, Func<bool> until)
        /// </remarks>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="action">The action, typically a lambda expression like () => { ... }.</param>
        /// <param name="actionOptions">The action options</param>
        /// <param name="until">The until, typically a lambda expression like () => { ... }.</param>
        public static void TryUntil(this BrowserSession browserSession, Action action, Options actionOptions, Func<bool> until)
        {
            var browserAction = new LambdaBrowserAction(action, actionOptions);
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            browserSession.TryUntil(browserAction, predicate);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that allows the use of a bool Func until instead of a PredicateQuery
        /// </summary>
        /// <remarks>
        /// This version of the TryUntil method allows the caller to override the options used for the action and until
        /// This version of the TryUntil method is not typically used, refer to:
        /// TryUntil(this BrowserSession browserSession, Action action, Func<bool> until)
        /// </remarks>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="action">The action, typically a lambda expression like () => { ... }.</param>
        /// <param name="actionOptions">The action options</param>
        /// <param name="until">The until, typically a lambda expression like () => { ... }.</param>
        /// /// <param name="tryUntilOptions">The try until options</param>
        public static void TryUntil(this BrowserSession browserSession, Action action, Options actionOptions, Func<bool> until, Options tryUntilOptions)
        {
            var browserAction = new LambdaBrowserAction(action, actionOptions);
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            browserSession.TryUntil(browserAction, predicate, tryUntilOptions);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that defaults to a "do nothing" action for instances when the action already occurred,
        /// there is no need to repeat the action, and the caller is simply waiting for the side effect to occur (until).
        /// A bool Func is passed in instead of a PredicateQuery for the until parameter.
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="until">The until condition to wait for as a function that returns a bool.</param>
        /// <param name="descriptionForError">Provide a helpful description for troubleshooting timeouts</param>
        /// <param name="options">The options used for the call to Coypu.TryUntil, optional.</param>
        public static void WaitUntil(this BrowserSession browserSession, Func<bool> until, string descriptionForError, Options options = null)
        {
            var doNothing = new LambdaBrowserAction(() => { }, new Options()); // options can't be null
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            try
            {
                browserSession.TryUntil(doNothing, predicate, options ?? new Options());
            }
            catch (Exception e)
            {
                throw new TimeoutException(descriptionForError, e);
            }
        }

        /// <summary>
        /// Calls BrowserSession.Visit and waits until BrowserSession.Location changes to navigation URL.
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="sessionConfiguration">The session configuration.</param>
        /// <param name="url">The URL to visit.</param>
        /// <exception cref="TimeoutException"></exception>
        public static void WaitForNavigation(this BrowserSession browserSession, SessionConfiguration sessionConfiguration, string url)
        {
            double currentWait = 0;
            browserSession.Visit(url);
            while (!browserSession.Location.AbsoluteUri.StartsWith(url) && currentWait < sessionConfiguration.Timeout.TotalMilliseconds)
            {
                Thread.Sleep(Convert.ToInt32(sessionConfiguration.RetryInterval.TotalMilliseconds));
                currentWait += sessionConfiguration.RetryInterval.TotalMilliseconds;
            }
            if (!browserSession.Location.AbsoluteUri.StartsWith(url))
            {
                throw new TimeoutException($"Exceeded timeout '{sessionConfiguration.Timeout.TotalMilliseconds} ms' trying to navigate to page '{url}'. Browser still on page '{browserSession.Location.AbsoluteUri}'.");
            }
        }
    }
}
