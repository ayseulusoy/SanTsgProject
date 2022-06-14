using SanTsgProject.Data.Interfaces;
using SanTsgProject.Domain.Users;

namespace SanTsgProject.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }
    }
}
