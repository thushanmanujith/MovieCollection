using MovieCollection.Movie.Domain.DTOs;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results
{
    public class AddCollectionResult
    {
        public bool MoveCollectionAlreadyExist { get; private set; }
        public bool IsSuccess { get; private set; }
        public MovieCollectionEntityDto MovieCollection { get; private set; }

        public AddCollectionResult MoveCollectionAlreadyExistError() => new AddCollectionResult { MoveCollectionAlreadyExist = true };
        public AddCollectionResult Success(MovieCollectionEntityDto movieCollection) => new AddCollectionResult { IsSuccess = true, MovieCollection = movieCollection };
    }
}
