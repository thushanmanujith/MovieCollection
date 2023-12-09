using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Enums;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using NUnit.Framework;

namespace MovieCollection.Movie.Test.Port.Incoming.Commands
{
    [TestFixture]
    public class AddCollectionCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShoulAddCollection()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User);
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var movieIds = new List<int> { movie.Id };
            var addCollectionCommand = new AddCollectionCommand("My Collection", user.Id, movieIds);
            var newCollection = new Collection(addCollectionCommand.Title, addCollectionCommand.UserId);
            var newCollectionMovies = movieIds.Select(m => new Domain.Entities.MovieCollection(m, newCollection.Id)).ToList();

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.FromResult((Collection)null));
            moviePersistenceMock.Setup(m => m.AddCollectionAsync(newCollection)).Returns(Task.Run(() => newCollection));
            moviePersistenceMock.Setup(m => m.AddMoviesToCollectionAsync(newCollectionMovies)).Returns(Task.Run(() => true));
            moviePersistenceMock.Setup(m => m.GetAllMoviesByCollectionIdAsync(newCollection.Id)).Returns(Task.Run(() => new List<Domain.Entities.Movie> { movie }));

            // Act
            var addCollectionResult = commandDispatcher.Dispatch<AddCollectionCommand, AddCollectionResult>(addCollectionCommand).Result;

            // Assert
            Assert.AreEqual(addCollectionResult.IsSuccess, true);
        }

        [Test]
        public async Task Dispatch_WhenUserCollectionAlreadyExsist_ShouldRaiseMoveCollectionAlreadyExistError()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User);
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var movieIds = new List<int> { movie.Id };
            var addCollectionCommand = new AddCollectionCommand("My Collection", user.Id, movieIds);
            var collection = new Collection(1, "Test Collection", user.Id);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.Run(() => collection));

            // Act
            var addCollectionResult = commandDispatcher.Dispatch<AddCollectionCommand, AddCollectionResult>(addCollectionCommand).Result;

            // Assert
            Assert.AreEqual(addCollectionResult.MoveCollectionAlreadyExist, true);
        }
    }
}
