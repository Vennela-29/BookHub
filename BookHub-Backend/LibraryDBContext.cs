using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement
{
    public class LibraryDBContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Admin>().HasKey(s => s.AdminId);
            //modelBuilder.Entity<Admin>().HasData(new List<Admin>
            //{
            //    new Admin
            //    {
            //        AdminId=1,
            //        UserName="Aravind",
            //        Designation="Librarian",
            //        Password=BCrypt.Net.BCrypt.HashPassword("Admin123")
            //    }
            //});
;            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<Student>().HasKey(s=>s.Id);
            modelBuilder.Entity<Student>(entity => entity.Property(s => s.Id).UseIdentityColumn());
            modelBuilder.Entity<Student>(entity => entity.Property(s =>s.StudentName).IsRequired().HasMaxLength(100));
            //modelBuilder.Entity<Student>(entity => entity.Property(s => s.Password).HasMaxLength(12));
            modelBuilder.Entity<Student>(entity => entity.Property(s => s.Phone).HasMaxLength(10));
            modelBuilder.Entity<Student>().HasData(new List<Student>
            {
                new Student
                {
                    Id = 1,
                    StudentName= "Akash",
                    Year=1,
                    Department="CSE",
                    Email="Akash@gmail.com",
                    Password=BCrypt.Net.BCrypt.HashPassword("Ak2345"),
                    Phone="9876543212"
                },
                new Student
                {
                    Id = 2,
                    StudentName= "Goutham",
                    Year=3,
                    Department="ECE",
                    Email="Goutham@gmail.com",
                    Password=BCrypt.Net.BCrypt.HashPassword("GM2345"),
                    Phone="9321456708"
                },
                new Student
                {
                    Id = 3,
                    StudentName= "Karthik",
                    Year=2,
                    Department="EEE",
                    Email="Karthik@gmail.com",
                    Password=BCrypt.Net.BCrypt.HashPassword("KT2345"),
                    Phone="8759543212"
                },
                new Student
                {
                    Id = 4,
                    StudentName= "Darshan",
                    Year=1,
                    Department="MECH",
                    Email="Darshan@gmail.com",
                    Password=BCrypt.Net.BCrypt.HashPassword("DS2345"),
                    Phone="9892826312"
                },
                new Student
                {
                    Id = 5,
                    StudentName= "Arjun",
                    Year=4,
                    Department="CSE",
                    Email="Arjun@gmail.com",
                    Password=BCrypt.Net.BCrypt.HashPassword("AJ2345"),
                    Phone="9874853212"
                }
            });
            modelBuilder.Entity<Book>().ToTable("Books");
            //modelBuilder.Entity<Book>().HasOne(s => s.Students).WithMany(b => b.Books).HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Book>().Property(b => b.BookId).UseIdentityColumn();
            modelBuilder.Entity<Book>().Property(b => b.BookName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(s => s.Author).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Book>().HasData(new List<Book>
            {
                new Book
                {
                    BookId = 1,
                    BookName = "Ramayan",
                    Author = "Valmiki",
                    
                    
                },
                new Book
                {
                    BookId = 2,
                    BookName = "Diary of a Young girl",
                    Author = "Anne Frank",
                    
                },
                new Book
                {
                    BookId = 3,
                    BookName = "House of cards",
                    Author = "Sudha Murthy",
                    
                    
                },
                new Book
                {
                    BookId = 4,
                    BookName = "Wings of Fire",
                    Author = "A.P.J Abdul Kalam",
                },
                new Book
                {
                    BookId = 5,
                    BookName = "You can win",
                    Author = "Shiv Khera",
                    
                },
                new Book
                {
                    BookId = 6,
                    BookName = "Mahabharat",
                    Author = "Veda Vyas",
                }
            });
            modelBuilder.Entity<BorrowedBook>()
        .HasKey(bb => new { bb.studentId, bb.bookId });
            modelBuilder.Entity<BorrowedBook>().HasOne(b=>b.book).WithMany(s=>s.BorrowedList).HasForeignKey(b=>b.bookId);
            modelBuilder.Entity<BorrowedBook>().HasOne(s=>s.student).WithMany(s => s.BorrowedList).HasForeignKey(b => b.studentId);
        }
    }
}
