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
    public class AuthenticationStepDefinitions
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _jsonContent;

        public AuthenticationStepDefinitions()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:7077/");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3MTg1NDIyOTYsImV4cCI6MTcxOTE0NzA5NiwiaWF0IjoxNzE4NTQyMjk2fQ.epNLAp56DdHNld92WzcxMUm1nqmq9ywk8dg0Mog6nmk");
        }

        [Given("I have valid credentials")]
        public void GivenIHaveValidCredentials()
        {
            var loginModel = new
            {
                Username = "validUser",
                Password = "validPass"
            };
            _jsonContent = JsonConvert.SerializeObject(loginModel);
        }

        [Given("I have invalid credentials")]
        public void GivenIHaveInvalidCredentials()
        {
            var loginModel = new
            {
                Username = "invalidUser",
                Password = "invalidPass"
            };
            _jsonContent = JsonConvert.SerializeObject(loginModel);
        }

        [When("I POST to /api/Auth/login with my credentials")]
        public async Task WhenIPOSTToApiAuthLoginWithMyCredentials()
        {
            var content = new StringContent(_jsonContent, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/Auth/login", content);
        }

        [Then("I should receive a successful response with my user ID")]
        public async Task ThenIShouldReceiveASuccessfulResponseWithMyUserID()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(response.UserId);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue((int)response.UserId > 0);
        }

        [Then("I should receive an invalid login attempt response")]
        public async Task ThenIShouldReceiveAnInvalidLoginAttemptResponse()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Invalid login attempt.", content);
        }

        [Given("I have valid registration details")]
        public void GivenIHaveValidRegistrationDetails()
        {
            var registerModel = new
            {
                Username = "newUser",
                Password = "newPass",
                Email = "newuser@mail.com",
                Address = "789 Road",
                FirstName = "Alice",
                LastName = "Johnson"
            };
            _jsonContent = JsonConvert.SerializeObject(registerModel);
        }

        [Given("I have invalid registration details")]
        public void GivenIHaveInvalidRegistrationDetails()
        {
            var registerModel = new
            {
                Username = "",
                Password = "",
                Email = "invalid@address.com",
                Address = "",
                FirstName = "Bob",
                LastName = ""
            };
            _jsonContent = JsonConvert.SerializeObject(registerModel);
        }

        [When("I POST to /api/Auth/register with my details")]
        public async Task WhenIPOSTToApiAuthRegisterWithMyDetails()
        {
            var content = new StringContent(_jsonContent, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/Auth/register", content);
        }

        [Then("I should receive a successful response with my new user ID")]
        public async Task ThenIShouldReceiveASuccessfulResponseWithMyNewUserID()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject(content);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(response.UserId);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue((int)response.UserId > 0);
        }

        [Then("I should receive a registration failure response")]
        public async Task ThenIShouldReceiveARegistrationFailureResponse()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            var content = await _response.Content.ReadAsStringAsync();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Registration failed.", content);
        }
    }
}
