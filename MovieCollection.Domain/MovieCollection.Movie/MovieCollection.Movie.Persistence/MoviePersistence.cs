using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Ports.OutGoing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace MovieCollection.Movie.Persistence
{
    public class MoviePersistence : IMoviePersistence
    {
        protected readonly MovieDataContext _dataContext;

        public MoviePersistence(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddMovieToCollectionAsync(int collectionId, int moveId)
        {
            var movieCollection = new Domain.Entities.MovieCollection(moveId, collectionId);
            var entity = _dataContext.Add(movieCollection);
            await _dataContext.SaveChangesAsync();
            return entity.Entity != null;
        }

        public async Task<bool> AddMoviesToCollectionAsync(List<Domain.Entities.MovieCollection> movieCollections)
        {
            await _dataContext.AddRangeAsync(movieCollections);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Collection> AddCollectionAsync(Collection collection)
        {
            var entity = _dataContext.Add(collection);
            await _dataContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Domain.Entities.Movie> AddUpdateMovieAsync(Domain.Entities.Movie movie)
        {
            var entity = _dataContext.Update(movie);
            await _dataContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<List<Domain.Entities.Movie>> GetAllMoviesAsync()
        {
            return await _dataContext.Movie.ToListAsync();
        }

        public async Task<List<Domain.Entities.Movie>> GetAllMoviesByCollectionIdAsync(int collectionId)
        {
            var movieIds = _dataContext.MovieCollection.Where(m => m.CollectionId.Equals(collectionId)).Select(c => c.MovieId).ToList();
            return await _dataContext.Movie.Where(m => movieIds.Contains(m.Id)).ToListAsync();
        }

        public async Task<Collection> GetCollectionByUserAsync(int userId)
        {
            return await _dataContext.Collection.FirstOrDefaultAsync(m => m.UserId == userId);
        }

        public async Task<Domain.Entities.Movie> GetMovieAsync(string title)
        {
            return await _dataContext.Movie.Where(m => m.Title.ToLower().Equals(title)).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.Movie> GetMovieAsync(int id)
        {
            return await _dataContext.Movie.Where(m => m.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveMovieFromCollectionAsync(int collectionId, int moveId)
        {
            var movieCollection = await _dataContext.MovieCollection.FirstOrDefaultAsync(m => m.MovieId.Equals(moveId) && m.CollectionId.Equals(collectionId));
            var entity = _dataContext.Remove(movieCollection);
            await _dataContext.SaveChangesAsync();
            return entity.Entity != null;
        }

        public async Task<List<Domain.Entities.Movie>> SearchMoviesByTitle(string title, int collectionId)
        {
            var movieIds = _dataContext.MovieCollection.Where(m => m.CollectionId.Equals(collectionId)).Select(c => c.CollectionId).ToList();
            return await _dataContext.Movie.Where(m => movieIds.Contains(m.Id) && m.Title.ToLower().StartsWith(title.ToLower())).ToListAsync();
        }
    }
}
