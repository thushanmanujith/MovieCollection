namespace MovieCollection.Movie.Domain.DTOs
{
    public class MovieEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Language { get; set; }

        public MovieEntityDto(int id, string title, string description, string thumbnailUrl, string language)
        {
            Id = id;
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Language = language;
        }
    }
}
