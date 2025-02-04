using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class KhtBHXHMapper : Profile
    {
        public KhtBHXHMapper()
        {
            CreateMap<BhKhtBHXH, BhKhtBHXHModel>().ReverseMap();
            CreateMap<BhKhtBHXHQuery, BhKhtBHXHModel>().ReverseMap();
            CreateMap<BhKhtBHXHQuery, BhKhtBHXH>().ReverseMap();
        }
    }
}
