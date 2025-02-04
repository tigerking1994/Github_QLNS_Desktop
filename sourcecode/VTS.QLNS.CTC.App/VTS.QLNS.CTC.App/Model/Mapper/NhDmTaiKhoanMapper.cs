using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmTaiKhoanMapper : Profile
    {
        public NhDmTaiKhoanMapper()
        {
            CreateMap<NhDmTaiKhoan, NhDmTaiKhoanModel>().ReverseMap();
        }
    }
}
