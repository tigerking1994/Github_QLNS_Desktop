using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;


namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcqBHXHChiTietMapper : Profile
    {
        public BhQtcqBHXHChiTietMapper()
        {
            CreateMap<BhQtcqBHXHChiTietModel, BhQtcqBHXHChiTiet>().ReverseMap();
            CreateMap<BhQtcqBHXHChiTietQuery, BhQtcqBHXHChiTietModel>().ReverseMap();
        }
    }
}
