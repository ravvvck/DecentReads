using AutoMapper;
using DecentReads.Application.DTOs.Book;
using DecentReads.Application.Features.Book.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecentReads.Domain.Entities;
using DecentReads.Application.DTOs.Book.Validators;
using DecentReads.Application.Exceptions;
using DecentReads.Application.Responses;
using DecentReads.Application.Contracts.Persistence;

namespace DecentReads.Application.Features.Book.Handlers.Commands
{
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, BaseCommandResponse>
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public CreateBookRequestHandler(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBookDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BookDto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var book = mapper.Map<Domain.Entities.Book>(request.BookDto);
            book = await bookRepository.Create(book);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = book.Id;

            return response;
        }


    }
}
