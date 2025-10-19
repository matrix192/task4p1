namespace task4p1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
    }
}
