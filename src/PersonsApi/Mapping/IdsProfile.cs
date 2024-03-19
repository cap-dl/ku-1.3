using AutoMapper;
using Core;

namespace PersonsApi.Mapping
{
    public class IdsProfile
        : Profile
    {
        public IdsProfile()
        {
            CreateMap<PersonId, int>()
                .ConvertUsing(s => s.Value);
            CreateMap<NoteId, int>()
                .ConvertUsing(s => s.Value);
        }
    }
}
