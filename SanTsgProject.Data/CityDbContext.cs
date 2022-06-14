using Microsoft.EntityFrameworkCore;
using SanTsgProject.Domain.Cities;

namespace SanTsgProject.Data
{
    public class CityDbContext:DbContext
    {
        public CityDbContext(DbContextOptions<CityDbContext> city) : base(city)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
