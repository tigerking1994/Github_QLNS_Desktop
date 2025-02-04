using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class PbdtcBHXHMapper : Profile
    {
        public PbdtcBHXHMapper()
        {
            CreateMap<BhPbdtcBHXHModel, BhPbdtcBHXH>().ReverseMap();
        }
    }
}
