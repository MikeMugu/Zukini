using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zukini
{
    public class PropertyAlreadyExistsException : Exception
    {
        public PropertyAlreadyExistsException(string key)
            : base(String.Format("A property with the name {0} was already rememberd."))
        {
        }
    }
}
