using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Enums;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using NUnit.Framework;

namespace MovieCollection.Movie.Test.Port.Incoming.Commands
{
    [TestFixture]
    public class AddMovieToCollectionCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShoulAdddMovieToCollection()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var collection = new Collection(1, "Test Collection", user.Id);
            var addMovieToCollectionCommand = new AddMovieToCollectionCommand(user.Id, movie.Id);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.Run(() => collection));
            moviePersistenceMock.Setup(m => m.GetMovieAsync(movie.Id)).Returns(Task.Run(() => movie));
            moviePersistenceMock.Setup(m => m.AddMovieToCollectionAsync(collection.Id, addMovieToCollectionCommand.MovieId)).Returns(Task.Run(() => true));

            // Act
            var addMovieResult = commandDispatcher.Dispatch<AddMovieToCollectionCommand, AddMovieToCollectionResult>(addMovieToCollectionCommand).Result;

            // Assert
            Assert.That(addMovieResult.IsSuccess, Is.True);
        }

        [Test]
        public async Task Dispatch_WhenUserDoesNotHaveCollection_ShouldRaiseCollectionNotFoundError()
        {
            // Arrange
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var movie = new Domain.Entities.Movie("Movie 1", "Movie descrip", string.Empty, "en-US");
            var addMovieToCollectionCommand = new AddMovieToCollectionCommand(user.Id, movie.Id);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.FromResult((Collection)null));

            // Act
            var addMovieResult = commandDispatcher.Dispatch<AddMovieToCollectionCommand, AddMovieToCollectionResult>(addMovieToCollectionCommand).Result;

            // Assert
            Assert.That(addMovieResult.CollectionNotFound, Is.True);
        }


        [Test]
        public async Task Dispatch_WhenInvalidMoveFound_ShouldRaiseMovieNotFoundError()
        {
            // Arrange
            var movieId = 1;
            var user = new User(1, "thu@gmail.com", "abcD@123", "Thushan", "Manujith", UserRole.User, new DateTime(2024, 01, 01));
            var collection = new Collection(1, "Test Collection", user.Id);
            var addMovieToCollectionCommand = new AddMovieToCollectionCommand(user.Id, movieId);

            moviePersistenceMock.Setup(m => m.GetCollectionByUserAsync(1)).Returns(Task.Run(() => collection));
            moviePersistenceMock.Setup(m => m.GetMovieAsync(movieId)).Returns(Task.FromResult((Domain.Entities.Movie)null));

            // Act
            var addMovieResult = commandDispatcher.Dispatch<AddMovieToCollectionCommand, AddMovieToCollectionResult>(addMovieToCollectionCommand).Result;

            // Assert
            Assert.That(addMovieResult.MovieNotFound, Is.True);
        }
    }
}
