namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands
{
    public class RemoveMovieFromCollectionCommand : ICommand
    {
        public RemoveMovieFromCollectionCommand(int userId, int movieId)
        {
            UserId = userId;
            MovieId = movieId;
        }

        public int UserId { get; set; }
        public int MovieId { get; }
    }
}
