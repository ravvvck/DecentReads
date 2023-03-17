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
    public class GetBookRequestHandler : IRequestHandler<GetBookRequest, BookDto>
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public GetBookRequestHandler(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }
        public async Task<BookDto> Handle(GetBookRequest request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.Get(request.Id);
            return mapper.Map<BookDto>(book);
        }
    }
}
