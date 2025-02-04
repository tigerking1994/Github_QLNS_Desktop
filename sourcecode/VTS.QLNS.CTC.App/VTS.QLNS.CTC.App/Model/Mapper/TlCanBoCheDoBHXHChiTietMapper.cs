using AutoMapper;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoCheDoBHXHChiTietMapper: Profile
    {
        public TlCanBoCheDoBHXHChiTietMapper()
        {
            CreateMap<TlCanBoCheDoBHXHChiTiet, TlCanBoCheDoBHXHChiTietModel>().ReverseMap();
            CreateMap<TlCanBoCheDoBHXHChiTiet, TlCanBoCheDoBHXHChiTietQuery>().ReverseMap();
            CreateMap<TlCanBoCheDoBHXHChiTietModel, TlCanBoCheDoBHXHChiTietQuery>().ReverseMap();
        }
    }
}
