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
    //denna klass hanterar httpclienten med tillhörande funktioner för att hämta data
     class Client
    {
        public Cats GlobalCats { get; set; }
        public HttpClient httpClient;
        public string baseAdress = "http://sogetiorebrointerview.azurewebsites.net/api/";
        public Client(HttpClient httpClient)
        {
            this.httpClient = httpClient;
           

        }

        public Client()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAdress);
   
        }
        #region Tasks som har med katter att göra
        public async Task<Cats> getCatsAsync(string path)
        {
            Cats cats = new Cats();
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                cats = JsonConvert.DeserializeObject<Cats>(
                    await response.Content.ReadAsStringAsync());
            }
            GlobalCats = cats;
            return cats;
        }

        public async Task getCats()
        {
            Cats cats = new Cats();
            Uri uri = new Uri(httpClient.BaseAddress, cats.uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            try
            { 
                cats = await getCatsAsync(uri.PathAndQuery);
                Console.Write(cats.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region ny order

        public async Task PlaceOrderAsync()
        {
            if(GlobalCats == null)
            {
                GlobalCats = await getCatsAsync("cats");
            }
            NewOrder newOrder = new NewOrder();
            List<String> userInput = new List<String>();
            String input = "";
            Console.WriteLine("Vilka katter vill du köpa? ange namn, tryck därefter enter. Avsluta med q när du är klar");

            while(!input.Equals("q"))
            {
                input = Console.ReadLine();
                if (input.Equals("q"))
                {
                    break;
                }
                else
                {
                    userInput.Add(input);
                }
            }

            foreach (var catname in userInput)
            {
                foreach (var catInDB in GlobalCats.cats)
                {
                    if(catInDB.name.Equals(catname))
                    {
                        newOrder.catIds.Add(catInDB.id);
                    }
                }
            }


            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(newOrder));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            
            var httpResponse = await httpClient.PostAsync("http://sogetiorebrointerview.azurewebsites.net/api/orders", httpContent);
            if(httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            
        }

        #endregion

        #region Se alla ordrars status
        //Varje gång denna funktion körs så returneras en orders kvitto
        public async Task<Receipt> getReceiptAsync(string path)
        {
            Receipt receipt = new Receipt();
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                receipt = JsonConvert.DeserializeObject<Receipt>(
                    await response.Content.ReadAsStringAsync());
            }
            return receipt;
        }

        //Varje gång denna funktion körs så returneras alla ordrar
        public async Task<Orders> getOrderstAsync(string path)
        {
            Orders orders= new Orders();
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                orders = JsonConvert.DeserializeObject<Orders>(
                    await response.Content.ReadAsStringAsync());
            }
            return orders;
        }
        //Hämta alla ordrar
        public async Task getOrderStatus()
        {
            Orders orders = new Orders();
            Receipt receipt = new Receipt();

            Uri uriGetOrders = new Uri(httpClient.BaseAddress, "orders");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            try
            {
                orders = await getOrderstAsync(uriGetOrders.PathAndQuery);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (var order in orders.orders)
            {
                Uri uri = new Uri(httpClient.BaseAddress, "orders/" + order.id + "/receipt");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    receipt = await getReceiptAsync(uri.PathAndQuery);
                    Console.Write(order.ToString() +" Status: ");
                    Console.Write(receipt.status+"\n");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
           
        }
        #endregion

    }
}
