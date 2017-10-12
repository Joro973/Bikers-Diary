namespace Bikers_Diary.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }
    }
}
