using MovieCollection.Movie.Domain.DTOs;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Queries
{
    public interface IMovieQueries
    {
        Task<List<MovieEntityDto>> GetAllMoviesAsync();
        Task<MovieCollectionEntityDto> GetMoveCollectionByUserAsync(int userId);
        Task<List<MovieEntityDto>> SearchMoviesByTitleAsync(string title, int collectionId);
    }
}
