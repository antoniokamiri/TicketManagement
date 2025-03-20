namespace Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime Raised_Date { get; set; }
    public DateTime Expected_Date { get; set; }
    public string? Assigned_To_Id { get; set; }
    public virtual User? Assigned_To { get; set; }
    public string? Raised_By_Id { get; set; }
    public virtual User? Raised_By { get; set; }
    public int Product_Id { get; set; }
    public virtual Product? Product { get; set; }
    public int Category_Id { get; set; }
    public virtual Category? Category { get; set; }
    public int Priority_Id { get; set; }
    public virtual Priority? Priority { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; } = [];
}
