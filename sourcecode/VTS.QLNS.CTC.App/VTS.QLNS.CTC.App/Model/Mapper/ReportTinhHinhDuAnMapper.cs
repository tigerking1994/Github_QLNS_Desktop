using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ReportTinhHinhDuAnMapper : Profile
    {
        public ReportTinhHinhDuAnMapper()
        {
            CreateMap<ReportTinhHinhDuAnQuery, ReportProcessProjectViewModel>();
        }
    }
}
