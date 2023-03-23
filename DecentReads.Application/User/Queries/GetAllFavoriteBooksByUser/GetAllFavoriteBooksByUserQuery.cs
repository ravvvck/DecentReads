using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Domain.Users.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.User.Queries.GetAllFavoriteBooksByUser
{
    public class GetAllFavoriteBooksByUserQuery : IRequest<List<FavoriteBook>>
    {
        public int UserId { get; set; }
    }

    public class GetAllFavoriteBooksByUserQueryHandler : IRequestHandler<GetAllFavoriteBooksByUserQuery, List<FavoriteBook>>
    {
        private readonly IUserRepository userRepository;

        public GetAllFavoriteBooksByUserQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task<List<FavoriteBook>> Handle(GetAllFavoriteBooksByUserQuery request, CancellationToken cancellationToken)
        {
            var user = userRepository.GetUserByUserId(request.UserId);
            var favoriteBooks = user.FavoriteBooks.ToList();
            return Task.FromResult(favoriteBooks);
        }
    }
}
