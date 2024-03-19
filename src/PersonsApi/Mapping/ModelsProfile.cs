using AutoMapper;
using PersonsApi.Models;
using Core.Models;

namespace PersonsApi.Mapping
{
    public class ModelsProfile
        : Profile
    {
        public ModelsProfile()
        {
            CreateMap<Person, PersonModel>();
        }
    }
}
