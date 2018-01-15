using System;
using System.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace LowLevelStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri(ConfigurationManager.AppSettings["BaseUri"]);

            using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(uri.Host, uri.Port);
                WriteRequest(client);
                var dateTime = ReadResponse(client);
                Console.WriteLine(dateTime);
            }
        }

        static void WriteRequest(Socket client)
        {
            var body = Encoding.ASCII.GetBytes(@"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""><s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><GetSystemDateAndTime xmlns=""http://www.onvif.org/ver10/device/wsdl""/></s:Body></s:Envelope>");

            var requestBuilder = new StringBuilder();
            requestBuilder.AppendLine(@"POST / HTTP/1.1");
            requestBuilder.AppendLine(@"Content-Type: application/soap+xml; charset=utf-8; action=""http://www.onvif.org/ver10/device/wsdl/GetSystemDateAndTime""");
            requestBuilder.Append(@"Content-Length: ").Append(body.Length);
            requestBuilder.AppendLine();
            requestBuilder.AppendLine();
            var request = Encoding.ASCII.GetBytes(requestBuilder.ToString());
            client.Send(request);
            client.Send(body);
        }

        static DateTime ReadResponse(Socket client)
        {
            var responseBuilder = new StringBuilder();
            int bytesRead;
            var buffer = new byte[client.ReceiveBufferSize];
            while ((bytesRead = client.Receive(buffer)) > 0)
                responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            var response = responseBuilder.ToString();
            var contentLength = int.Parse(Regex.Match(response, @"Content-Length:\s+(?<length>\d+)\s+").Groups["length"].Value);
            var soapContent = response.Substring(response.Length - contentLength);

            var soapDoc = XDocument.Parse(soapContent);

            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;

            foreach (var node in soapDoc.Descendants())
            {
                switch (node.Name.LocalName)
                {
                    case "Hour":
                        hour = int.Parse(node.Value);
                        break;
                    case "Minute":
                        minute = int.Parse(node.Value);
                        break;
                    case "Second":
                        second = int.Parse(node.Value);
                        break;
                    case "Year":
                        year = int.Parse(node.Value);
                        break;
                    case "Month":
                        month = int.Parse(node.Value);
                        break;
                    case "Day":
                        day = int.Parse(node.Value);
                        break;
                }
            }

            var dt = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            return dt;
        }
    }
}
