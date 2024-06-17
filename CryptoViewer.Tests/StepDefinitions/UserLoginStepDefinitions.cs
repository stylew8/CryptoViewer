using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.MVC.Helpers;

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
            var registerUrl = "api/Auth/register";
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
            var loginDataJson = JsonConvert.SerializeObject(new
            {
                Username = username,
                Password = password
            });

            try
            {
                var content = new StringContent(loginDataJson, Encoding.UTF8, "application/json");
                _response = await _apiHelper.PostAsync<HttpResponseMessage>(loginUrl, content);
                _responseContent = await _response.Content.ReadAsStringAsync();

                System.Console.WriteLine($"Status Code: {(int)_response.StatusCode}");
                System.Console.WriteLine($"Response Content: {_responseContent}");
            }
            catch (HttpRequestException ex)
            {

                System.Console.WriteLine($"HttpRequestException: {ex.Message}");
                throw;
            }
        }

        [Then(@"the response status should be (.*)")]
        public void ThenTheResponseStatusShouldBe(int statusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_response, "Response is null.");
            if ((int)_response.StatusCode != statusCode)
            {
                System.Console.WriteLine($"Actual status code: {(int)_response.StatusCode}, Response content: {_responseContent}");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected status code {statusCode}, but got {(int)_response.StatusCode}.");
            }
        }


        [Then(@"the response should contain a user ID")]
        public void ThenTheResponseShouldContainAUserID()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_responseContent, "Response content is null.");
            System.Console.WriteLine($"Parsing response content: {_responseContent}");

            if (string.IsNullOrEmpty(_responseContent))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Response content is empty.");
            }

            try
            {
                var json = JObject.Parse(_responseContent);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(json.ContainsKey("UserId"), $"Response does not contain a user ID. Response content: {_responseContent}");
            }
            catch (JsonReaderException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Error parsing response content: {_responseContent}. Exception: {ex.Message}");
            }
        }

        [Then(@"the response should contain ""([^""]*)""")]
        public void ThenTheResponseShouldContain(string message)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_responseContent, "Response content is null.");
            if (string.IsNullOrEmpty(_responseContent))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Response content is empty.");
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_responseContent.Contains(message),
                $"Response does not contain expected message: {message}. Response content: {_responseContent}");
        }
    }
}
