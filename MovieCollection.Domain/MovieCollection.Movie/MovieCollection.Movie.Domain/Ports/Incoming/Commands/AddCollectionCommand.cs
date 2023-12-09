using MovieCollection.Movie.Domain.DTOs;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands
{
    public class AddCollectionCommand : ICommand
    {
        public AddCollectionCommand(string title, int userId, List<int> movieIds)
        { 
            Title = title;
            UserId = userId;
            MovieIds = movieIds;
        }
        
        public string Title { get; }
        public int UserId { get; }
        public List<int> MovieIds { get; }
    }
}
