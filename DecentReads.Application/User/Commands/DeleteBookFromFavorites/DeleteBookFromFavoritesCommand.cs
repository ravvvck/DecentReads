using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.User.Commands.DeleteBookFromFavorites
{
    public class DeleteBookFromFavoritesCommand : IRequest<Unit>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class DeleteBookFromFavoritesCommandHandler : IRequestHandler<DeleteBookFromFavoritesCommand, Unit>
    {
        private readonly IUserRepository userRepository;

        public DeleteBookFromFavoritesCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteBookFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var user = userRepository.GetUserByUserId(request.UserId);
            user.DeleteBookFromFavorites(request.BookId);
            await userRepository.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
