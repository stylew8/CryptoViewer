using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.Exceptions;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.Tests.StepDefinitions
{
    [Binding]
    public class RegistrationSuccessCheckStepDefinitions
    {
        private readonly IAuth _auth;
        private UserModel _user;
        private UserDetails _userDetails;
        private Exception? _exception;

        public RegistrationSuccessCheckStepDefinitions(IAuth auth)
        {
            _auth = auth;
            _user = new UserModel();
            _userDetails = new UserDetails();
        }

        [Given(@"a new user with username ""([^""]*)"" and email ""([^""]*)""")]
        public void GivenANewUserWithUsernameAndEmail(string username, string email)
        {
            _user.Username = username;
            _userDetails.Email = email;
            _userDetails.FirstName = "Test";
            _userDetails.LastName = "User";
        }

        [When(@"the user registers with password ""([^""]*)""")]
        public async Task WhenTheUserRegistersWithPassword(string password)
        {
            _user.Password = password;

            try
            {
                await _auth.Register(_user, _userDetails);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"the user should be successfully registered")]
        public void ThenTheUserShouldBeSuccessfullyRegistered()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(_exception);
        }

        [Then(@"a duplicate email error should be thrown")]
        public void ThenADuplicateEmailErrorShouldBeThrown()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(_exception, typeof(DuplicateEmailException));
        }

        [Then(@"a duplicate username error should be thrown")]
        public void ThenADuplicateUsernameErrorShouldBeThrown()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(_exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(_exception, typeof(DuplicateUsernameException));
        }
    }
}
