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
    public class VdtTtDeNghiThanhToanChiPhiMapper : Profile
    {
        public VdtTtDeNghiThanhToanChiPhiMapper()
        {
            CreateMap<VdtTtDeNghiThanhToanChiPhiIndexQuery, VdtTtDeNghiThanhToanChiPhiIndexModel>();
            CreateMap<VdtTtDeNghiThanhToanChiPhiIndexModel, VdtTtDeNghiThanhToanChiPhiIndexQuery>();

            CreateMap<VdtTtDeNghiThanhToanChiPhiChiTietModel, VdtTtDeNghiThanhToanChiPhiChiTietQuery>();
            CreateMap<VdtTtDeNghiThanhToanChiPhiChiTietQuery, VdtTtDeNghiThanhToanChiPhiChiTietModel>();

            CreateMap<VdtTtDeNghiThanhToanChiPhiIndexModel, VdtTtDeNghiThanhToanChiPhi>();
            CreateMap<VdtTtDeNghiThanhToanChiPhi, VdtTtDeNghiThanhToanChiPhiIndexModel>();
        }
    }
}
