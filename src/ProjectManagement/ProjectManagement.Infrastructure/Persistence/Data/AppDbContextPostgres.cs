using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Infrastructure.Persistence.Data;

public class AppDbContextPostgres : AppDbContext
{
    public AppDbContextPostgres(DbContextOptions<AppDbContextPostgres> options) : base(options)
    {
    }
}
