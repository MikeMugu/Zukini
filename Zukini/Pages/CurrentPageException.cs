using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
