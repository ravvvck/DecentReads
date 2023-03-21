using AutoMapper;
using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.DTOs.Rating;
using DecentReads.Domain.Books.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQuery : IRequest<List<RatingDto>>
    {

    }

    public class GetAllRatingsQueryHandler : IRequestHandler<GetAllRatingsQuery, List<RatingDto>>
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IMapper mapper;

        public GetAllRatingsQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }
        public async Task<List<RatingDto>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
        {
            var ratings = await ratingRepository.GetAllAsync();
            var ratingDtos = mapper.Map<List<RatingDto>>(ratings);
            return ratingDtos;
        }
    }


}
