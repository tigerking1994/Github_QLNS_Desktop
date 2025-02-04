using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDmKinhPhiMapper : Profile
    {
        public BhDmKinhPhiMapper()
        {
            CreateMap<BhDmKinhPhi, BhDmKinhPhiModel>()
                .ReverseMap();
            CreateMap<BhDmKinhPhiModel, BhDmKinhPhi>()
                .ReverseMap();
        }
    }
}
