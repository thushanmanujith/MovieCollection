using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Enums;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using NUnit.Framework;

namespace MovieCollection.Movie.Test.Port.Incoming.Commands
{
    [TestFixture]
    public class RemoveMovieFromCollectionCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShoulRemoveMovieFromCollection()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var collection = new Collection(1, "Test Collection", user.Id);
            var removeMovieFromCollectionCommand = new RemoveMovieFromCollectionCommand(user.Id, movie.Id);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.Run(() => collection));
            moviePersistenceMock.Setup(m => m.GetMovieAsync(movie.Id)).Returns(Task.Run(() => movie));
            moviePersistenceMock.Setup(m => m.AddMovieToCollectionAsync(collection.Id, removeMovieFromCollectionCommand.MovieId)).Returns(Task.Run(() => true));

            // Act
            var removeMovieFromCollectionResult = commandDispatcher.Dispatch<RemoveMovieFromCollectionCommand, RemoveMovieFromCollectionResult>(removeMovieFromCollectionCommand).Result;

            // Assert
            Assert.That(removeMovieFromCollectionResult.IsSuccess, Is.True);
        }

        [Test]
        public async Task Dispatch_WhenUserDoesNotHaveCollection_ShouldRaiseCollectionNotFoundError()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var removeMovieFromCollectionCommand = new RemoveMovieFromCollectionCommand(user.Id, movie.Id);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.FromResult((Collection)null));

            // Act
            var removeMovieFromCollectionResult = commandDispatcher.Dispatch<RemoveMovieFromCollectionCommand, RemoveMovieFromCollectionResult>(removeMovieFromCollectionCommand).Result;

            // Assert
            Assert.That(removeMovieFromCollectionResult.CollectionNotFound, Is.True);
        }


        [Test]
        public async Task Dispatch_WhenInvalidMoveFound_ShouldRaiseMovieNotFoundError()
        {
            // Arrange
            var movieId = 1;
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var collection = new Collection(1, "Test Collection", user.Id);
            var removeMovieFromCollectionCommand = new RemoveMovieFromCollectionCommand(user.Id, movieId);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.Run(() => collection));
            moviePersistenceMock.Setup(m => m.GetMovieAsync(movieId)).Returns(Task.FromResult((Domain.Entities.Movie)null));

            // Act
            var removeMovieFromCollectionResult = commandDispatcher.Dispatch<RemoveMovieFromCollectionCommand, RemoveMovieFromCollectionResult>(removeMovieFromCollectionCommand).Result;

            // Assert
            Assert.That(removeMovieFromCollectionResult.MoveNotFound, Is.True);
        }
    }
}
