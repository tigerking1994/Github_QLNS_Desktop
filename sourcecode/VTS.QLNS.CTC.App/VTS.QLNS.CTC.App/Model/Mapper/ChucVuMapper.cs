using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ChucVuMapper : Profile
    {
        public ChucVuMapper()
        {
            CreateMap<TlDmChucVuModel, TlDmChucVu>().ReverseMap();
            CreateMap<TlDmChucVuNq104, TlDmChucVuNq104Model>().ReverseMap();
        }
    }
}
