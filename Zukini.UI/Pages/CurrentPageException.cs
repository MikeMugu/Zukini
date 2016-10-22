using System;

namespace Zukini.UI.Pages
{
    public class CurrentPageException 
        : Exception
    {
        public CurrentPageException(string message) 
            : base(message)
        {
        }
    }
}
