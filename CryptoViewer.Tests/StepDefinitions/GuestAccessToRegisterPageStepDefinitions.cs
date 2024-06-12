using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class GuestAccessToRegisterPageStepDefinitions
    {
        [When(@"I navigate to the register page")]
        public void WhenINavigateToTheRegisterPage()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see the register page content")]
        public void ThenIShouldSeeTheRegisterPageContent()
        {
            throw new PendingStepException();
        }

        [Then(@"the register page should display fields for ""([^""]*)"", ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void ThenTheRegisterPageShouldDisplayFieldsForAnd(string username, string email, string p2, string password)
        {
            throw new PendingStepException();
        }

        [Then(@"the register page should include a ""([^""]*)"" button")]
        public void ThenTheRegisterPageShouldIncludeAButton(string register)
        {
            throw new PendingStepException();
        }
    }
}
