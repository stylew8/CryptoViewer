using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System.Net.Http;
using Newtonsoft.Json;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.MVC.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CryptoViewer.API.Models;

namespace CryptoViewer.Tests.Steps
{
    [Binding]
    public class TrackerApiSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private APIResponse _apiResponse;

        public TrackerApiSteps()
        {
            _httpClient = new HttpClient();
            _apiHelper = new ApiHelper(_httpClient);
        }

        [Given(@"the API is running")]
        public void GivenTheAPIIsRunning()
        {
            // Assuming the API is already running and accessible
        }

        [When(@"the user sends a GET request to ""(.*)""")]
        public async Task WhenTheUserSendsAGETRequestTo(string url)
        {
            // Send the GET request and capture the response
            _response = await _httpClient.GetAsync(url);
            _response.EnsureSuccessStatusCode();

            // Deserialize the response content into APIResponse
            var responseString = await _response.Content.ReadAsStringAsync();
            _apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual((HttpStatusCode)statusCode, _response.StatusCode);
        }

        [Then(@"the response should contain a list of cryptocurrencies")]
        public void ThenTheResponseShouldContainAListOfCryptocurrencies()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.IsSuccess);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_apiResponse.Result);

            var cryptocurrencies = JsonConvert.DeserializeObject<List<CryptocurrencyResource>>(_apiResponse.Result.ToString());
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(cryptocurrencies != null && cryptocurrencies.Count > 0);
        }
    }
}
