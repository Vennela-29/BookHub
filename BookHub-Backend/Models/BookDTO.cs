using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookDTO
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime? BorrowDate { get; set; } = DateTime.Now;
        public DateTime? OverdueDate { get; set; } = DateTime.Now.AddDays(7);

    }
}
