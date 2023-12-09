namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands
{
    public class AddMovieToCollectionCommand : ICommand
    {
        public AddMovieToCollectionCommand(int userId, int movieId)
        {
            UserId = userId;
            MovieId = movieId;
        }

        public int UserId { get; }
        public int MovieId { get; }
    }
}
