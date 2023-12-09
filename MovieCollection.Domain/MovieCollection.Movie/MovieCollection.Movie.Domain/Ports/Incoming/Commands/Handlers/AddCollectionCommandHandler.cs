using MovieCollection.Movie.Domain.DTOs;
using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.Movie.Domain.Ports.OutGoing;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers
{
    public class AddCollectionCommandHandler : ICommandHandler<AddCollectionCommand, AddCollectionResult>
    {
        private IMoviePersistence _moviePersistence;
        public AddCollectionCommandHandler(IMoviePersistence moviePersistence)
        {
            _moviePersistence = moviePersistence;
        }

        public async Task<AddCollectionResult> Handle(AddCollectionCommand command)
        {
            var addMovieCollectionResult = new AddCollectionResult();
            var collection = await _moviePersistence.GetCollectionByUserAsync(command.UserId);
            if (collection != null)
                return addMovieCollectionResult.MoveCollectionAlreadyExistError();

            var newCollection = new Collection(command.Title, command.UserId);
            await _moviePersistence.AddCollectionAsync(newCollection);

            var newCollectionMovies = command.MovieIds.Select(m => new Entities.MovieCollection(m, newCollection.Id)).ToList();
            await _moviePersistence.AddMoviesToCollectionAsync(newCollectionMovies);

            var movies = await _moviePersistence.GetAllMoviesByCollectionIdAsync(newCollection.Id);

            return addMovieCollectionResult.Success(new MovieCollectionEntityDto(newCollection.Id, newCollection.Title, newCollection.UserId
                , movies?.Select(m => new MovieEntityDto(m.Id, m.Title, m.Description, m.ThumbnailUrl, m.Language)).ToList()));
        }
    }
}
