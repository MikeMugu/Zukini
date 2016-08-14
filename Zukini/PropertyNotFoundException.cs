using System;

namespace Zukini
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string propertyName)
            : base(String.Format("Property named {0} was not found, are you sure it was remembered?", propertyName))
        {
        }
    }
}
