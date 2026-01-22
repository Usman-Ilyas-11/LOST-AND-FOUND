namespace LostAndFound
{
    public class MatchedItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; } // <-- Add this
        public string MatchedDate { get; set; }
        public string StudentId { get; set; }
        public string AdminUsername { get; set; }
        public byte[] Image { get; set; }
        public bool Returned { get; set; } = false;
    }
}
