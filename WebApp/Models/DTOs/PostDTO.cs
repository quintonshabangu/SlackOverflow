using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.DTOs
{
    public class PostDTO
    {
        public long Id { get; set; }

        public int Votes { get; set; }

        public string Text { get; set; }

        [Required]
        public string SlackUserId { get; set; }

        public bool IsQuestion { get; set; }

        public string TimeStamp { get; set; }
    }
}