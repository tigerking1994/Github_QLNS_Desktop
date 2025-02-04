using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcqKCBMapper : Profile
    {
        public BhQtcqKCBMapper()
        {
            CreateMap<BhQtcqKCBModel, BhQtcqKCB>().ReverseMap();
            CreateMap<BhQtcqKCBQuery, BhQtcqKCBModel>().ReverseMap();
        }
    }
}
