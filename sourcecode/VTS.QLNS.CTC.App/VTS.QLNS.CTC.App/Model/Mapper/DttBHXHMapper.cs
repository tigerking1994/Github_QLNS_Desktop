using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DttBHXHMapper : Profile
    {
        public DttBHXHMapper()
        {
            CreateMap<BhDttBHXH, BhDttBHXHModel>().ReverseMap();
            CreateMap<BhDttBHXHQuery, BhDttBHXHModel>().ReverseMap();
            CreateMap<BhDttBHXHQuery, BhDttBHXH>().ReverseMap();
            CreateMap<BhDttChungTuDotNhanQuery, BhDttBHXHModel>();
            CreateMap<BhDtPhanBoChungTu, BhDttBHXHModel>();
        }
    }
}
