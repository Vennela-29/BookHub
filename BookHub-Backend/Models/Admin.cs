using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Admin
    {
        [Required]
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public string Designation { get; set; }
    }
}
