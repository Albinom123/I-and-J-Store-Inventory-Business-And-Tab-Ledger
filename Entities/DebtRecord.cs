public class DebtRecord
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime DateAndTime { get; set; }
    public string Item { get; set; }        // ← Required
    public string Category { get; set; }    // ← Required
    public decimal Amount { get; set; }     // ← Maybe also required?
}