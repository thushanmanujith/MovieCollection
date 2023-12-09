namespace MovieCollection.Movie.Domain.Entities
{
    public class MovieCollection
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        public MovieCollection() { }
        public MovieCollection(int movieId, int collectionId) 
        {
            MovieId = movieId;
            CollectionId = collectionId;
        }
    }
}
