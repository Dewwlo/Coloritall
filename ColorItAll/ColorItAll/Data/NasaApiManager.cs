using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using Newtonsoft.Json;

namespace ColorItAll.Data
{
    public class NasaApiManager
    {
        const string Url = "https://andruxnet-random-famous-quotes.p.mashape.com/";

        public async Task<FamousQuote> GetFamousQuote()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Mashape-Key", "Nw0bZzbsHYmshaUlwqKo7F91AB3Vp1rJwAwjsn73FDSpKvfVhO");
            try
            {
                string result = await client.GetStringAsync(Url + "?cat=famous&count=1");
                return JsonConvert.DeserializeObject<FamousQuote>(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
