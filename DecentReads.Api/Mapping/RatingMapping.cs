using AutoMapper;
using DecentReads.Application.DTOs.Rating;
using DecentReads.Domain.Books.Entities;

namespace DecentReads.Api.Mapping
{
    public class RatingMapping : Profile
    {
        public RatingMapping()
        {
            CreateMap<Rating, RatingDto>()
                .ForMember(m => m.User_id, c => c.MapFrom(s => s.UserId))
                .ForMember(m => m.Rating, c => c.MapFrom(s => s.Value))
                .ForMember(m => m.Book_id, c => c.MapFrom(s => s.BookId));
        }
    }
}
