namespace Domain.Entities.Common;
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? Deleted { get; set; }
    string? DeletedBy { get; set; }
}