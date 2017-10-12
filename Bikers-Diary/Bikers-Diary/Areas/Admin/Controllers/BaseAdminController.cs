using System.Web.Mvc;

namespace Bikers_Diary.Areas.Admin.Controllers
{
    using Bikers_Diary.Controllers;
    using Bikers_Diary.Data;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IDiaryData data)
            : base(data)
        {
            
        }
    }
}