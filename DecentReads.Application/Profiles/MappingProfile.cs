using AutoMapper;
using DecentReads.Application.DTOs.Author;
using DecentReads.Application.DTOs.Book;
using DecentReads.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
          .ForMember(m => m.AuthorFirstName, c => c.MapFrom(s => s.Author.FirstName))
          .ForMember(m => m.AuthorLastName, c => c.MapFrom(s => s.Author.LastName));

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<CreateBookDto, Book>()
                .ForMember(r => r.Author,
                c => c.MapFrom(dto => new Author() { FirstName = dto.AuthorFirstName, LastName = dto.AuthorLastName }));
        }
    }
}
