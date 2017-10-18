namespace BikersDiary.ForumSystem.Data.Model
{
    using System;
    using Abstracts;

    public class Comment : DataModel
    {
        public string Content { get; set; }

        public Guid PostId { get; set; }

        //public int PostId { get; set; }

        public virtual User Author { get; set; }

        public string AuthorId { get; set; }
    }
}
