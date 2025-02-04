using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhPbdtcBHXHChiTietMapper : Profile
    {
        public BhPbdtcBHXHChiTietMapper()
        {
            CreateMap<BhPbdtcBHXHChiTiet, BhPbdtcBHXHChiTietModel>().ReverseMap();
            CreateMap<BhPbdtcBHXHChiTietQuery, BhPbdtcBHXHChiTietModel>().ReverseMap();
            CreateMap<BhPbdtcBHXHChiTiet, BhPbdtcBHXHChiTietQuery>().ReverseMap();
        
        }
    }
}
