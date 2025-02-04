using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmDonViTinhMapper : Profile
    {
        public NhDmDonViTinhMapper()
        {
            CreateMap<NhDmDonViTinh, NhDmDonViTinhModel>();
            CreateMap<NhDmDonViTinhModel, NhDmDonViTinhQuery>();
            CreateMap<NhDmDonViTinhQuery, NhDmDonViTinh>();
        }
    }
}
