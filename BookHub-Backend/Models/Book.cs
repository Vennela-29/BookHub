//using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace LibraryManagement.Models
{
    public class Book
    {
            public int BookId { get; set; }
            public string BookName { get; set; }
            public string Author { get; set; }
            public int AvailableCopies { get; set; } = 10;
        
        public ICollection<BorrowedBook> BorrowedList { get; set; } = new List<BorrowedBook>();
    }

}

