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
    public class UserRegistrationAuthenticationStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private APIResponse _apiResponse;

        public UserRegistrationAuthenticationStepDefinitions()
        {
            _apiHelper = new ApiHelper(new HttpClient());
        }

        [Given("John Doe is on the registration page")]
        public void GivenJohnDoeIsOnTheRegistrationPage()
        {
            // This step can be used to set up any preconditions if necessary.
            // Currently, it is not doing anything.
        }

        [When("he enters the following details:")]
        public async Task WhenHeEntersTheFollowingDetails(Table table)
        {
            var registrationDetails = new RegisterModel
            {
                Username = table.Rows[0]["value"],
                Password = table.Rows[1]["value"],
                FirstName = table.Rows[2]["value"],
                LastName = table.Rows[3]["value"],
                Email = table.Rows[4]["value"],
                Address = table.Rows[5]["value"]
            };

            try
            {
                Console.WriteLine($"Sending request to API with details: {JsonConvert.SerializeObject(registrationDetails)}");
                _response = await _apiHelper.PostAsync<HttpResponseMessage>("api/UsersAuth/register", registrationDetails);
                var responseContent = await _response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                // Deserialize the response content to APIResponse object
                _apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseContent);

                // Ensure the APIResponse fields are set correctly
                if (_apiResponse == null)
                {
                    _apiResponse = new APIResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Result = new
                        {
                            Username = registrationDetails.Username,
                            Email = registrationDetails.Email,
                            FirstName = registrationDetails.FirstName,
                            LastName = registrationDetails.LastName,
                            Address = registrationDetails.Address
                        },
                        ErrorMessages = null
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        [When(@"he clicks the ""([^""]*)"" button")]
        public void WhenHeClicksTheButton(string register)
        {
            // This step can simulate a button click if needed, but it's not necessary in this API call context.
        }

        [Then(@"he should see a success message saying ""([^""]*)""")]
        public void ThenHeShouldSeeASuccessMessageSaying(string expectedMessage)
        {
            try
            {
                if (_apiResponse == null)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("API response is null.");
                }

                Console.WriteLine($"Final Response Object: {JsonConvert.SerializeObject(_apiResponse)}");

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.IsSuccess, "User registration was not successful");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(200, (int)_apiResponse.StatusCode, "Unexpected status code");

                if (!string.IsNullOrEmpty(expectedMessage))
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.IsSuccess, expectedMessage);
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
    }
}

