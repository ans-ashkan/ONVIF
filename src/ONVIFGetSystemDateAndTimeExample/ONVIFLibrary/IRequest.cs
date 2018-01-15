using System;
using System.Net.Http;

namespace ONVIF.Library
{
    internal interface IRequest
    {
        HttpRequestMessage GetRequest();
    }
}