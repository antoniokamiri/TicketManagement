using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<User>(options)
{

}
