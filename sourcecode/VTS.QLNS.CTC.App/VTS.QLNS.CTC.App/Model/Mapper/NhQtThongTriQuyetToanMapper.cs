using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhQtThongTriQuyetToanMapper : Profile
    {
        public NhQtThongTriQuyetToanMapper()
        {
            CreateMap<NhQtThongTriQuyetToanQuery, NhQtThongTriQuyetToanModel>().ReverseMap();
            CreateMap<NhQtThongTriQuyetToanChiTietQuery, NhQtThongTriQuyetToanChiTietModel>().ReverseMap();
            CreateMap<NhQtThongTriQuyetToanChiTietQuery, RptThongTriQuyetToan>().ReverseMap();
        }
    }
}
