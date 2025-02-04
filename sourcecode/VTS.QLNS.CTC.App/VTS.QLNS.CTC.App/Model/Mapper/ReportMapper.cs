using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ReportMapper : Profile
    {
        public ReportMapper()
        {
            CreateMap<NsQtChungTuChiTietGiaiThich, RptQuyetToanGiaiThichSo>();
        }
    }
}
