using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using CryptoViewer.MVC.Helpers;
using System.Net;
using Newtonsoft.Json;
using CryptoViewer.Auth_API.Models;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UserRegistrationStepDefinitions
    {
        private readonly ApiHelper _apiHelper;
        private HttpResponseMessage _response;
        private string _responseContent;
        private APIResponse _apiResponse;
        private object _requestBody;

        public UserRegistrationStepDefinitions()
        {
            var httpClient = new HttpClient();
            _apiHelper = new ApiHelper(httpClient);
        }

        [Given("I have a valid registration request")]
        public void GivenIHaveAValidRegistrationRequest()
        {
            _requestBody = new
            {
                Username = "testuser",
                Password = "Test@1234",
                Email = "test@test.com",
                Address = "123 Test St",
                FirstName = "Test",
                LastName = "User"
            };
        }

        [Given("I have a registration request with an existing email")]
        public void GivenIHaveARegistrationRequestWithAnExistingEmail()
        {
            _requestBody = new
            {
                Username = "newuser",
                Password = "New@1234",
                Email = "test@test.com",
                Address = "456 New St",
                FirstName = "New",
                LastName = "User"
            };
        }

        [Given("I have a registration request with an existing username")]
        public void GivenIHaveARegistrationRequestWithAnExistingUsername()
        {
            _requestBody = new
            {
                Username = "testuser",
                Password = "New@1234",
                Email = "new@test.com",
                Address = "789 New St",
                FirstName = "New",
                LastName = "User"
            };
        }

        [Given("I have a registration request that causes UserDetails creation failure")]
        public void GivenIHaveARegistrationRequestThatCausesUserDetailsCreationFailure()
        {
            _requestBody = new
            {
                Username = "validuser",
                Password = "Valid@123",
                Email = "valid@test.com",
                Address = "123 Valid St",
                FirstName = "Valid",
                LastName = "User"
            };
        }

        [Given("I have a registration request that causes User creation failure")]
        public void GivenIHaveARegistrationRequestThatCausesUserCreationFailure()
        {
            _requestBody = new
            {
                Username = "validuser2",
                Password = "Valid@123",
                Email = "valid2@test.com",
                Address = "123 Valid St",
                FirstName = "Valid",
                LastName = "User"
            };
        }

        [Given("I have a registration request that causes an unknown error")]
        public void GivenIHaveARegistrationRequestThatCausesAnUnknownError()
        {
            _requestBody = new
            {
                Username = "validuser3",
                Password = "Valid@123",
                Email = "valid3@test.com",
                Address = "123 Valid St",
                FirstName = "Valid",
                LastName = "User"
            };
        }

        [When("I send a POST request to \"([^\"]*)\" with the following details")]
        public async Task WhenISendAPOSTRequestToWithTheFollowingDetails(string endpoint, Table table)
        {
            _requestBody = table.CreateInstance<dynamic>();
            try
            {
                _response = await _apiHelper.PostAsync<HttpResponseMessage>(endpoint, _requestBody);
                _responseContent = await _response.Content.ReadAsStringAsync();
                System.Console.WriteLine($"Response content: {_responseContent}");
                _apiResponse = JsonConvert.DeserializeObject<APIResponse>(_responseContent);
            }
            catch (HttpRequestException ex)
            {
                _response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                _responseContent = ex.Message;
                System.Console.WriteLine($"HttpRequestException: {_responseContent}");
                _apiResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message },
                    Result = null
                };
            }
        }

        [Then("the registration response status should be (.*)")]
        public void ThenTheRegistrationResponseStatusShouldBe(int expectedStatusCode)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_response, "Response is null.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode,
                $"Expected status code {expectedStatusCode}, but got {(int)_response.StatusCode}.\nResponse content: {_responseContent}");
        }

        [Then("the response should be contain a user ID")]
        public void ThenTheResponseShouldBeContainAUserID()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_apiResponse, "API Response is null.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.IsSuccess, "API response indicates failure.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_apiResponse.Result, "Result is null in API response.");
            var json = JObject.FromObject(_apiResponse.Result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(json.ContainsKey("userId"), $"Response content: {_responseContent}");
        }

        [Then("the response should contain an error message \"([^\"]*)\"")]
        public void ThenTheResponseShouldContainAnErrorMessage(string expectedErrorMessage)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_apiResponse, "API Response is null.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(_apiResponse.IsSuccess, "API response indicates success.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_apiResponse.ErrorMessages, "ErrorMessages is null in API response.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(_apiResponse.ErrorMessages.Contains(expectedErrorMessage),
                $"Response does not contain expected error message: {expectedErrorMessage}. Error messages: {string.Join(", ", _apiResponse.ErrorMessages)}");
        }
    }
}
