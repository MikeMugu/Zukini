using System;
using System.Collections.Generic;
using System.IO;
using Coypu;
using Zukini.UI.Pages;

namespace Zukini.UI.Examples.Pages
{
    /// <summary>
    /// Almost real page.
    /// </summary>
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

        /// Being polled for DelayedElement to exist if ViewFactory#Load is called.
        public override bool IsLoaded()
        {
            return DelayedElement.Exists();
        }
        
        private string ReadPageHtml()
        {
            var folder = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (folder == null)
                throw new ArgumentException("Something went wrong. Cannot find project directory.");

            var path = Path.Combine(folder, "fake.html");
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Populates the page with DOM.
        /// </summary>
        private void PopulatePageWithSomeContent()
        {
            var script = ReadPageHtml();
            _browser.ExecuteScript(@"document.open();
                           document.write(arguments[0]); 
                           document.close();",
                           script);
        }
        
        public YouTubeComponent FindPlayerByTitle(string frameTitle)
        {
            return _viewFactory.Load(() => new YouTubeComponent(_.FindFrame(frameTitle)));
        }

        public IEnumerable<GalleryImageComponent> GalleryImages1
            => _viewFactory.LoadAll<GalleryImageComponent>(_.FindAllCss(".gallery"));

        public IEnumerable<GalleryImageComponent> GalleryImages2
            => _viewFactory.LoadAll(_.FindAllCss(".gallery"), el => new GalleryImageComponent(el));
    }

    /// <summary>
    /// Implementation of youtube page component
    /// </summary>
    public class YouTubeComponent : BaseComponent<YouTubeComponent>
    {
        public YouTubeComponent(DriverScope browserScope) : base(browserScope){}

        public ElementScope Title => _.FindCss(".ytp-title");
        public ElementScope PlayButton => _.FindCss(".ytp-large-play-button");
        public ElementScope Controls => _.FindCss("[class*=controls] [class*=time-display]");

        /// Being polled for DelayedElement to exist if ViewFactory#Load is called.
        public override bool IsLoaded()
        {
            return Title.Exists();
        }
    }

    public class GalleryImageComponent : BaseComponent<GalleryImageComponent>
    {
        public GalleryImageComponent(DriverScope browserScope) : base(browserScope){}

        public ElementScope Desciption => _.FindCss(".desc");
        public ElementScope Image => _.FindCss("img");
    }
    
    public class MissingFakePage : BasePage<MissingFakePage>
    {
        public MissingFakePage(BrowserSession browser) : base(browser) { }

        public override bool IsLoaded()
        {
            return false;
        }
    }

    public class MissingFakeComponent : BaseComponent<MissingFakeComponent>
    {
        public MissingFakeComponent(DriverScope browserScope) : base(browserScope) { }

        public override bool IsLoaded()
        {
            return false;
        }
    }
}
