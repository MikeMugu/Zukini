using RestSharp.Deserializers;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace Zukini.API
{
    public class DynamicJsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            var converter = new ExpandoObjectConverter();
            dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(response.Content, converter);

            return result;
        }
    }
}
