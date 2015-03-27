using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IContactPro.Test.Functional
{
    /// <summary>
    /// Helper class to make getting at the test settings easy. 
    /// To reference, simply use TestSettings.Property (e.g. TestSettings.ApplicationUrl).
    /// </summary>
    public static class TestSettings
    {
        public static string ApplicationUrl { get { return ConfigurationManager.AppSettings["ApplicationUrl"]; } }
        public static string SmokeTestUsername { get { return ConfigurationManager.AppSettings["SmokeTestUsername"]; } }
        public static string SmokeTestPassword { get { return ConfigurationManager.AppSettings["SmokeTestPassword"]; } }
    }
}
