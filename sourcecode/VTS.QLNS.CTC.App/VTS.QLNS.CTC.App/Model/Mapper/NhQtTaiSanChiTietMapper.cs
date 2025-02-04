using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhQtTaiSanChiTietMapper : Profile
    {
        public NhQtTaiSanChiTietMapper()
        {
            CreateMap<NhQtTaiSanChiTiet, NhQtTaiSanChiTietModel>();
            CreateMap<NhQtTaiSanChiTietModel, NhQtTaiSanChiTietQuery>();
            CreateMap<NhQtTaiSanChiTietQuery, NhQtTaiSanChiTiet>();
        }
    }
}
