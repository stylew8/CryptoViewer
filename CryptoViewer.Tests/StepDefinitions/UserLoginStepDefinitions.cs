using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using CryptoViewer.MVC.Helpers;
using System.Net;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UserLoginStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private string _responseContent;

        public UserLoginStepDefinitions()
        {
            var httpClient = new HttpClient();
            _apiHelper = new ApiHelper(httpClient);
        }

        [Given(@"a user with username ""([^""]*)"" and password ""([^""]*)"" exists")]
        public async Task GivenAUserWithUsernameAndPasswordExists(string username, string password)
        {
            var registerUrl = "api/auth/register";
            var registerData = new
            {
                Username = username,
                Password = password,
                Email = $"{username}@example.com",
                Address = "123 Main St",
                FirstName = "Test",
                LastName = "User"
            };

            try
            {
                var response = await _apiHelper.PostAsync<HttpResponseMessage>(registerUrl, registerData);
                if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                if (!ex.Message.Contains("400"))
                {
                    throw;
                }
            }
        }

        [When(@"the user attempts to log in with username ""([^""]*)"" and password ""([^""]*)""")]
        public async Task WhenTheUserAttemptsToLogInWithUsernameAndPassword(string username, string password)
        {
            var loginUrl = "api/Auth/login";
            var loginData = new
            {
                Username = username,
                Password = password
            };

            try
            {
                _response = await _apiHelper.PostAsync<HttpResponseMessage>(loginUrl, loginData);
                _responseContent = await _response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                _responseContent = ex.Message;
            }
        }

        [Then(@"the response status should be (.*)")]
        public void ThenTheResponseStatusShouldBe(int statusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(statusCode, (int)_response.StatusCode, 
                $"Expected status code {statusCode}, but got {(int)_response.StatusCode}.\nResponse content: {_responseContent}");
        }

        [Then(@"the response should contain a user ID")]
        public void ThenTheResponseShouldContainAUserID()
        {
            var json = JObject.Parse(_responseContent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(json.ContainsKey("UserId"), $"Response does not contain a user ID. Response content: {_responseContent}");
        }

        [Then(@"the response should contain ""([^""]*)""")]
        public void ThenTheResponseShouldContain(string message)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_responseContent.Contains(message),
                $"Response does not contain expected message: {message}. Response content: {_responseContent}");
        }
    }
}
