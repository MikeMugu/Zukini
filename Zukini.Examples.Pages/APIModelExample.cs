using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zukini.Examples.Pages
{
    public class APIModelExample
    {
    }

    public class FakeRootObject
    {
        public List<Fields> Data { get; set; }
    }



    public class Fields
    {
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
        public string comments { get; set; }
    }
}
