using System.IO;
using Coypu;
using Zukini.UI.Pages;

namespace Zukini.UI.Examples.Pages
{
    public class FakePageObject : BasePage<FakePageObject>
    {
        private readonly IViewFactory _viewFactory;

        public FakePageObject(BrowserSession browser, IViewFactory viewFactory) : base(browser)
        {
            _viewFactory = viewFactory;
            var script = ReadPageHtml();
            browser.ExecuteScript(@"document.open();
                           document.write(arguments[0]); 
                           document.close();", 
                           script);
        }

        public ElementScope Title => _.FindCss("h1");
        public ElementScope DelayedElement => _.FindId("delayed");

        public override bool IsLoaded()
        {
            return DelayedElement.Exists();
        }
        
        private string ReadPageHtml()
        {
            var path = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), "fake.html");
            return File.ReadAllText(path);
        }
    }

    public class FakeYouTubeComponent : BaseComponent<FakeYouTubeComponent>
    {
        public FakeYouTubeComponent(DriverScope browserScope) : base(browserScope){}
        //public void Play
    }
}
