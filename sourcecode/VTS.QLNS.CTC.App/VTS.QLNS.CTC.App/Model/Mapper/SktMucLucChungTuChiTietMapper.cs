using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SktMucLucChungTuChiTietMapper : Profile
    {
        public SktMucLucChungTuChiTietMapper()
        {
            CreateMap<NsSktMucLuc, SktMucLucChungTuChiTietModel>();
            CreateMap<NsSktChungTuChiTiet, SktMucLucChungTuChiTietModel>();
        }
    }
}