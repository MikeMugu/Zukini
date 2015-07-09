using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zukini.Examples.Pages
{
    public class ApiPostHelper
    {
        public static List<KeyValuePair<string, string>> SetPostData<T>(T testClass)
        {
            var postData = new List<KeyValuePair<string, string>>();
            foreach (PropertyInfo property in testClass.GetType().GetProperties())
            {
                if (property.GetValue(testClass, null) != null)
                {
                    postData.Add(new KeyValuePair<string, string>(property.Name.ToString(), property.GetValue(testClass, null).ToString()));

                }
            }
            return postData;
        }


    }
}
