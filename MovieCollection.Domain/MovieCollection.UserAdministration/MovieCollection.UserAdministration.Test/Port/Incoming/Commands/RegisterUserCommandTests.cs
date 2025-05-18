using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Enums;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.UserAdministration.Test;
using NUnit.Framework;

namespace MovieCollection.Movie.Test.Port.Incoming.Commands
{
    [TestFixture]
    public class RegisterUserCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShoulAdddMovieToCollection()
        {
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User);
            var registerUserCommand = new RegisterUserCommand(user.Email, "abcD@123", user.FirstName, user.LastName);

            userAdministrationPersistenceMock.Setup(m => m.AddUpdateUserAsync(user)).Returns(Task.Run(() => user));

            var registerUserResult = commandDispatcher.Dispatch<RegisterUserCommand, RegisterUserResult>(registerUserCommand).Result;

            Assert.That(registerUserResult.IsSuccess, Is.True);
        }
    }
}
