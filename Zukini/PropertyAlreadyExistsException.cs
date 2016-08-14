using System;

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
