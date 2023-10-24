using AutoMapper;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models.StudentDTO;

namespace MiniLMS.Application.Mappings;
public class MappingProfile : Profile
{
    Func<StudentCreateDTO, Student, int> func = (src, dest) =>
    {
        if (int.TryParse(src.Login, out int res))
            return res;
        return 0;


    };
    public MappingProfile()
    {
        CreateMap<StudentCreateDTO, Student>()
            //.ReverseMap();
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
            .ForMember("FullName", (config) =>
            {
                config.MapFrom(x => x.Name);
            });
            //.ForMember(dest => dest.Login, opt => opt.MapFrom((src, dest) =>
            //{
            //    if (int.TryParse(src.Login, out int res))
            //        return res;
            //    return 0;
            //}))
           //));

    }
}
