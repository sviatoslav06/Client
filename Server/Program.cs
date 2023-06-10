using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Ukraine_ = { "Rivne", "Kyiv", "Kharkiv", "Lviv", "Odessa" };
            string[] Poland_ = { "Vrozlav", "Warshav", "Krakiv", "Lodz", "Lublin" };
            string[] Germany_ = { "Berlin", "Munchen", "Frankfurt", "Keln" };
            Dictionary<string, string[]> dictionary = new()
            {
                ["Ukraine"] = new[] { "Rivne", "Kyiv", "Kharkiv", "Lviv", "Odessa" },
                ["Poland"] = new[] { "Vrozlav", "Warshav", "Krakiv", "Lodz", "Lublin" },
                ["Germany"] = new[] { "Berlin", "Munchen", "Frankfurt", "Keln" }
            };

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.7.13.11"), 3344);

            UdpClient server = new UdpClient(endPoint);

            while (true)
            {
                Console.WriteLine("...Waiting for the request...");

                IPEndPoint clientEndPoint = null;

                byte[] request = server.Receive(ref clientEndPoint);

                string message = Encoding.UTF8.GetString(request);
                Console.WriteLine($"Received message: {message} : {DateTime.Now.ToShortTimeString()} from {clientEndPoint}");
                string[] responses = null;
                foreach (KeyValuePair<string, string[]> item in dictionary)
                {
                    if (message.ToLower() == item.Key.ToLower())
                    {
                        responses = item.Value;
                    }
                    else responses = null;
                }
                //if(message == "Ukraine")
                //{
                //    foreach (var c in Ukraine_)
                //    {
                //        responses += c + " ";
                //    }
                //}
                //else if (message == "Poland")
                //{
                //    foreach (var c in Poland_)
                //    {
                //        responses += c + " ";
                //    }
                //}
                //if (message == "Germany")
                //{
                //    foreach (var c in Germany_)
                //    {
                //        responses += c + " ";
                //    }
                //}
                string result = string.Join(" ", responses);
                byte[] response = Encoding.UTF8.GetBytes(result);
                server.Send(response, response.Length, clientEndPoint);
            }
        }
    }
}
