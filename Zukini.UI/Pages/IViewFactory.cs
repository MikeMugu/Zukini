using System;
using System.Collections.Generic;
using Coypu;

namespace Zukini.UI.Pages
{
    public interface IViewFactory
    {   
        /// <summary>
        /// Constructs an IView. Similar to new IView with dependencies resolution. 
        /// </summary>
        /// <typeparam name="TView">Type of view</typeparam>
        /// <returns>IView</returns>
        TView Get<TView>() where TView : class, IView<TView>;

        /// <summary>
        /// Constructs an IView and calls post construction methods, like IView#WaitToLoad.
        /// </summary>
        /// <typeparam name="TView">Type of view</typeparam>
        /// <returns>IView</returns>
        TView Load<TView>() where TView : class, IView<TView>;
        
        /// <summary>
        /// This maybe used for Views with parent scope, like components.
        /// <code>
        /// public class Activity : BaseComponent<Activity>
        /// {
        ///     public Activity(BrowserSession browser, DriverScope scope) : base(browser, scope){ }
        ///     public ElementScope Status => _.FindCss(".status");
        /// }
        /// </code>
        /// To construct it with view factory:
        /// <code>
        /// _factory.Get(() => new Activity(Browser, _.FindCss("parentSelector")));
        /// </code> 
        /// </summary>
        /// <typeparam name="TView">to be constructed</typeparam>
        /// <param name="supplier">how to create a view</param>
        /// <returns></returns>
        TView Get<TView>(Func<TView> supplier) where TView : class, IView<TView>;

        /// <summary>
        /// The same as this#Get, but also will execute post constuction steps. Like wait for it.
        /// </summary>
        /// <typeparam name="TView">to be constructed</typeparam>
        /// <param name="supplier">how to construct</param>
        /// <returns></returns>
        TView Load<TView>(Func<TView> supplier) where TView : class, IView<TView>;

        /// <summary>
        /// Converts a sequence of ElementScope to a sequence of IView.
        /// <code>
        /// IEnumerable<Activity> many = _factory.GetAll(Activities, it => new Activity(_session, it))
        /// </code> 
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TScope"></typeparam>
        /// <param name="elements"></param>
        /// <param name="mapFunc"></param>
        /// <returns></returns>
        IEnumerable<TView> GetAll<TView, TScope>(IEnumerable<TScope> elements,
            Func<TScope, TView> mapFunc)
            where TView : class, IView<TView>
            where TScope : DriverScope;

        /// <summary>
        /// Converts a sequence of ElementScope to a sequence of IView.
        /// <code>
        /// IEnumerable<Activity> many = _factory.GetAll<Activity>(Activities);
        /// </code> 
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="elements"></param>
        /// <returns></returns>
        IEnumerable<TView> GetAll<TView>(IEnumerable<DriverScope> elements)
                                         where TView : BaseComponent<TView>;

        /// <summary>
        /// Converts a sequence of ElementScope to a sequence of IView with post constuction actions, like wait.
        /// <code>
        /// IEnumerable<Activity> many = _factory.LoadAll(Activities, it => new Activity(_session, it)) 
        /// </code> 
        /// + call WaitToLoad on each Activity object.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TScope"></typeparam>
        /// <param name="elements"></param>
        /// <param name="mapFunc"></param>
        /// <returns></returns>
        IEnumerable<TView> LoadAll<TView, TScope>(IEnumerable<TScope> elements, 
                                         Func<TScope, TView> mapFunc) 
                                         where TView : class, IView<TView>
                                         where TScope : DriverScope;

        /// <summary>
        /// Converts a sequence of ElementScope to a sequence of BaseComponent with post constuction actions, like wait.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="elements"></param>
        /// <returns></returns>
        IEnumerable<TView> LoadAll<TView>(IEnumerable<DriverScope> elements) where TView : BaseComponent<TView>;
    }
}