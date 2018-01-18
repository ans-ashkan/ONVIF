
using System;
using System.Configuration;
using System.Threading.Tasks;
using ONVIF.Library;

namespace OnvifApplicationUser
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task AsyncMain(string[] args)
        {
            try
            {
                var clientFactory = new OnvifClientFactory();
                var baseUri = ConfigurationManager.AppSettings["BaseUri"];
                var client = clientFactory.CreateClient(baseUri);
                var systemDateAndTime = await client.DeviceService.GetSystemDateAndTimeAsync();
                Console.WriteLine(systemDateAndTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
