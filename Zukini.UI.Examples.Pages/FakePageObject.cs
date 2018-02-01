using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coypu;
using Zukini.UI.Pages;

namespace Zukini.UI.Examples.Pages
{
    public class FakePageObject : BasePage<FakePageObject>
    {
        private readonly IViewFactory _viewFactory;
        private readonly BrowserSession _browser;

        public FakePageObject(BrowserSession browser, IViewFactory viewFactory) : base(browser)
        {
            _viewFactory = viewFactory;
            _browser = browser;

            PopulatePageWithSomeContent();
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

        private void PopulatePageWithSomeContent()
        {
            var script = ReadPageHtml();
            _browser.ExecuteScript(@"document.open();
                           document.write(arguments[0]); 
                           document.close();",
                           script);
        }

        public FakeYouTubeComponent RapsodyPlayer => _viewFactory.Load(() => new FakeYouTubeComponent(_.FindFrame("Rhapsody")));
        public FakeYouTubeComponent StarWardsPlayer => _viewFactory.Load(() => new FakeYouTubeComponent(_.FindFrame("Star Wars")));
    }

    public class FakeYouTubeComponent : BaseComponent<FakeYouTubeComponent>
    {
        public FakeYouTubeComponent(DriverScope browserScope) : base(browserScope){}

        public ElementScope Title => _.FindCss(".ytp-title");
        public ElementScope PlayButton => _.FindCss(".ytp-large-play-button");
        public ElementScope Controls => _.FindCss("[class*=controls] [class*=time-display]");
    }
}
