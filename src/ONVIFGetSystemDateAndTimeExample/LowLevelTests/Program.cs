using ONVIF.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpRequest = new HttpRequest(HttpMethod.GET, "http://google.com");
            var resp = httpRequest.GetResponse();
            Console.WriteLine(resp.Content.Length);
        }
    }
}
