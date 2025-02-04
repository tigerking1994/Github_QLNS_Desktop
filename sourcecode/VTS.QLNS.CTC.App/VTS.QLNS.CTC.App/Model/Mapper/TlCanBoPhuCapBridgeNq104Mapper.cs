using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapBridgeNq104Mapper : Profile
    {
        public TlCanBoPhuCapBridgeNq104Mapper()
        {

            CreateMap<TlCanBoPhuCapBridgeNq104, AllowencePhuCapNq104Criteria>();
        }
    }
}
