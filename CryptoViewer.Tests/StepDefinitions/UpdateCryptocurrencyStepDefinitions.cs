using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CryptoViewer.DAL.Crypto;
using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Models.Dto;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.DAL.Models;
using CryptoViewer.API.Models;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UpdateCryptocurrencyStepDefinitions
    {
     
        private HttpResponseMessage _response;
        private readonly HttpClient _httpClient;
        private Cryptocurrency crypto;

        public UpdateCryptocurrencyStepDefinitions()
        {
            _httpClient = new HttpClient();
      
        }

        [Given(@"a cryptocurrency with ID (.*) exists")]
        public async Task GivenACryptocurrencyWithIDExists(int id)
        {
            _response = await _httpClient.GetAsync($"http://localhost:5004/api/TrackerApi/{id}");
            _response.EnsureSuccessStatusCode();

            var responseString = await _response.Content.ReadAsStringAsync();
            crypto = JsonConvert.DeserializeObject<Cryptocurrency>(responseString);
        }

        [When(@"the admin sends a PUT request to ""([^""]*)"" with the following data:")]
        public async Task WhenTheAdminSendsAPUTRequestToWithTheFollowingData(string endpoint, Table table)
        {
            var data = table.CreateInstance<AddCryptocurrencyViewModel>();

           
            if (string.IsNullOrEmpty(data.Name))
            {
                data.Name = crypto.Name;
            }

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            
           

            _response = await _httpClient.PutAsync($"http://localhost:5004/{endpoint}", content);

           
            var responseContent = await _response.Content.ReadAsStringAsync();
          
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedStatusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"the response should contain the updated cryptocurrency")]
        public async Task ThenTheResponseShouldContainTheUpdatedCryptocurrency()
        {
            var responseData = await _response.Content.ReadAsStringAsync();
            var updatedCryptocurrency = JsonConvert.DeserializeObject<Cryptocurrency>(responseData);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(updatedCryptocurrency);
           
        }
    }
}
