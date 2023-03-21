using AutoMapper;
using DecentReads.Application.DTOs.Book;
using DecentReads.Domain.Books;

namespace DecentReads.Api.Mapping
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, BookDto>()
           .ForMember(m => m.Author, c => c.MapFrom(s => s.Author.FullName));
        }
    }
}
