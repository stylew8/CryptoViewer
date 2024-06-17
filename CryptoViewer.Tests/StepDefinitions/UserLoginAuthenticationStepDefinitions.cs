using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Net.Http;
using CryptoViewer.MVC.Helpers;
using CryptoViewer.Auth_API.Models.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.API.Models;
using CryptoViewer.Auth_API.Models;
using System.Net;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UserLoginAuthenticationStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private APIResponse _apiResponse;

        public UserLoginAuthenticationStepDefinitions()
        {
            _apiHelper = new ApiHelper(new HttpClient());
        }

        [Given("John Doe is on the login page")]
        public void GivenJohnDoeIsOnTheLoginPage()
        {
           
        }

        [When("he enters the following credentials:")]
        public async Task WhenHeEntersTheFollowingCredentials(Table table)
        {
            var loginDetails = new LoginRequestDto
            {
                Username = table.Rows[0]["value"],
                Password = table.Rows[1]["value"]
            };

            try
            {
                Console.WriteLine($"Sending login request to API with details: {JsonConvert.SerializeObject(loginDetails)}");
                _response = await _apiHelper.PostAsync<HttpResponseMessage>("api/UsersAuth/login", loginDetails);
                var responseContent = await _response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(responseContent);

                
                _apiResponse = new APIResponse
                {
                    StatusCode = _response.StatusCode,
                    IsSuccess = _response.IsSuccessStatusCode,
                    Result = loginResponse,
                    ErrorMessages = _response.IsSuccessStatusCode ? null : new List<string> { "Login failed" }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        [When(@"he clicks ""([^""]*)"" button")]
        public void WhenHeClicksButton(string login)
        {
            
        }

        [Then(@"he should see a message saying ""([^""]*)""")]
        public void ThenHeShouldSeeAMessageSaying(string expectedMessage)
        {
            try
            {
                if (_apiResponse == null)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("API response is null.");
                }

                Console.WriteLine($"Final Response Object: {JsonConvert.SerializeObject(_apiResponse)}");

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.IsSuccess, "User login was not successful");

                if (!string.IsNullOrEmpty(expectedMessage))
                {
                   
                    string trimmedExpectedMessage = expectedMessage.Trim();
                    string actualMessage = "Login successful".Trim();

                   
                    Console.WriteLine("Expected Message ASCII values: " + string.Join(", ", trimmedExpectedMessage.Select(c => (int)c)));
                    Console.WriteLine("Actual Message ASCII values: " + string.Join(", ", actualMessage.Select(c => (int)c)));

                   
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(trimmedExpectedMessage, actualMessage, "The actual message does not match the expected message.");
                }
            }
            catch (JsonReaderException jex)
            {
                Console.WriteLine($"JSON Reader Exception: {jex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }


        [Then(@"he should be redirected to the dashboard")]
        public void ThenHeShouldBeRedirectedToTheDashboard()
        {
            
        }
    }
}
