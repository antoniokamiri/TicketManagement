namespace Domain.Entities;

public class Discussion
{
    public int Id { get; set; }
    public string? Message { get; set; }
    public DateTime Created_Date { get; set; }

    public int? Ticket_Id { get; set; }
    public virtual Ticket? Ticket { get; set; }

    public string? User_Id { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = [];
}