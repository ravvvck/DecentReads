using DecentReads.Application.Books.Queries.GetAll;
using DecentReads.Application.DTOs.Rating;
using DecentReads.Application.Ratings.Commands.AddRating;
using DecentReads.Application.Ratings.Queries.GetAllRatings;
using DecentReads.Application.Ratings.Queries.GetAllRatingsForUserId;
using DecentReads.Application.Ratings.Queries.GetRatingByBookIdAndUserId;
using DecentReads.Domain.Books.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DecentReads.Api.Controllers
{
    [Route("api/ratings")]
    [ApiController]

    public class RatingsController : Controller
    {
        private readonly IMediator mediator;

        public RatingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<RatingDto>>> GetAll()
        {
            var query = new GetAllRatingsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddRating(AddRatingCommand command)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            command.UserId = userId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<int>> GetRatingForUserByBookId([FromRoute] int bookId)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var command = new GetRatingByBookIdAndUserIdQuery() { UserId= userId, BookId = bookId };

            var result = await mediator.Send(command);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("personal")]
        public async Task<ActionResult<List<RatingDto>>> GetAllRatingsByUserId()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var query = new GetAllRatingsForUserIdQuery() { UserId = userId};
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
