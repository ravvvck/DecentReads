using AutoMapper;
using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.DTOs.Rating;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Ratings.Queries.GetAllRatingsForUserId
{
    public class GetAllRatingsForUserIdQuery : IRequest<List<RatingDto>>
    {
        public int UserId { get; set; }
    }

    public class GetAllRatingsForUserIdQueryHandler : IRequestHandler<GetAllRatingsForUserIdQuery, List<RatingDto>>
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IMapper mapper;

        public GetAllRatingsForUserIdQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }
        public async Task<List<RatingDto>> Handle(GetAllRatingsForUserIdQuery request, CancellationToken cancellationToken)
        {
            var ratings = await ratingRepository.GetAllRatingsForUserByIdAsync(request.UserId);
            var ratingDtos = mapper.Map<List<RatingDto>>(ratings);
            return ratingDtos;
        }
    }
}
