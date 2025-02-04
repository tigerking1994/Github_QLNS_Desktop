using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmLoaiTaiSanMapper : Profile
    {
        public NhDmLoaiTaiSanMapper()
        {
            CreateMap<NhDmLoaiTaiSan, NhDmLoaiTaiSanModel>();
            CreateMap<NhDmLoaiTaiSanModel, NhDmLoaiTaiSanQuery>();
            CreateMap<NhDmLoaiTaiSanQuery, NhDmLoaiTaiSan>();
        }
    }
}
