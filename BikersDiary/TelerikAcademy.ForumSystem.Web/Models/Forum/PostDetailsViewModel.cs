using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TelerikAcademy.ForumSystem.Data.Model;

namespace TelerikAcademy.ForumSystem.Web.Models.Forum
{
    public class PostDetailsViewModel
    {
        private ICollection<Comment> comments;

        public PostDetailsViewModel(ICollection<Comment> comments)
        {
            this.comments = comments;
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorEmail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}