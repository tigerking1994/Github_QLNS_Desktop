using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;


namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcnKCBChiTietMapper : Profile
    {
        public BhQtcnKCBChiTietMapper()
        {
            CreateMap<BhQtcnKCBChiTietModel, BhQtcnKCBChiTiet>().ReverseMap();
            CreateMap<BhQtcnKCBChiTietQuery, BhQtcnKCBChiTietModel>()
                .ForMember(x => x.IsHangCha, y => y.MapFrom(z => z.BHangCha))
                .ReverseMap();
        }
    }
}
