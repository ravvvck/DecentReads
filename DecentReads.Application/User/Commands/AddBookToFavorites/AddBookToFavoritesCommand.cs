using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.User.Commands.AddBookToFavorites
{
    public class AddBookToFavoritesCommand : IRequest<Unit>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class AddBookToFavoritesCommandHandler : IRequestHandler<AddBookToFavoritesCommand, Unit>
    {
        private readonly IUserRepository userRepository;

        public AddBookToFavoritesCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Unit> Handle(AddBookToFavoritesCommand request, CancellationToken cancellationToken)
        {
           var user = userRepository.GetUserByUserId(request.UserId);
           user.AddBookToFavorites(request.BookId);
           await userRepository.UpdateAsync(user);
           return Unit.Value;
        }
    }
}
