using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zukini
{
    public class PropertyBucket
    {
        private readonly Dictionary<string, object> _properties;

        /// <summary>
        /// Creates a new instance of the property bucket.
        /// </summary>
        internal PropertyBucket()
        {
            _properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Remembers a property in the PropertyBucket.
        /// </summary>
        /// <param name="key">Name of the item to save. Use this key to retrieve later.</param>
        /// <param name="item">The item to remember.</param>
        /// <param name="allowOverwrite"<c>true</c> to overwrite the property if it already exists, otherwise <c>false</c>.</param>
        public void Remember<T>(string key, T item, bool allowOverwrite = false)
        {
            SaveProperty<T>(key, item, allowOverwrite);
        }

        /// <summary>
        /// Remembers a property in the PropertyBucket.
        /// </summary>
        /// <param name="key">Name of the item to save. Use this key to retrieve later.</param>
        /// <param name="item">The item to remember.</param>
        /// <param name="allowOverwrite"<c>true</c> to overwrite the property if it already exists, otherwise <c>false</c>.</param>
        public void Remember(string key, object item, bool allowOverwrite = false)
        {
            SaveProperty(key, item, allowOverwrite);
        }

        /// <summary>
        /// Retrieves a property from the bucket using the specified key.
        /// If no property is found, a PropertyNotFoundException is thrown.
        /// </summary>
        /// <param name="key">Name of the item to retrieve.</param>
        public object GetProperty(string key)
        {
            object item;
            if (!TryGetValue<object>(key, out item))
            {
                throw new PropertyNotFoundException(key);
            }
            return item;
        }

        /// <summary>
        /// Retrieves a property from the bucket using the specified key.
        /// If no property is found, a PropertyNotFoundException is thrown.
        /// </summary>
        /// <param name="key">Name of the item to retrieve.</param>
        public T GetProperty<T>(string key) 
        {
            T item;
            if (!TryGetValue<T>(key, out item))
            {
                throw new PropertyNotFoundException(key);
            }
            return item;
        }

        /// <summary>
        /// Saves the given item in the property bucket. If allowOverwrite 
        /// is false, and the property already exists, this method will throw
        /// a <c>PropertyAlreadyExistsException</c>.
        /// </summary>
        /// <typeparam name="T">Type of item to save.</typeparam>
        /// <param name="key">The item key</param>
        /// <param name="item">The item to save</param>
        /// <param name="allowOverwrite"><c>true</c> to overwrite an existing property if it already exists.</param>
        private void SaveProperty<T>(string key, T item, bool allowOverwrite)
        {
            if (_properties.ContainsKey(key) && !allowOverwrite)
            {
                throw new PropertyAlreadyExistsException(key);
            }

            if (allowOverwrite && _properties.ContainsKey(key))
            {
                _properties[key] = item;
            }
            else
            {
                _properties.Add(key, item);
            }
        }

        /// <summary>
        /// Attempts to retrieve a value from the properties collection.
        /// </summary>
        /// <typeparam name="T">Type of item to retrieve.</typeparam>
        /// <param name="key">Name of the item to retrieve</param>
        /// <param name="item">Output argument to accept the item.</param>
        /// <returns><c>true</c> if the item could be found by name, otherwise <c>false</c>.</returns>
        private bool TryGetValue<T>(string key, out T item)
        {
            item = default(T);

            if (_properties != null && _properties.ContainsKey(key))
            {
                item = (T)_properties[key];
                return true;
            }
            
            return false;
        }
    }
}
