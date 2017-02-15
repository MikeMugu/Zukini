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
        /// <param name="until">The until.</param>
        public static void WaitUntil(this BrowserSession browserSession, Func<bool> until)
        {
            var doNothing = new LambdaBrowserAction(() => { }, new Options()); // options can't be null
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            browserSession.TryUntil(doNothing, predicate);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that defaults to a "do nothing" action for instances when the action already occurred,
        /// there is no need to repeat the action, and the caller is simply waiting for the side effect to occur (until).
        /// A bool Func is passed in instead of a PredicateQuery for the until parameter.
        /// </summary>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="until">The until.</param>
        /// <param name="descriptionForError">Provide a helpful description for troubleshooting timeouts</param>
        /// <exception cref="Exception"></exception>
        public static void WaitUntil(this BrowserSession browserSession, Func<bool> until, string descriptionForError)
        {
            try
            {
                WaitUntil(browserSession, until);
            }
            catch (Exception e)
            {
                throw new TimeoutException(descriptionForError, e);
            }
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that defaults to a "do nothing" action for instances when the action already occurred,
        /// there is no need to repeat the action, and the caller is simply waiting for the side effect to occur (until).
        /// A bool Func is passed in instead of a PredicateQuery for the until parameter.
        /// </summary>
        /// <remarks>
        /// This version of the TryUntil method allows the caller to override the options used for the action and until
        /// This version of the TryUntil method is not typically used, refer to:
        /// WaitUntil(this BrowserSession browserSession, Func<bool> until)
        /// </remarks>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="until">The until.</param>
        /// <param name="options">The options.</param>
        public static void WaitUntil(this BrowserSession browserSession, Func<bool> until, Options options)
        {
            var doNothing = new LambdaBrowserAction(() => { }, new Options()); // options can't be null
            var predicate = new LambdaPredicateQuery(until, new Options()); // options can't be null despite being optional
            browserSession.TryUntil(doNothing, predicate, options);
        }

        /// <summary>
        /// Wrapper for BrowserSession.TryUntil that defaults to a "do nothing" action for instances when the action already occurred,
        /// there is no need to repeat the action, and the caller is simply waiting for the side effect to occur (until).
        /// A bool Func is passed in instead of a PredicateQuery for the until parameter.
        /// </summary>
        /// <remarks>
        /// This version of the TryUntil method allows the caller to override the options used for the action and until
        /// This version of the TryUntil method is not typically used, refer to:
        /// WaitUntil(this BrowserSession browserSession, Func<bool> until)
        /// </remarks>
        /// <param name="browserSession">The browser session.</param>
        /// <param name="until">The until.</param>
        /// <param name="options">The options.</param>
        /// <param name="descriptionForError">Provide a helpful description for troubleshooting timeouts</param>
        public static void WaitUntil(this BrowserSession browserSession, Func<bool> until, Options options, string descriptionForError)
        {
            try
            {
                WaitUntil(browserSession, until, options);
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
            if (currentWait > sessionConfiguration.Timeout.TotalMilliseconds)
            {
                throw new TimeoutException($"Exceeded timeout '{sessionConfiguration.Timeout.TotalMilliseconds} ms' trying to navigate to page '{url}'. Browser still on page '{browserSession.Location.AbsoluteUri}'.");
            }
        }
    }
}
