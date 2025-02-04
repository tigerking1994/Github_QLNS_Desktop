using AutoMapper;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDanhMucChucVuMapper : Profile
    {
        public TlDanhMucChucVuMapper()
        {
            CreateMap<Core.Domain.TlDmChucVu, TlDanhMucChucVuModel>();
            CreateMap<TlDanhMucChucVuModel, Core.Domain.TlDmChucVu>();
            CreateMap<TlDanhMucChucVuNq104Model, Core.Domain.TlDmChucVuNq104>().ReverseMap();
            CreateMap<TlDanhMucChucDanhNq104Model, Core.Domain.TlDmChucVuNq104>().ReverseMap();
        }
    }
}
