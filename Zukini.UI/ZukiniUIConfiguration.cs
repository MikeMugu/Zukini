
namespace Zukini.UI
{
    public class ZukiniUIConfiguration
    {
        private string _screenshotDirectory = "Screenshots";
        private bool _maximizeBrowser = true;

        /// <summary>
        /// Get or set the path to where screenshots should be saved.
        /// </summary>
        public string ScreenshotDirectory 
        {
            get { return _screenshotDirectory; }
            set { _screenshotDirectory = value; } 
        }
        
        /// <summary>
        /// Get or set whether to maximize the browser when starting up.
        /// </summary>
        public bool MaximizeBrowser 
        {
            get { return _maximizeBrowser; } 
            set { _maximizeBrowser = value; } 
        }

    }
}
