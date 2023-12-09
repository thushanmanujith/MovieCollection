namespace MovieCollection.Movie.Domain.Ports.Incoming.Commands
{
    public class AddMovieCommand : ICommand
    {
        public AddMovieCommand(string title, string description, string thumbnailUrl, string language)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Language = language;
        }

        public string Title { get; }
        public string Description { get; }
        public string ThumbnailUrl { get; }
        public string Language { get; }
    }
}
