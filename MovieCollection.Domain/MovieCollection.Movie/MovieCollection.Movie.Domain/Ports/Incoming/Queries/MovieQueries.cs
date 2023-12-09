using MovieCollection.Movie.Domain.DTOs;
using MovieCollection.Movie.Domain.Ports.OutGoing;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Queries
{
    public class MovieQueries : IMovieQueries
    {
        private IMoviePersistence _moviePersistence;

        public MovieQueries(IMoviePersistence moviePersistence)
        {
            _moviePersistence = moviePersistence;
        }

        public async Task<List<MovieEntityDto>> GetAllMoviesAsync()
        {
            var movies = await _moviePersistence.GetAllMoviesAsync();
            return movies.Select(m => new MovieEntityDto(m.Id, m.Title, m.Description, m.ThumbnailUrl, m.Language)).ToList();
        }

        public async Task<List<MovieEntityDto>> SearchMoviesByTitleAsync(string title, int collectionId)
        {
            var movies = await _moviePersistence.SearchMoviesByTitle(title, collectionId);
            return movies.Select(m => new MovieEntityDto(m.Id, m.Title, m.Description, m.ThumbnailUrl, m.Language)).ToList();
        }

        public async Task<MovieCollectionEntityDto> GetMoveCollectionByUserAsync(int userId)
        {
            var collection = await _moviePersistence.GetCollectionByUserAsync(userId);
            if (collection == null)
                return new MovieCollectionEntityDto();

            var movies = await _moviePersistence.GetAllMoviesByCollectionIdAsync(collection.Id);
            var movieDtos = movies != null ? movies.Select(m => new MovieEntityDto(m.Id, m.Title, m.Description, m.ThumbnailUrl, m.Language)).ToList() : new List<MovieEntityDto>();

            return new MovieCollectionEntityDto(collection.Id, collection.Title, collection.UserId, movieDtos);
        }
    }
}
