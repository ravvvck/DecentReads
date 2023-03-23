using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Reviews.Commands.UpdateReviewCommand
{
    
    public class UpdateReviewCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
    }

    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IReviewRepository reviewRepository;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewRepository.UpdateAsync(request.UserId, request.BookId, request.Content);
            return Unit.Value;
        }
    }
}
