namespace MovieCollection.Core.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Language { get; set; }

        public MovieDto(string title, string description, string thumbnailUrl, string language)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Language = language;
        }
    }
}
