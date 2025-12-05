using AutoMapper;
using LibraryManagement.Models;

namespace LibraryManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDTO, Student>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BorrowedBook>().ReverseMap();
        }

    }
}
