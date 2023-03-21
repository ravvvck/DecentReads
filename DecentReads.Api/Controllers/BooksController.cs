using DecentReads.Application.Books.Commands.CreateBook;
using DecentReads.Application.Books.Queries.GetAll;
using DecentReads.Application.Books.Queries.GetBookById;
using DecentReads.Application.Books.Queries.SearchByTitleOrAuthor;
using DecentReads.Application.DTOs.Book;
using DecentReads.Domain.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DecentReads.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var query = new GetAllBooksQuery();
            var books = await mediator.Send(query);
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("{bookId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetById([FromRoute]int bookId)
        {
            var query = new GetBookByIdQuery();
            query.BookId = bookId;
            var books = await mediator.Send(query);
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateBookCommand command)
        {
            var newBookId = await mediator.Send(command);
            return Ok(newBookId);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> Search([FromQuery] string name)
        {
            var command = new SearchByTitleOrAuthorQuery { SearchPhrase = name };
            var result = await mediator.Send(command);
            return Ok(result);
        }
            

    }
}
