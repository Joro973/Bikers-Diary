using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BikersDiary.ForumSystem.Data.Model;
using BikersDiary.ForumSystem.Web.Infrastructure;

namespace BikersDiary.ForumSystem.Web.Models.Home
{
    public class PostViewModel
    {
        private ICollection<Comment> comments;

        public Guid Id { get; set; }

        public PostViewModel()
        {
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        //public int Identifier { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorEmail { get; set; }

        //TODO FIX
        [DataType(DataType.DateTime)]
        public DateTime? PostedOn { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    configuration.CreateMap<Post, PostViewModel>()
        //        .ForMember(postViewModel => postViewModel.AuthorEmail, cfg => cfg.MapFrom(post => post.Author.Email))
        //        .ForMember(postViewModel => postViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        //}
    }
}