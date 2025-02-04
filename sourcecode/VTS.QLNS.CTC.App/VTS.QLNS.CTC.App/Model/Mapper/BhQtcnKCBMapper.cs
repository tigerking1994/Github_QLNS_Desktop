using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcnKCBMapper : Profile
    {
        public BhQtcnKCBMapper()
        {
            CreateMap<BhQtcnKCBModel, BhQtcnKCB>().ReverseMap();
            CreateMap<BhQtcnKCBQuery, BhQtcnKCBModel>().ReverseMap();
        }
    }
}
