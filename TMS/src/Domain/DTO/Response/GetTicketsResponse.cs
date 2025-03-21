namespace Domain.DTO.Response;

public class GetTicketsResponse
{
    public int Id { get; set; }
    public string TicketIdView
    {
        get { return "T" + Id.ToString().PadLeft(5, '0'); }
    }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime Raised_Date { get; set; }
    public DateTime Expected_Date { get; set; }
    public string? Assigned_To_Id { get; set; }
    public string? Assigned_To_Name { get; set; }
    public string? Raised_By_Id { get; set; }
    public string? Raised_By_Name { get; set; }
    public int Product_Id { get; set; }
    public string? Product_Name { get; set; }
    public int Category_Id { get; set; }
    public string? Category_Name { get; set; }
    public int Priority_Id { get; set; }
    public string? Priority_Name { get; set; }
}