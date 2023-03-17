using AutoMapper;
using DecentReads.Application.Contracts.Persistence;
using DecentReads.Application.DTOs.Book;
using DecentReads.Application.Features.Book.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Features.Book.Handlers.Queries
{
    public class GetBooksListRequestHandler : IRequestHandler<GetBooksListRequest, List<BookDto>>
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public GetBooksListRequestHandler(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        

        public async Task<List<BookDto>> Handle(GetBooksListRequest request, CancellationToken cancellationToken)
        {
            var books = await bookRepository.GetAll();
            return mapper.Map<List<BookDto>>(books);
        }
    }
}
