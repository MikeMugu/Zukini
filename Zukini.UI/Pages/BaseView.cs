using System;
using Coypu;
using Coypu.Actions;
using Coypu.Queries;

namespace Zukini.UI.Pages
{
    /// <summary>
    /// Contains generic stuff for pages and page components.
    /// </summary>
    /// <typeparam name="TView">Type of view</typeparam>
    public abstract class BaseView<TView> : IView<TView> where TView : BaseView<TView>
    {
        private readonly DriverScope _browserScope;
        private static readonly Options _NONE = new Options();
        
        /// <summary>
        /// Actualy can be used as selector to find elements. Will be working differently for pages and components.
        /// page      -> looks from the root of the doom
        /// component -> looks inside parent scope (element, frame, window etc)
        /// public Element SomeElement => _.FindCss("");
        /// </summary>
        protected virtual DriverScope _ => _browserScope;
        
        public BaseView(DriverScope browserScope)
        {
            if (browserScope == null)
                throw new ArgumentNullException("driver");
            _browserScope = browserScope;
        }
        
        /// <summary>
        /// Can be called to wait for IsLoaded to fire.
        /// </summary>
        /// <returns>TView</returns>
        public virtual TView WaitToLoad()
        {
            try
            {
                if (IsLoaded()) return this as TView; // Load, so just immediately return this
                var doNothing = new LambdaBrowserAction(() => {},  _NONE);
                var predicate = new LambdaPredicateQuery(IsLoaded, _NONE);
                _browserScope.TryUntil(doNothing, predicate);
            }
            catch (Exception e)
            {
                throw new ZukiniAssertionException($"Expected '{typeof(TView)}' failed to load.\nReason: {e}");
            }
            return this as TView;
        }
        
        /// <summary>
        /// Override this if you need to wait for custom wait condition on the page.
        /// </summary>
        /// <returns>true</returns> if IView is ready to be tested
        public virtual bool IsLoaded() => true; // Yes it is loaded by default

        /// <summary>
        /// Can be used by Assert to verify the page / component meets the contract.
        /// </summary>
        /// <param name="condition">some condition to be tested</param>
        protected virtual void AssertView(bool condition)
        {
            if (! condition)
                throw new ZukiniAssertionException($"Expected view '{typeof(TView)}' does not pass the assert requirement.");
        }
    }
}
