using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zukini.Examples.Features
{
    /// <summary>
    /// Helper class to make getting at the test settings easy. 
    /// To reference, simply use TestSettings.Property (e.g. TestSettings.ApplicationUrl).
    /// </summary>
    public static class TestSettings
    {
        public static string GoogleUrl { get { return ConfigurationManager.AppSettings["GoogleUrl"]; } }
        public static string W3SchoolsBaseUrl { get { return ConfigurationManager.AppSettings["W3SchoolsBaseUrl"]; } }
        public static string WWFUrl { get { return ConfigurationManager.AppSettings["WWFUrl"]; } }
    }
}
