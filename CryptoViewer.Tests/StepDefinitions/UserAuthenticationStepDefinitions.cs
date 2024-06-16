using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class UserAuthenticationStepDefinitions
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _jsonContent;

        public UserAuthenticationStepDefinitions()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:7077/");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3MTg1NDIyOTYsImV4cCI6MTcxOTE0NzA5NiwiaWF0IjoxNzE4NTQyMjk2fQ.epNLAp56DdHNld92WzcxMUm1nqmq9ywk8dg0Mog6nmk");
        }

        [Given("I have valid login credentials")]
        public void GivenIHaveValidLoginCredentials()
        {
            var loginModel = new
            {
                Username = "validUser",
                Password = "validPass123"
            };
            _jsonContent = JsonConvert.SerializeObject(loginModel);
        }

        [When("I send a POST request to /api/UsersAuth/login with the credentials")]
        public async Task WhenISendAPOSTRequestToApiUsersAuthLoginWithTheCredentials()
        {
            var content = new StringContent(_jsonContent, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/UsersAuth/login", content);
        }

        [Then("I should receive a successful login response with a token")]
        public async Task ThenIShouldReceiveASuccessfulLoginResponseWithAToken()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(response.IsSuccess);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(response.Result.Token);
        }

        [Given("I have invalid login credentials")]
        public void GivenIHaveInvalidLoginCredentials()
        {
            var loginModel = new
            {
                Username = "invalidUser",
                Password = "invalidPass123"
            };
            _jsonContent = JsonConvert.SerializeObject(loginModel);
        }

        [Then("I should receive an error response indicating incorrect credentials")]
        public async Task ThenIShouldReceiveAnErrorResponseIndicatingIncorrectCredentials()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(response.IsSuccess);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Username or password is incorrect", response.ErrorMessages[0]);
        }

        [Given("I have unique registration details")]
        public void GivenIHaveUniqueRegistrationDetails()
        {
            var registerModel = new
            {
                Username = "newUser",
                Password = "newPass123",
                Email = "newuser@mail.com"
            };
            _jsonContent = JsonConvert.SerializeObject(registerModel);
        }

        [When("I send a POST request to /api/UsersAuth/register with the details")]
        public async Task WhenISendAPOSTRequestToApiUsersAuthRegisterWithTheDetails()
        {
            var content = new StringContent(_jsonContent, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/UsersAuth/register", content);
        }

        [Then("I should receive a successful registration response")]
        public void ThenIShouldReceiveASuccessfulRegistrationResponse()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = _response.Content.ReadAsStringAsync().Result;
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(response.IsSuccess);
        }

        [Given("I have registration details with an existing username")]
        public void GivenIHaveRegistrationDetailsWithAnExistingUsername()
        {
            var registerModel = new
            {
                Username = "existingUser",
                Password = "pass123",
                Email = "existing@mail.com"
            };
            _jsonContent = JsonConvert.SerializeObject(registerModel);
        }

        [Then("I should receive an error response indicating the username already exists")]
        public void ThenIShouldReceiveAnErrorResponseIndicatingTheUsernameAlreadyExists()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            var content = _response.Content.ReadAsStringAsync().Result;
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(response.IsSuccess);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Username already exists", response.ErrorMessages[0]);
        }
    }
}
