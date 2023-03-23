using DecentReads.Application.Books.Commands.CreateBook;
using DecentReads.Application.Books.Queries.GetAll;
using DecentReads.Application.Books.Queries.GetBookById;
using DecentReads.Application.Books.Queries.SearchByTitleOrAuthor;
using DecentReads.Application.DTOs.Book;
using DecentReads.Application.User.Commands.AddBookToFavorites;
using DecentReads.Application.User.Commands.DeleteBookFromFavorites;
using DecentReads.Application.User.Queries.GetAllFavoriteBooksByUser;
using DecentReads.Domain.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "Librarian,Administrator")]
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

        [Authorize]
        [HttpPost("{bookId}/fav")]
        public async Task<ActionResult<IEnumerable<BookDto>>> AddBookToFAvorites([FromRoute] int bookId)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var command = new AddBookToFavoritesCommand() { BookId = bookId, UserId = userId };
            await mediator.Send(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{bookId}/fav")]
        public async Task<ActionResult<IEnumerable<BookDto>>> DeleteBookFromFavorites([FromRoute] int bookId)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var command = new DeleteBookFromFavoritesCommand() { BookId = bookId, UserId = userId };
            await mediator.Send(command);
            return Ok();
        }

        [Authorize]
        [HttpGet("fav")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllFavoriteBooksByUser()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var query = new GetAllFavoriteBooksByUserQuery() { UserId = userId };
            var results = await mediator.Send(query);
            return Ok(results);
        }

    }
}
