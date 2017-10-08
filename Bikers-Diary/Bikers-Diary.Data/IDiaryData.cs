
namespace Bikers_Diary.Data
{
    using Bikers_Diary.Data.Repositories;
    using Bikers_Diary.Models;

    public interface IDiaryData
    {
        IRepository<User> users { get; }
    }
}
