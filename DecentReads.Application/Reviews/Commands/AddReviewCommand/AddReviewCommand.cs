using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Reviews.Commands.AddReviewCommand
{
    public class AddReviewCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
    }

    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, Unit>
    {
        private readonly IReviewRepository reviewRepository;

        public AddReviewCommandHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<Unit> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewRepository.AddAsync(request.UserId, request.BookId, request.Content);
            return Unit.Value;
        }
    }
}
