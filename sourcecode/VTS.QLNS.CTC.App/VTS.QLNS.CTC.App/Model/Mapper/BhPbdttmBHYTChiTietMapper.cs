using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhPbdttmBHYTChiTietMapper : Profile
    {
        public BhPbdttmBHYTChiTietMapper()
        {
            CreateMap<BhPbdttmBHYTChiTietModel, BhPbdttmBHYTChiTiet>().ReverseMap();
            CreateMap<BhPbdttmBHYTChiTietQuery, BhPbdttmBHYTChiTietModel>().ReverseMap();

        }
    }
}
