namespace LibraryManagement.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int Year {  get; set; }
        public string Department {  get; set; }
        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();

    }
}
