using Domain.Entities;

namespace Domain.Interfaces;

public interface ICriteriaService
{
    List<Category> GetCategories();
    List<Product> GetProducts();
    List<Priority> GetPriorities();
    List<string> GetStatus();
}
