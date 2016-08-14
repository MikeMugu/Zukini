using System;

namespace Zukini.Pages
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
