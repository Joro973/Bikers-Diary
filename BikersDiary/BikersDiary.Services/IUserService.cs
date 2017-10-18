using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikersDiary.ForumSystem.Data.Model;

namespace BikersDiary.ForumSystem.Services
{
    public interface IUserService
    {
        User GetUserByName(string userName);

        IQueryable<User> GetAll();
    }
}
