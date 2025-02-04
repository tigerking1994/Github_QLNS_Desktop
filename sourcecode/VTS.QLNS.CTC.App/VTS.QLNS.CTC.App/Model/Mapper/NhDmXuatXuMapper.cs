using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmXuatXuMapper : Profile
    {
        public NhDmXuatXuMapper()
        {
            CreateMap<NhDmXuatXu, NhDmXuatXuModel>();
            CreateMap<NhDmXuatXuModel, NhDmXuatXuQuery>();
            CreateMap<NhDmXuatXuQuery, NhDmXuatXu>();
        }
    }
}
