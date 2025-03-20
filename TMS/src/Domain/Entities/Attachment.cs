namespace Domain.Entities;

public class Attachment
{
    public int Id { get; set; }
    public string? File_Name { get; set; }
    public string? Server_File_Name { get; set; }
    public int File_Size { get; set; }
    public DateTime Created_Date { get; set; }

    public int? Ticket_Id { get; set; }
    public virtual Ticket? Ticket { get; set; }

    public int? Discussion_Id { get; set; }
    public virtual Discussion? Discussion { get; set; }
}
