using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhHdnkPhuongAnNhapKhauMapper : Profile
    {
        public NhHdnkPhuongAnNhapKhauMapper()
        {
            CreateMap<NhHdnkPhuongAnNhapKhau, NhHdnkPhuongAnNhapKhauModel>();
            CreateMap<NhHdnkPhuongAnNhapKhauModel, NhHdnkPhuongAnNhapKhau>();
        }
    }
}
