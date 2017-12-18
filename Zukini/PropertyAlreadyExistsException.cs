using System;

namespace Zukini
{
    public class PropertyAlreadyExistsException : Exception
    {
        public PropertyAlreadyExistsException(string key)
            : base($"A property with the name {key} was already remembered."){}
    }
}
