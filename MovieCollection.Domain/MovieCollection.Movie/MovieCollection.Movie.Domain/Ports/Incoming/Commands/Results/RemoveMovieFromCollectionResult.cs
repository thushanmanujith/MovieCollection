namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results
{
    public class RemoveMovieFromCollectionResult
    {
        public bool CollectionNotFound { get; private set; }
        public bool IsSuccess { get; private set; }
        public bool MoveNotFound { get; private set; }

        public RemoveMovieFromCollectionResult MoveNotFoundError() => new RemoveMovieFromCollectionResult { MoveNotFound = true };
        public RemoveMovieFromCollectionResult CollectionNotFoundError() => new RemoveMovieFromCollectionResult { CollectionNotFound = true };
        public RemoveMovieFromCollectionResult Success() => new RemoveMovieFromCollectionResult { IsSuccess = true };
    }
}
