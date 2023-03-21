using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Exceptions;
using GameHost.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Ratings.Commands.AddRating
{
    public class AddRatingCommand : IRequest<Unit>
    {
        [RegularExpression(@"1|2|3|4|5")]
        public int Value { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, Unit>
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IBookRepository bookRepository;

        public AddRatingCommandHandler(IRatingRepository ratingRepository, IBookRepository bookRepository)
        {
            this.ratingRepository = ratingRepository;
            this.bookRepository = bookRepository;
        }
        public async Task<Unit> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.BookId);
            
            await ratingRepository.AddOrUpdateAsync(request.UserId, request.BookId, request.Value);
            return Unit.Value;
        }
    }
}
