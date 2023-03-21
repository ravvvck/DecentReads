using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.DTOs.Rating;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Ratings.Queries.GetRatingByBookIdAndUserId
{
    public class GetRatingByBookIdAndUserIdQuery : IRequest<int>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class GetRatingByBookIdAndUserIdQueryHandler : IRequestHandler<GetRatingByBookIdAndUserIdQuery, int>
    {
        private readonly IRatingRepository ratingRepository;

        public GetRatingByBookIdAndUserIdQueryHandler(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }
        public async Task<int> Handle(GetRatingByBookIdAndUserIdQuery request, CancellationToken cancellationToken)
        {
            var rating = await ratingRepository.GetRatingForUserByBookIdAsync(request.BookId, request.UserId);
            return rating;
        }
    }
}
