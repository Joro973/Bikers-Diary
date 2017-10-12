using System;
using System.Linq;
using System.Web.Routing;

namespace Bikers_Diary.Controllers
{
    using System.Web.Mvc;
    using Bikers_Diary.Data;
    using Bikers_Diary.Models;
    using Microsoft.AspNet.Identity;

    public class BaseController : Controller
    {
        public BaseController(IDiaryData data)
        {
            this.Data = data;
        }

        public BaseController(IDiaryData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public IDiaryData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}