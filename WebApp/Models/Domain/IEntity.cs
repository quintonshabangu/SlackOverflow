using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models.Domain
{
    public interface IEntity
    {
        [Required]
        long Id { get; set; }
    }
}
