using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuToanChiPhiMapper : Profile
    {
        public NhDaDuToanChiPhiMapper()
        {
            CreateMap<Core.Domain.NhDmChiPhi, NhDaDuToanChiPhiModel>();
            CreateMap<Core.Domain.NhDaQdDauTuChiPhi, NhDaDuToanChiPhiModel>();
            CreateMap<NhDaQdDauTuChiPhiModel, NhDaDuToanChiPhiModel>();
            CreateMap<Core.Domain.NhDaDuToanChiPhi, NhDaDuToanChiPhiModel>();
            CreateMap<NhDaDuToanChiPhiModel, Core.Domain.NhDaDuToanChiPhi>();
        }
    }
}
