using System.Configuration;

namespace Zukini.API.Examples.Features
{
    /// <summary>
    /// Helper class to make getting at the test settings easy. 
    /// To reference, simply use TestSettings.Property (e.g. TestSettings.ApplicationUrl).
    /// </summary>
    public static class TestSettings
    {
        public static string JsonPlaceholderApiUrl {  get { return ConfigurationManager.AppSettings["JsonPlaceholderApiUrl"]; } }
    }
}
