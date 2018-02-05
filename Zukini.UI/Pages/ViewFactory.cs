using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using Coypu;

namespace Zukini.UI.Pages
{
    public class ViewFactory : IViewFactory
    {
        private readonly IObjectContainer _container;

        /// <summary>
        /// Containter with all the bindings.
        /// </summary>
        /// <param name="container"></param>
        public ViewFactory(IObjectContainer container)
        {
            _container = container;
        }

        /// <inheritdoc />
        public TView Get<TView>() where TView : class, IView<TView> 
            => _container.Resolve<TView>();

        /// <inheritdoc />
        public TView Load<TView>() where TView : class, IView<TView> 
            => Get<TView>().WaitToLoad();

        /// <inheritdoc />
        public TView Get<TView>(Func<TView> supplier) where TView : class, IView<TView> 
            => supplier.Invoke();

        /// <inheritdoc />
        public TView Load<TView>(Func<TView> supplier) where TView : class, IView<TView> 
            => Get(supplier).WaitToLoad();

        /// <inheritdoc />
        public IEnumerable<TView> GetAll<TView, TScope>(IEnumerable<TScope> elements, Func<TScope, TView> mapFunc)
            where TView : class, IView<TView> 
            where TScope : DriverScope
            => elements.Select(mapFunc);

        /// <inheritdoc />
        public IEnumerable<TView> GetAll<TView>(IEnumerable<DriverScope> elements)
            where TView : BaseComponent<TView>
        {
            Func<DriverScope, TView> mapFunc = it => (TView) Activator.CreateInstance(typeof(TView), it);
            return GetAll(elements, mapFunc);
        }
        
        /// <inheritdoc />
        public IEnumerable<TView> LoadAll<TView, TScope>(IEnumerable<TScope> elements, Func<TScope, TView> mapFunc) 
            where TView : class, IView<TView>
            where TScope : DriverScope
            => GetAll(elements, mapFunc).Select(it => it.WaitToLoad());

        /// <inheritdoc />
        public IEnumerable<TView> LoadAll<TView>(IEnumerable<DriverScope> elements)
            where TView : BaseComponent<TView>
            => GetAll<TView>(elements).Select(it => it.WaitToLoad());
        
        // TODO Attributes processor. Factory takes a map {Actibute, Action} with registered attributes
        // Example: [WaitOnLoad] which will automatically redirect the call wait.
        // [Listener = Some] to register Javascript listener... ETC.
    }
}
