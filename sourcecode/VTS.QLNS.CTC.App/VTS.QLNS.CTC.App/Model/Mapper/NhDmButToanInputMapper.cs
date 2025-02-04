using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmButToanInputMapper : Profile
    {
        public NhDmButToanInputMapper()
        {
            CreateMap<NhDmButToanInput, NhDmButToanInputModel>().ReverseMap();

        }
    }
}
