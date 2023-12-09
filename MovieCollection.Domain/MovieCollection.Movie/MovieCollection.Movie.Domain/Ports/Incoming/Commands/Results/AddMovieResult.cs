using MovieCollection.Movie.Domain.DTOs;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results
{
    public class AddMovieResult
    {
        public bool MoveAlreadyExist { get; private set; }
        public bool IsSuccess { get; private set; }
        public MovieEntityDto Movie { get; private set; }

        public AddMovieResult MoveAlreadyExistError() => new AddMovieResult { MoveAlreadyExist = true };
        public AddMovieResult Success(MovieEntityDto movie) => new AddMovieResult { IsSuccess = true, Movie = movie };
    }
}
