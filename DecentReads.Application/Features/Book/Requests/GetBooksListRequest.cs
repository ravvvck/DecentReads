using DecentReads.Application.DTOs.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Features.Book.Requests
{
    public class GetBooksListRequest : IRequest<List<BookDto>>
    {

    }
}
