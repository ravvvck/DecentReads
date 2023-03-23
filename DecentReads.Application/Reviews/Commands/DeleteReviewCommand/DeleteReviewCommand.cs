using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Reviews.Commands.DeleteReviewCommand
{
    
    public class DeleteReviewCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewRepository reviewRepository;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewRepository.DeleteAsync(request.UserId, request.BookId);
            return Unit.Value;
        }
    }
}
