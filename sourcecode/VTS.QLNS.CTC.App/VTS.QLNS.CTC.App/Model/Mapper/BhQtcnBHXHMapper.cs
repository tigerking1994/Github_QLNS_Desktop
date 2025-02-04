using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcnBHXHMapper : Profile
    {
        public BhQtcnBHXHMapper()
        {
            CreateMap<BhQtcnBHXHModel, BhQtcnBHXH>().ReverseMap();
            CreateMap<BhQtcnBHXHQuery, BhQtcnBHXHModel>().ReverseMap();
        }
    }
}
