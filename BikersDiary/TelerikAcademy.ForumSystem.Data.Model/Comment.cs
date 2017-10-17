using System;
using TelerikAcademy.ForumSystem.Data.Model.Abstracts;

namespace TelerikAcademy.ForumSystem.Data.Model
{
    public class Comment : DataModel
    {
        public string Content { get; set; }

        public Guid PostId { get; set; }

        //public int PostId { get; set; }

        public virtual User Author { get; set; }

        public string AuthorId { get; set; }
    }
}
