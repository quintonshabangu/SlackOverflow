using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Domain.ReferenceModels;

namespace WebApp.Models.Domain
{
    public class Post : IEntity
    {
        public long Id { get; set; }

        public int Votes { get; set; }

        public string Text { get; set; }

        [Required]
        public string TimeStamp { get; set; }

        [Required]
        public string SlackUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public PostType PostType { get; set; }
    }
}