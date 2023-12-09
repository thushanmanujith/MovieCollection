namespace MovieCollection.Movie.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Language { get; set; }
        public virtual ICollection<MovieCollection> MovieCollection { get; set; }

        public Movie() { }
        public Movie(string title, string description, string thumbnailUrl, string language)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Language = language;
        }
    }
}
