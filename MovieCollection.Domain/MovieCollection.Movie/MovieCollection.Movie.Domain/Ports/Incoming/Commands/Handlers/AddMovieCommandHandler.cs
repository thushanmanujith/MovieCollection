using MovieCollection.Movie.Domain.DTOs;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.Movie.Domain.Ports.OutGoing;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers
{
    public class AddMovieCommandHandler : ICommandHandler<AddMovieCommand, AddMovieResult>
    {
        private IMoviePersistence _moviePersistence;
        public AddMovieCommandHandler(IMoviePersistence moviePersistence)
        {
            _moviePersistence = moviePersistence;
        }

        public async Task<AddMovieResult> Handle(AddMovieCommand command)
        {
            var addMovieResult = new AddMovieResult();
            var movie = await _moviePersistence.GetMovieAsync(command.Title);
            if (movie != null)
                return addMovieResult.MoveAlreadyExistError();

            var newMove = new Domain.Entities.Movie(command.Title, command.Description, command.ThumbnailUrl, command.Language);
            var resultNewMove = await _moviePersistence.AddUpdateMovieAsync(newMove);

            return addMovieResult.Success(new MovieEntityDto(resultNewMove.Id, resultNewMove.Title, resultNewMove.Description, resultNewMove.ThumbnailUrl, resultNewMove.Language));
        }
    }
}
