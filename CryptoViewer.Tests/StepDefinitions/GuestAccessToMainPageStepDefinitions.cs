using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class GuestAccessToMainPageStepDefinitions
    {
        [Given(@"I am a guest")]
        public void GivenIAmAGuest()
        {
            throw new PendingStepException();
        }

        [When(@"I navigate to the main page")]
        public void WhenINavigateToTheMainPage()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see the main page content")]
        public void ThenIShouldSeeTheMainPageContent()
        {
            throw new PendingStepException();
        }

        [Then(@"the main page should display general information about the site")]
        public void ThenTheMainPageShouldDisplayGeneralInformationAboutTheSite()
        {
            throw new PendingStepException();
        }

        [Then(@"the main page should include a ""([^""]*)"" button")]
        public void ThenTheMainPageShouldIncludeAButton(string login)
        {
            throw new PendingStepException();
        }
    }
}
