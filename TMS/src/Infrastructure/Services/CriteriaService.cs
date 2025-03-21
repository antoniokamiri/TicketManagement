using Domain.Entities;
using Domain.Interfaces;
using Domain.IRepository;

namespace Infrastructure.Services;

public class CriteriaService(IUnitOfWork unitOfWork) : ICriteriaService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public List<Category> GetCategories()
    {
        return _unitOfWork.Repository<Category>().GetAll();
    }

    public List<Priority> GetPriorities()
    {
        return _unitOfWork.Repository<Priority>().GetAll();

    }

    public List<Product> GetProducts()
    {
        return _unitOfWork.Repository<Product>().GetAll();

    }

    public List<string> GetStatus()
    {
        return ["NEW", "OPEN", "CLOSED"];
    }
}
