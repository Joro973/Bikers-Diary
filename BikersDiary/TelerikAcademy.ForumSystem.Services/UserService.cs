using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bytes2you.Validation;
using TelerikAcademy.ForumSystem.Data.Model;
using TelerikAcademy.ForumSystem.Data.Repositories;
using TelerikAcademy.ForumSystem.Data.SaveContext;

namespace TelerikAcademy.ForumSystem.Services
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
