using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ProjectAdjustementMapper : Profile
    {
        public ProjectAdjustementMapper()
        {
            CreateMap<VdtKhvPhanBoVonChiTietProAdjustementReportQuery, ProAdjustementReport>();
        }
    }
}
