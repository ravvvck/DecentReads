using AutoMapper;
using DecentReads.Application.DTOs.Book;
using DecentReads.Domain.Books;
using GameHost.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Books.Queries.GetAll
{
    public class GetAllBooksQuery : IRequest<List<BookDto>>
    {
    }

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }
        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await bookRepository.GetAllAsync();
            var bookDtos = mapper.Map<List<BookDto>>(books);
            return bookDtos;
        }
    }

}
