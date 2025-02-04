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
    public class VdtKhvPhanBoVonChiPhiChiTietMapper : Profile
    {
        public VdtKhvPhanBoVonChiPhiChiTietMapper()
        {
            CreateMap<VdtKhvPhanBoVonChiPhiChiTiet, VdtKhvPhanBoVonChiPhiChiTietModel>();
            CreateMap<VdtKhvPhanBoVonChiPhiChiTietModel, VdtKhvPhanBoVonChiPhiChiTiet>();
            CreateMap<VdtKhvPhanBoVonChiPhiChiTietQuery, VdtKhvPhanBoVonChiPhiChiTietModel>();
        }
    }
}
