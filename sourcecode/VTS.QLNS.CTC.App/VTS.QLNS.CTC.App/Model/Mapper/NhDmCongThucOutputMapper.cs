using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmCongThucOutputMapper : Profile
    {
        public NhDmCongThucOutputMapper()
        {
            CreateMap<NhDmCongThucOutput, NhDmCongThucOutputModel>().ReverseMap();

        }
    }
}
