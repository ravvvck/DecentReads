using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Domain.Books.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Reviews.Queries.GetAllReviewsForBookQuery
{
    public class GetAllReviewsForBookQuery : IRequest<List<Review>>
    {
        public int BookId { get; set; }
    }

    public class GetAllReviewsForBookQueryHandler : IRequestHandler<GetAllReviewsForBookQuery, List<Review>>
    {
        private readonly IReviewRepository reviewRepository;

        public GetAllReviewsForBookQueryHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<List<Review>> Handle(GetAllReviewsForBookQuery request, CancellationToken cancellationToken)
        {
            var reviews = await reviewRepository.GetAllByBookIdAsync(request.BookId);
            return reviews;
        }
    }
}
