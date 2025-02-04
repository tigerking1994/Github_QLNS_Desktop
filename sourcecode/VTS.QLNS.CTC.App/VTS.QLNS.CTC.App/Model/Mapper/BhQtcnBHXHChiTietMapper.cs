using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;


namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcnBHXHChiTietMapper : Profile
    {
        public BhQtcnBHXHChiTietMapper()
        {
            CreateMap<BhQtcnBHXHChiTietModel, BhQtcnBHXHChiTiet>().ReverseMap();
            CreateMap<BhQtcnBHXHChiTietQuery, BhQtcnBHXHChiTietModel>().ReverseMap();
            CreateMap<QtcnDetailImportModel, BhQtcnBHXHChiTiet>().ReverseMap();
        }
    }
}
