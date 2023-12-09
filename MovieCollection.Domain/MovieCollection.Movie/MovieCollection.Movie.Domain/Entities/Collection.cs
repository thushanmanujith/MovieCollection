namespace MovieCollection.Movie.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<MovieCollection> MovieCollection { get; set; }

        public Collection() { }
        public Collection(string title, int userId)
        {
            Title = title;
            UserId = userId;
        }
        public Collection(int id, string title, int userId) 
        {
            Id = id;
            Title = title;
            UserId = userId;   
        } 
    }
}
