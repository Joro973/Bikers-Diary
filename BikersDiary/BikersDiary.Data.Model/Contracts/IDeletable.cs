using System;

namespace BikersDiary.ForumSystem.Data.Model.Contracts
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
