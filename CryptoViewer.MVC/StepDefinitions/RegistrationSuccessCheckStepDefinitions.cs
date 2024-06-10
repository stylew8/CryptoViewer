using System;
using TechTalk.SpecFlow;

namespace CryptoViewer.MVC.StepDefinitions
{
    [Binding]
    public class RegistrationSuccessCheckStepDefinitions
    {
        [When(@"User enters information")]
        public void WhenUserEntersInformation()
        {
            throw new PendingStepException();
        }

        [When(@"User click button Register")]
        public void WhenUserClickButtonRegister()
        {
            throw new PendingStepException();
        }

        [Then(@"User should be registered to system")]
        public void ThenUserShouldBeRegisteredToSystem()
        {
            throw new PendingStepException();
        }

        [Then(@"Welcome message is displayed")]
        public void ThenWelcomeMessageIsDisplayed()
        {
            throw new PendingStepException();
        }
    }
}
