
namespace Zukini.UI.Pages
{
    public interface IView<out TView> where TView : class
    {
        /// <summary>
        /// Waits until IView is ready to be tested.
        /// </summary>
        /// <returns>TView</returns> constructed IView
        TView WaitToLoad();
        
        /// <summary>
        ///  Condition to be waited for. 
        /// <code>
        ///     public override bool IsLoaded() => 
        ///         new List<ElementScope> {Element1, Element2, ... , Elemen3}.FindAll(it => ! it.Exists()).Any();
        /// </code>
        /// </summary>
        /// <returns>true</returns> if IView is loaded
        bool IsLoaded();
    }
}
