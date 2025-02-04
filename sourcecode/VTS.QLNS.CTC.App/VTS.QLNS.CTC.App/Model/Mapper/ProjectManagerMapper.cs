using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class ProjectManagerMapper: Profile
    {
        public ProjectManagerMapper()
        {
            CreateMap<Core.Domain.VdtDaDuAn, ProjectManagerModel>();
            CreateMap<Core.Domain.Query.ProjectManagerQuery, ProjectManagerModel>();
            CreateMap<ProjectManagerModel,Core.Domain.VdtDaDuAn>();
            CreateMap<ReportTongHopThongTinDuAnQuery, RptTongHopThongTinDuAn>()
                .ForMember(entity => entity.SDuAnDonVi, model => model.MapFrom(x => string.Format("{0} - {1}", x.STenDuAn, x.TenDonVi)));
            CreateMap<ReportChiTieuCapPhatDuAnQuery, RptChiTieuCapPhatDuAn>();
            CreateMap<ReportDuToanNSQPNamQuery, RptDuToanNSQPNam>();
        }
    }
}
