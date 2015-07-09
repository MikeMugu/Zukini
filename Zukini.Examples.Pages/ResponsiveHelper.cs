using Coypu;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.Pages;



namespace Zukini.Examples.Pages
{
    public class ResponsiveHelper : BasePage
    {



        public ResponsiveHelper(BrowserSession browser)
            : base(browser)
        {
        }


        public void AAndBLineUp(ElementScope field, ElementScope field2)
        {
            string One = field["offsetTop"];
            string Two = field2["offsetTop"];
            int one = int.Parse(One);
            int two = int.Parse(Two);
            Assert.AreEqual(one, two);
        }

        public void ABeforeB(ElementScope field, ElementScope field2)
        {
            string One = field["offsetTop"];
            string Two = field2["offsetTop"];
            int one = int.Parse(One);
            int two = int.Parse(Two);
            int result = (two - one);
            Assert.Less(one, two);
        }



    }
}