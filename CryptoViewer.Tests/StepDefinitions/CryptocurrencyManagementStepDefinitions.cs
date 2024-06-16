using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.API.Models;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class CryptocurrencyManagementStepDefinitions
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _jsonContent;

        public CryptocurrencyManagementStepDefinitions()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:7077/");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3MTg1NDIyOTYsImV4cCI6MTcxOTE0NzA5NiwiaWF0IjoxNzE4NTQyMjk2fQ.epNLAp56DdHNld92WzcxMUm1nqmq9ywk8dg0Mog6nmk");
        }

        [When("I send a GET request to /api/trackerapi")]
        public async Task WhenISendAGETRequestToApiTrackerapi()
        {
            _response = await _client.GetAsync("/api/trackerapi");
        }

        [Then("I should receive a list of cryptocurrencies")]
        public async Task ThenIShouldReceiveAListOfCryptocurrencies()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            var cryptocurrencies = JsonConvert.DeserializeObject<List<CryptocurrencyResource>>(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(cryptocurrencies);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(cryptocurrencies.Count > 0);
        }

        [Given("a cryptocurrency with ID (.*) exists")]
        public void GivenACryptocurrencyWithIDExists(int id)
        {
            // Assume this step sets up a cryptocurrency in the database for testing
            // This might involve setting up a mock repository or pre-populating the test database
        }

        [When("I send a GET request to /api/trackerapi/(.*)")]
        public async Task WhenISendAGETRequestToApiTrackerapi(int id)
        {
            _response = await _client.GetAsync($"/api/trackerapi/{id}");
        }

        [Then("I should receive the details of the cryptocurrency with ID (.*)")]
        public async Task ThenIShouldReceiveTheDetailsOfTheCryptocurrencyWithID(int id)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            var cryptocurrency = JsonConvert.DeserializeObject<CryptocurrencyResource>(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(cryptocurrency);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(id, cryptocurrency.Id);
        }

        [Then("the response should include a self-link")]
        public async Task ThenTheResponseShouldIncludeASelfLink()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var cryptocurrency = JsonConvert.DeserializeObject<CryptocurrencyResource>(content);
            var selfLink = cryptocurrency.Links.FirstOrDefault(link => link.Rel == "self");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(selfLink);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(selfLink.Href.Contains($"/api/trackerapi/{cryptocurrency.Id}"));
        }

        [Given("I have a valid cryptocurrency model")]
        public void GivenIHaveAValidCryptocurrencyModel()
        {
            var model = new AddCryptocurrencyViewModel
            {
                Name = "Litecoin",
                LogoPath = "/path3",
                TrackerAction = "track",
                BorderColor = "#A6A9AA"
            };
            _jsonContent = JsonConvert.SerializeObject(model);
        }

        [Given("I have an invalid cryptocurrency model")]
        public void GivenIHaveAnInvalidCryptocurrencyModel()
        {
            var model = new AddCryptocurrencyViewModel
            {
                Name = "",
                LogoPath = "",
                TrackerAction = "invalidAction",
                BorderColor = "invalidColor"
            };
            _jsonContent = JsonConvert.SerializeObject(model);
        }

        [When("I send a POST request to /api/trackerapi with the model")]
        public async Task WhenISendAPOSTRequestToApiTrackerapiWithTheModel()
        {
            var content = new StringContent(_jsonContent, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/trackerapi", content);
        }

        [Then("I should receive a created response with the new cryptocurrency details")]
        public async Task ThenIShouldReceiveACreatedResponseWithTheNewCryptocurrencyDetails()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            var cryptocurrency = JsonConvert.DeserializeObject<CryptocurrencyResource>(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(cryptocurrency);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(cryptocurrency.Id > 0);
        }

        [Then("the response should include a self-link to the new cryptocurrency")]
        public async Task ThenTheResponseShouldIncludeASelfLinkToTheNewCryptocurrency()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var cryptocurrency = JsonConvert.DeserializeObject<CryptocurrencyResource>(content);
            var selfLink = cryptocurrency.Links.FirstOrDefault(link => link.Rel == "self");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(selfLink);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(selfLink.Href.Contains($"/api/trackerapi/{cryptocurrency.Id}"));
        }

        [Then("I should receive a bad request response")]
        public void ThenIShouldReceiveABadRequestResponse()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}
