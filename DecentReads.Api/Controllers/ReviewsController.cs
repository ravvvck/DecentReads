using DecentReads.Application.DTOs.Rating;
using DecentReads.Application.Ratings.Commands.AddRating;
using DecentReads.Application.Ratings.Queries.GetAllRatings;
using DecentReads.Application.Ratings.Queries.GetAllRatingsForUserId;
using DecentReads.Application.Ratings.Queries.GetRatingByBookIdAndUserId;
using DecentReads.Application.Reviews.Commands.AddReviewCommand;
using DecentReads.Application.Reviews.Commands.DeleteReviewCommand;
using DecentReads.Application.Reviews.Commands.UpdateReviewCommand;
using DecentReads.Application.Reviews.Queries.GetAllReviewsForBookQuery;
using DecentReads.Domain.Books.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DecentReads.Api.Controllers
{
    [Route("api/books/{bookId}/review")]
    [ApiController]

    public class ReviewsController : Controller
    {
        private readonly IMediator mediator;

        public ReviewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAllReviewsForBook([FromRoute] int bookId)
        {
            var query = new GetAllReviewsForBookQuery() {BookId = bookId };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddReview(AddReviewCommand command)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            command.UserId = userId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> UpdateReview(UpdateReviewCommand command)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            command.UserId = userId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteReview(DeleteReviewCommand command)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            command.UserId = userId;
            var result = await mediator.Send(command);
            return Ok(result);
        }





    }
}
