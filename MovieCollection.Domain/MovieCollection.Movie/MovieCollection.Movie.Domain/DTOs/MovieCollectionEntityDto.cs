namespace MovieCollection.Movie.Domain.DTOs
{
    public class MovieCollectionEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public List<MovieEntityDto> Movies { get; set; }

        public MovieCollectionEntityDto() { }
        public MovieCollectionEntityDto(int id, string title, int userId, List<MovieEntityDto> movies)
        {
            Id = id;
            Title = title;
            UserId = userId;
            Movies = movies;
        }
    }
}
