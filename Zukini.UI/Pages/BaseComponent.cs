using Coypu;

namespace Zukini.UI.Pages
{
    public abstract class BaseComponent<TView> : BaseView<TView> where TView : BaseComponent<TView>
    {
        private readonly DriverScope _parentScope;
        
        /// <summary>
        /// Standalone component? Scope is browserScope.
        /// </summary>
        /// <param name="browserScope">BrowserSession</param>
        public BaseComponent(DriverScope browserScope) : base(browserScope)
        {
            _parentScope = browserScope;
        }

        /// <summary>
        /// Locator will look inside parent scope.
        /// </summary>
        protected override DriverScope _ => _parentScope;
    }
}
