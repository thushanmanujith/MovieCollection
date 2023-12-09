using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.Movie.Domain.Ports.OutGoing;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers
{
    public class AddMovieToCollectionCommandHandler : ICommandHandler<AddMovieToCollectionCommand, AddMovieToCollectionResult>
    {
        private IMoviePersistence _moviePersistence;
        public AddMovieToCollectionCommandHandler(IMoviePersistence moviePersistence)
        {
            _moviePersistence = moviePersistence;
        }

        public async Task<AddMovieToCollectionResult> Handle(AddMovieToCollectionCommand command)
        {
            var addMovieResult = new AddMovieToCollectionResult();

            var collection = await _moviePersistence.GetCollectionByUserAsync(command.UserId);
            if (collection == null)
            {
                return addMovieResult.CollectionNotFoundError();
            }

            var movie = await _moviePersistence.GetMovieAsync(command.MovieId);
            if (movie == null)
                return addMovieResult.MovieNotFoundError();

            await _moviePersistence.AddMovieToCollectionAsync(collection.Id, movie.Id);
            return addMovieResult.Success();
        }
    }
}
