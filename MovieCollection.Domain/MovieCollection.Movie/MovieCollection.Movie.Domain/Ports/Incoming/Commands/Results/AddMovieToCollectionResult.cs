namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results
{
    public class AddMovieToCollectionResult
    {
        public bool CollectionNotFound { get; private set; }
        public bool MovieNotFound { get; private set; }
        public bool IsSuccess { get; private set; }

        public AddMovieToCollectionResult CollectionNotFoundError() => new AddMovieToCollectionResult { CollectionNotFound = true };
        public AddMovieToCollectionResult MovieNotFoundError() => new AddMovieToCollectionResult { MovieNotFound = true };
        public AddMovieToCollectionResult Success() => new AddMovieToCollectionResult { IsSuccess = true };
    }
}
