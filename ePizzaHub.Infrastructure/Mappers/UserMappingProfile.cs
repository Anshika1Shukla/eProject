using AutoMapper;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Request;


namespace ePizzaHub.Infrastructure.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
