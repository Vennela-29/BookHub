using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class Student
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string StudentName { get; set; } = null!;
        [Required]
        public int Year { get; set; }
        [Required]
        public string Department { get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        [Required]
        [JsonIgnore]
        public string Password {  get; set; }
        [Phone]
        public string Phone { get; set; }
        public ICollection<BorrowedBook> BorrowedList { get; set; } = new List<BorrowedBook>();
        
    }
}
