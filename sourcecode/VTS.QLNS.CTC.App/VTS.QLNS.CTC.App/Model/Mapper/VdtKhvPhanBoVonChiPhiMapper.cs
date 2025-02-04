using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvPhanBoVonChiPhiMapper : Profile
    {
        public VdtKhvPhanBoVonChiPhiMapper() 
        {
            CreateMap<VdtKhvPhanBoVonChiPhi, VdtKhvPhanBoVonChiPhiModel>();
            CreateMap<VdtKhvPhanBoVonChiPhiModel, VdtKhvPhanBoVonChiPhi>();
            CreateMap<VdtKhvPhanBoVonChiPhiQuery, VdtKhvPhanBoVonChiPhiModel>();
        }
    }
}
