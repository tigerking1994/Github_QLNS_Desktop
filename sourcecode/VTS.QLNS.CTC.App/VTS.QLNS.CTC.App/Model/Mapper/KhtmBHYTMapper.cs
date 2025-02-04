using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class KhtmBHYTMapper : Profile
    {
        public KhtmBHYTMapper()
        {
            CreateMap<BhKhtmBHYT, BhKhtmBHYTModel>().ReverseMap();
            CreateMap<BhKhtmBHYTQuery, BhKhtmBHYTModel>().ReverseMap();
            CreateMap<BhKhtmBHYTQuery, BhKhtmBHYT>().ReverseMap();
        }
    }
}
