using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.Pages;

namespace Zukini.Examples.Pages
{
    public class WWFPage : BasePage
    {
        public WWFPage(BrowserSession browser)
            : base(browser)
        { 
        }

        public ElementScope Header { get { return Browser.FindId("header"); } }
        public ElementScope MenuIcon { get { return Browser.FindCss(".ico.open"); } }

        public ElementScope Text { get { return Browser.FindCss(".span6.gutter-top-in-4.gutter-bottom-in-2.gutter-horiz-in"); } }
        public ElementScope Image { get { return Browser.FindCss(".span6.medium-break.gutter-top-in-3.gutter-horiz-in.gutter-bottom-in-2"); } }

    }
}
