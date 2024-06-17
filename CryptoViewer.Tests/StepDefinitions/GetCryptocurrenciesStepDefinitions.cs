using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.MVC.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CryptoViewer.API.Models;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.Tests.Steps
{
    [Binding]
    public class TrackerApiSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private List<Cryptocurrency> _cryptocurrencies;

        public TrackerApiSteps()
        {
            _httpClient = new HttpClient();
            _apiHelper = new ApiHelper(_httpClient);
        }

        [Given(@"the API is running")]
        public void GivenTheAPIIsRunning()
        {
            
        }

        [When(@"the user sends a GET request to ""(.*)""")]
        public async Task WhenTheUserSendsAGETRequestTo(string url)
        {
            
            _response = await _httpClient.GetAsync(url);
            _response.EnsureSuccessStatusCode();

           
            var responseString = await _response.Content.ReadAsStringAsync();

            
            _cryptocurrencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(responseString);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
          Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual((HttpStatusCode)statusCode, _response.StatusCode);
        }

        [Then(@"the response should contain a list of cryptocurrencies")]
        public void ThenTheResponseShouldContainAListOfCryptocurrencies()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_cryptocurrencies);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_cryptocurrencies.Count > 0);
        }
    }
}
