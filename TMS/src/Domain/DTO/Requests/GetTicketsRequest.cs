namespace Domain.DTO.Requests;
public class GetTicketsRequest
{
    public string? Summary { get; set; }
    public string[]? Raised_By_Id { get; set; }
    public string[]? Status { get; set; }
    public int[]? Product_Id { get; set; }
    public int[]? Category_Id { get; set; }
    public int[]? Priority_Id { get; set; }
}
