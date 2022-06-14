using SanTsgProject.Data.Interfaces;
using SanTsgProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanTsgProject.Data
{
   public interface IUnitOfWork : IDisposable
    {
        public IUserRepository User { get; }
        void Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _userDbContext;
        public IUserRepository User { get; private set; }
        public UnitOfWork(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
            User = new UserRepository(userDbContext);
        }

        

        public void Complete()
        {
            _userDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _userDbContext.Dispose();
        }
    }
}
