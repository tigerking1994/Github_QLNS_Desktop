using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NdtctgBHXHChiTietMapper : Profile
    {
        public NdtctgBHXHChiTietMapper()
        {
            CreateMap<BhDtctgBHXHChiTiet, BhDtctgBHXHChiTietModel>().ReverseMap();
            CreateMap<BhDtctgBHXHChiTietQuery, BhDtctgBHXHChiTietModel>().ReverseMap();
            CreateMap<BhDtctgBHXHChiTiet, BhDtctgBHXHChiTietQuery>().ReverseMap();
        
        }
    }
}
