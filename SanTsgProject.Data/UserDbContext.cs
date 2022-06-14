using Microsoft.EntityFrameworkCore;
using SanTsgProject.Domain.Users;

namespace SanTsgProject.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> user) : base(user)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
