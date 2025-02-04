using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;


namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcqKCBChiTietMapper : Profile
    {
        public BhQtcqKCBChiTietMapper()
        {
            CreateMap<BhQtcqKCBChiTietModel, BhQtcqKCBChiTiet>().ReverseMap();
            CreateMap<BhQtcqKCBChiTietQuery, BhQtcqKCBChiTietModel>().ReverseMap();
        }
    }
}
