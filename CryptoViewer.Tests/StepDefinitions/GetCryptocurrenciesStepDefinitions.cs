using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.MVC.Helpers;
using CryptoViewer.API.Models;
using System.Collections.Generic;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class GetCryptocurrenciesStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private string _responseContent;

        public GetCryptocurrenciesStepDefinitions()
        {
            _apiHelper = new ApiHelper(new HttpClient());
        }

        [Given(@"the API is running")]
        public void GivenTheAPIIsRunning()
        {
            // This step can be used to ensure the API is running. 
            // For this example, we'll assume the API is running.
        }

        [When(@"the user sends a GET request to ""([^""]*)""")]
        public async Task WhenTheUserSendsAGETRequestTo(string endpoint)
        {
            // Get the response from the API
            _response = await _apiHelper.GetAsync<HttpResponseMessage>(endpoint);
            // Get the response content as a string
            _responseContent = await _response.Content.ReadAsStringAsync();
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode, $"Expected status code: {expectedStatusCode}, but got: {(int)_response.StatusCode}");
        }

        [Then(@"the response should contain a list of cryptocurrencies")]
        public void ThenTheResponseShouldContainAListOfCryptocurrencies()
        {
            var cryptocurrencies = JsonConvert.DeserializeObject<List<CryptocurrencyResource>>(_responseContent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(cryptocurrencies, "Response does not contain a valid list of cryptocurrencies.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(cryptocurrencies.Any(), "The list of cryptocurrencies is empty.");
        }
    }
}
