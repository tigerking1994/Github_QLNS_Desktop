using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhPbdttmBHYTMapper : Profile
    {
        public BhPbdttmBHYTMapper()
        {
            CreateMap<BhPbdttmBHYTModel, BhPbdttmBHYT>().ReverseMap();
            CreateMap<BhPbdttmBHYTQuery, BhPbdttmBHYT>().ReverseMap();
        }
    }
}
