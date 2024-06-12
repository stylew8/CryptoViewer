using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class UserLogoutFeatureStepDefinitions
    {
        [Given(@"User is logged in and is on the dashboard page")]
        public void GivenUserIsLoggedInAndIsOnTheDashboardPage()
        {
            throw new PendingStepException();
        }

        [When(@"User clicks on the ""([^""]*)"" button")]
        public void WhenUserClicksOnTheButton(string logout)
        {
            throw new PendingStepException();
        }

        [Then(@"User should be redirected to the login page")]
        public void ThenUserShouldBeRedirectedToTheLoginPage()
        {
            throw new PendingStepException();
        }

        [Then(@"a message ""([^""]*)"" should be displayed")]
        public void ThenAMessageShouldBeDisplayed(string p0)
        {
            throw new PendingStepException();
        }
    }
}
