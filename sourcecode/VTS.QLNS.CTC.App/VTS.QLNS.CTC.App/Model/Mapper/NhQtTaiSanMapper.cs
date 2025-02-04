using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhQtTaiSanMapper : Profile
    {
        public NhQtTaiSanMapper()
        {
            CreateMap<NhQtTaiSan, NhQtTaiSanModel>();
            CreateMap<NhQtTaiSanModel, NhQtTaiSanQuery>();
            CreateMap<NhQtTaiSanQuery, NhQtTaiSan>();
            CreateMap<NhQtTaiSanQuery, NhQtTaiSanModel>();
        }
    }
}
