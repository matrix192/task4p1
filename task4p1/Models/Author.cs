namespace task4p1.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}