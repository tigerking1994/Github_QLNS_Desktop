using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaQdDauTuChiPhiMapper : Profile
    {
        public NhDaQdDauTuChiPhiMapper()
        {
            CreateMap<NhDaQdDauTuChiPhi, NhDaQdDauTuChiPhiModel>();
            CreateMap<NhDmChiPhi, NhDaQdDauTuChiPhiModel>();
            CreateMap<NhDaQdDauTuChiPhiModel, NhDaQdDauTuChiPhi>();
        }
    }
}
