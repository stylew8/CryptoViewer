using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class GuestAccessToLoginPageStepDefinitions
    {
        [When(@"I navigate to the Login page")]
        public void WhenINavigateToTheLoginPage()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see the Login page content")]
        public void ThenIShouldSeeTheLoginPageContent()
        {
            throw new PendingStepException();
        }

        [Then(@"the Login page should display fields for ""([^""]*)"" and ""([^""]*)""")]
        public void ThenTheLoginPageShouldDisplayFieldsForAnd(string username, string password)
        {
            throw new PendingStepException();
        }

        [Then(@"the Login page should include a ""([^""]*)"" button")]
        public void ThenTheLoginPageShouldIncludeAButton(string login)
        {
            throw new PendingStepException();
        }
    }
}
