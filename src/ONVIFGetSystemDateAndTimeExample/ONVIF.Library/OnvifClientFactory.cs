using System;
using ONVIF.Framework;

namespace ONVIF.Library
{
    public class OnvifClientFactory
    {
        public IOnvifClient CreateClient(string uri)
        {
            return new Client(uri);
        }
    }
}