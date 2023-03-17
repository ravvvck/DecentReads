using DecentReads.Application.DTOs.Book;
using DecentReads.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Features.Book.Requests
{
    public class CreateBookRequest : IRequest<BaseCommandResponse>
    {
        public CreateBookDto BookDto { get; set; }
    }
}
