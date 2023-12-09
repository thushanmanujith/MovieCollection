namespace MovieCollection.Core.DTOs
{
    public class MovieCollectionDto
    {
        public string Title { get; set; }
        public List<int> MovieIds { get; set; }

        public MovieCollectionDto() { }
        public MovieCollectionDto(string title, List<int> movieIds)
        {
            Title = title;
            MovieIds = movieIds;
        }
    }
}
