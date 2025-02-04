using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcqBHXHMapper : Profile
    {
        public BhQtcqBHXHMapper()
        {
            CreateMap<BhQtcqBHXHModel, BhQtcqBHXH>().ReverseMap();
            CreateMap<BhQtcqBHXHQuery, BhQtcqBHXHModel>().ReverseMap();
        }
    }
}
