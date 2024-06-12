using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class UpdateUserContactInformationStepDefinitions
    {
        [Given(@"User is logged in and is on the ""([^""]*)"" page")]
        public void GivenUserIsLoggedInAndIsOnThePage(string profile)
        {
            throw new PendingStepException();
        }

        [When(@"User updates the ""([^""]*)"" field to ""([^""]*)""")]
        public void WhenUserUpdatesTheFieldTo(string email, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"User's contact information should be updated")]
        public void ThenUsersContactInformationShouldBeUpdated()
        {
            throw new PendingStepException();
        }

        [Then(@"the updated contact information should be displayed")]
        public void ThenTheUpdatedContactInformationShouldBeDisplayed()
        {
            throw new PendingStepException();
        }
    }
}
