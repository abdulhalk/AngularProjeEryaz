using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProje.Models
{
    public class user_table
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string UserMail { get; set; }
    }
}
