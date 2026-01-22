public class LostItem
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public string LocationLost { get; set; }
    public string LostDate { get; set; }
    public string PersonName { get; set; }
    public string Contact { get; set; }
    public byte[] Image { get; set; }
    public string StudentId { get; set; } // <- MUST match DB column
}
