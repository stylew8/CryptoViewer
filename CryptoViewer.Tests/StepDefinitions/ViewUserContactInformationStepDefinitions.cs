using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class ViewUserContactInformationStepDefinitions
    {
        [When(@"User navigates to the ""([^""]*)"" page")]
        public void WhenUserNavigatesToThePage(string profile)
        {
            throw new PendingStepException();
        }

        [Then(@"User's contact information should be displayed")]
        public void ThenUsersContactInformationShouldBeDisplayed()
        {
            throw new PendingStepException();
        }

        [Then(@"the displayed contact information should include ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void ThenTheDisplayedContactInformationShouldIncludeAnd(string email, string p1, string username)
        {
            throw new PendingStepException();
        }
    }
}
