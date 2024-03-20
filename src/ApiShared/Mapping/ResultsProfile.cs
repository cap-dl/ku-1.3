using ApiShared.Models;
using AutoMapper;
using FluentResults;

namespace ApiShared.Mapping
{
    public sealed class ResultsProfile
        : Profile
    {
        public ResultsProfile()
        {
            CreateMap<IError, ErrorModel>();
            CreateMap<Error, ErrorModel>();

            CreateMap(typeof(Result<>), typeof(ResultModel<>))
                .ForMember(nameof(ResultModel<int>.Value), o =>
                {
                    o.MapFrom(nameof(Result<int>.ValueOrDefault));
                });
        }
    }
}
