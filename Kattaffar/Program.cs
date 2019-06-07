using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kattaffar
{
    class Program
    {        
        static void Main(string[] args)
        {
            string choice = "";
            Client client = new Client();

            while (!choice.Equals("q"))
            {
                switch (choice)
                {
                    case "1":
                        client.getCats().GetAwaiter().GetResult();
                        choice = "";
                        break;

                    case "2":
                        client.PlaceOrderAsync().GetAwaiter().GetResult();
                        choice = "";
                        break;
                    case "3":
                        client.getOrderStatus().GetAwaiter().GetResult();
                        choice = "";
                        break;
                    default:
                        Console.WriteLine("Välkommen till kattaffären");
                        Console.WriteLine("1. Visa katter");
                        Console.WriteLine("2. Lägg order");
                        Console.WriteLine("3. Visa ordrar");
                        Console.WriteLine("q. Avsluta");
                        choice = Console.ReadLine();
                        break;
                }
            }

            
        }
    }
}
