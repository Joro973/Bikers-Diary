using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BikersDiary.ForumSystem.Data.Model;
using BikersDiary.ForumSystem.Data.Repositories;
using BikersDiary.ForumSystem.Data.SaveContext;
using Bytes2you.Validation;

namespace BikersDiary.ForumSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly ISaveContext context;

        public UserService(IEfRepository<User> usersRepo, ISaveContext context)
        {
            Guard.WhenArgument(usersRepo, "usersRepo").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.usersRepo = usersRepo;
            this.context = context;
        }

        public User GetUserByName(string userName)
        {
            return this.usersRepo.All.First(u => u.UserName == userName);
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }
    }
}
