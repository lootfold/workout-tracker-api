using AutoMapper;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Controllers.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Status, StatusDto>();
        }
    }
}