using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.Domain.ReferenceModels
{
    public class PostType : IReferenceModel
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }

        public const byte Question = 0;
        public const byte Answer = 1;
        public const byte Comment = 2;
    }
}