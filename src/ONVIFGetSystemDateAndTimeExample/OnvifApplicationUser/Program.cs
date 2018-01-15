
using System;
using System.Configuration;
using ONVIF.Library;

namespace OnvifApplicationUser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var clientFactory = new OnvifClientFactory(); 
                var baseUri = ConfigurationManager.AppSettings["BaseUri"];
                var client = clientFactory.CreateClient(baseUri);
                Console.WriteLine(client.DeviceService.GetSystemDateAndTimeAsync().ConfigureAwait(false).GetAwaiter().GetResult());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
