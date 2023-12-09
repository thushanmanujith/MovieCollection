using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.Movie.Domain.Ports.OutGoing;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers
{
    public class RemoveMovieFromCollectionCommandHandler : ICommandHandler<RemoveMovieFromCollectionCommand, RemoveMovieFromCollectionResult>
    {
        private IMoviePersistence _moviePersistence;
        public RemoveMovieFromCollectionCommandHandler(IMoviePersistence moviePersistence)
        {
            _moviePersistence = moviePersistence;
        }

        public async Task<RemoveMovieFromCollectionResult> Handle(RemoveMovieFromCollectionCommand command)
        {
            var removeMovieResult = new RemoveMovieFromCollectionResult();

            var collection = await _moviePersistence.GetCollectionByUserAsync(command.UserId);
            if (collection == null)
            {
                return removeMovieResult.CollectionNotFoundError();
            }

            var movie = await _moviePersistence.GetMovieAsync(command.MovieId);
            if (movie == null)
            {
                return removeMovieResult.MoveNotFoundError();
            }

            await _moviePersistence.RemoveMovieFromCollectionAsync(collection.Id, movie.Id);
            return removeMovieResult.Success();
        }
    }
}
