using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BorrowedBook
    {
        
        public int studentId {  get; set; }
        public Student student { get; set; }
        public Book book { get; set; }
        public int bookId { get; set; }
        public string BorrowedOn { get; set; } = DateTime.Now.ToShortDateString();
        public string OverdueOn { get; set; } = DateTime.Now.AddDays(7).ToShortDateString();
        //public bool Status {  get; set; }
        
    }
}
