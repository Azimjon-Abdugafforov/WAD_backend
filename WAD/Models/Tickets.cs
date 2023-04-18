namespace WAD.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ActorsId { get; set; }
        public string Status { get; set; } = string.Empty;
        public byte[] Image { get; set; } 

    }
}
