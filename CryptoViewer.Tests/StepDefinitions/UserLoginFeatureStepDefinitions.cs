using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using SeleniumExtras.WaitHelpers;

namespace YourNamespace.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [BeforeScenario]
        public void Setup()
        {
            var options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [AfterScenario]
        public void TearDown()
        {
            _driver?.Quit();
        }

        [When(@"User enters valid username and password")]
        public void WhenUserEntersValidUsernameAndPassword()
        {
            _driver.Navigate().GoToUrl("https://localhost:7262/login"); // Adjust the URL to your actual login page

            // Wait for the username field to be present
            var usernameField = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
            usernameField.SendKeys("validUsername"); // Replace with a valid username

            // Wait for the password field to be present
            var passwordField = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys("validPassword"); // Replace with a valid password
        }

        [When(@"User clicks on the login button")]
        public void WhenUserClicksOnTheLoginButton()
        {
            // Wait for the login button to be clickable
            var loginButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".form-button input[type='submit']")));
            loginButton.Click();
        }

        [Then(@"User should be redirected to the home page")]
        public void ThenUserShouldBeRedirectedToTheDashboard()
        {
            // Wait for the URL to change to the home page
            _wait.Until(ExpectedConditions.UrlContains("/Home"));
            Assert.IsTrue(_driver.Url.Contains("/Home")); // Adjust the URL to your actual dashboard page
        }
    }
}
