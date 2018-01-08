using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;

namespace ColorItAll.Data
{
    public class FamousQuoteManager
    {
        private readonly HttpClient _client;
        const string Url = "https://andruxnet-random-famous-quotes.p.mashape.com/";

        public FamousQuoteManager()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("X-Mashape-Key", "Nw0bZzbsHYmshaUlwqKo7F91AB3Vp1rJwAwjsn73FDSpKvfVhO");
        }

        public async Task<FamousQuote> GetFamousQuote()
        {
            try
            {
                var result = await _client.GetStringAsync("?cat=famous&count=1");
                return JsonConvert.DeserializeObject<FamousQuote>(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
        public async void LoadQuotePrompt()
        {
            var quote = await GetFamousQuote();
            if (quote != null)
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Title = $"Author: {quote.Author}",
                    Message = quote.Quote,
                    OkText = "One more!",
                    CancelText = "Cancel"
                });

                if (result)
                    LoadQuotePrompt();
            }
            else
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Title = "Something went wrong",
                    Message = "Make sure you have a working internet connection",
                    OkText = "Retry",
                    CancelText = "Cancel"
                });

                if (result)
                    LoadQuotePrompt();
            }
        }
    }
}
