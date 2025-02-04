using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvPhanBoVonDonViChiTietPheDuyetMapper : Profile
    {
        public VdtKhvPhanBoVonDonViChiTietPheDuyetMapper()
        {
            CreateMap<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery, PhanBoVonDonViChiTietPheDuyetQuery>();
            CreateMap<VdtKhvPhanBoVonDonViChiTietPheDuyetModel, PhanBoVonDonViChiTietPheDuyetQuery>();
            CreateMap<PhanBoVonDonViChiTietPheDuyetQuery, VdtKhvPhanBoVonDonViChiTietPheDuyetModel>()
                .ForMember(n => n.fVonConLai, m => m.MapFrom(n => n.fGiaTriDauTu - n.fVonDaBoTri));

            CreateMap<VdtKhvPhanBoVonDonViChiTietPheDuyetModel, PhanBoVonDonViChiTietPheDuyetQuery>();
        }
    }
}
