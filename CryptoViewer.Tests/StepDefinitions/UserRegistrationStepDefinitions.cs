using System;
using System.Linq;
using System.Net;
using CryptoViewer.API.Models;
using CryptoViewer.MVC.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using CryptoViewer.MVC.Models.Dto;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UserRegistrationStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private string _responseContent;

        public UserRegistrationStepDefinitions()
        {
            var httpClient = new HttpClient();
            _apiHelper = new ApiHelper(httpClient);
        }

        [Given(@"no user with username ""([^""]*)"" exists")]
        public async Task GivenNoUserWithUsernameExists(string username)
        {
            try
            {
                // Check if user with given username exists
                var getUserResponse = await _apiHelper.GetAsync<UserDto>($"api/Users/{username}");

                // If user exists, throw an exception
                if (getUserResponse != null)
                {
                    throw new InvalidOperationException($"User with username '{username}' already exists.");
                }
            }
            catch (Exception ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Failed to check user existence for username '{username}'. Error: {ex.Message}");
            }
        }

        [Given(@"no user with email ""([^""]*)"" exists")]
        public async Task GivenNoUserWithEmailExists(string email)
        {
            try
            {
                // Check if user with given email exists
                var getUsersResponse = await _apiHelper.GetAsync<List<UserDto>>($"api/Users?email={email}");

                // If any user with the same email exists, throw an exception
                if (getUsersResponse != null && getUsersResponse.Any())
                {
                    throw new InvalidOperationException($"User with email '{email}' already exists.");
                }
            }
            catch (Exception ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Failed to check user existence for email '{email}'. Error: {ex.Message}");
            }
        }

        [When(@"the admin attempts to register a user with the following details:")]
        public async Task WhenTheAdminAttemptsToRegisterAUserWithTheFollowingDetails(Table table)
        {
            try
            {
                var registerData = table.CreateInstance<RegisterModel>();

                // Make API call to register user
                _response = await _apiHelper.PostAsync<HttpResponseMessage>("api/Auth/register", registerData);

                // Read response content
                _responseContent = await _response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Failed to register user. Error: {ex.Message}");
            }
        }

        [Then(@"the response status should be (.*)")]
        public void ThenTheResponseStatusShouldBe(int expectedStatusCode)
        {
            HttpStatusCode actualStatusCode = _response.StatusCode;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedStatusCode, (int)actualStatusCode,
                $"Expected status code {expectedStatusCode}, but got {(int)actualStatusCode}.\nResponse content: {_responseContent}");
        }

        [Then(@"the response should contain a user ID")]
        public void ThenTheResponseShouldContainAUserId()
        {
            try
            {
                var json = JObject.Parse(_responseContent);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(json.ContainsKey("UserId"), $"Response does not contain a user ID. Response content: {_responseContent}");
            }
            catch (Exception ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Failed to parse response content. Error: {ex.Message}");
            }
        }

        [Then(@"the response should contain ""([^""]*)""")]
        public void ThenTheResponseShouldContain(string expectedMessage)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_responseContent.Contains(expectedMessage),
                $"Response does not contain expected message: '{expectedMessage}'. Response content: {_responseContent}");
        }
    }
}
