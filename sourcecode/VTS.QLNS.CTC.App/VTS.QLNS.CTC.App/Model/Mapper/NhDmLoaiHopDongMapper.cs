using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmLoaiHopDongMapper : Profile
    {
        public NhDmLoaiHopDongMapper()
        {
            CreateMap<NhDmLoaiHopDong, NhDmLoaiHopDongModel>();
            CreateMap<NhDmLoaiHopDongModel, NhDmLoaiHopDong>();
        }
    }
}
