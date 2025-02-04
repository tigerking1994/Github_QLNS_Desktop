using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDtcDcdToanChiChiTietMapper : Profile
    {
        public BhDtcDcdToanChiChiTietMapper()
        {
            CreateMap<BhDtcDcdToanChiChiTietModel, BhDtcDcdToanChiChiTiet>().ReverseMap();
            CreateMap<BhDtcDcdToanChiChiTietModel, BhDtcDcdToanChiChiTietQuery>().ReverseMap();
        }
    }
}
