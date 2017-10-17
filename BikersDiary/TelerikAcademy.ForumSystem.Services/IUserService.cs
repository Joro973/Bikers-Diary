using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelerikAcademy.ForumSystem.Data.Model;

namespace TelerikAcademy.ForumSystem.Services
{
    public interface IUserService
    {
        User GetUserByName(string userName);

        IQueryable<User> GetAll();
    }
}
