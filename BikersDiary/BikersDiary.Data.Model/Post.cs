using System;

namespace BikersDiary.ForumSystem.Data.Model
{
    using System.Collections.Generic;
    using Abstracts;

    public class Post : DataModel
    {
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual User Author { get; set; }

        public string AuthorId { get; set; }

        //public int Identifier { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
