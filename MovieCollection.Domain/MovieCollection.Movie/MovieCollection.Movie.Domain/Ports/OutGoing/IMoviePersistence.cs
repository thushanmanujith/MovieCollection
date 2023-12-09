using MovieCollection.Movie.Domain.Entities;

namespace MovieCollection.Movie.Domain.Ports.OutGoing
{
    public interface IMoviePersistence
    {
        Task<Entities.Movie> GetMovieAsync(int id);
        Task<Entities.Movie> GetMovieAsync(string title);
        Task<Entities.Movie> AddUpdateMovieAsync(Entities.Movie movie);
        Task<Collection> AddCollectionAsync(Collection collection);
        Task<bool> AddMoviesToCollectionAsync(List<Entities.MovieCollection> movieCollections);
        Task<bool> AddMovieToCollectionAsync(int collectionId, int moveId);
        Task<bool> RemoveMovieFromCollectionAsync(int collectionId, int moveId);
        Task<Collection> GetCollectionByUserAsync(int userId);
        Task<List<Entities.Movie>> GetAllMoviesAsync();
        Task<List<Entities.Movie>> GetAllMoviesByCollectionIdAsync(int collectionId);
        Task<List<Entities.Movie>> SearchMoviesByTitle(string title, int collectionId);
    }
}
