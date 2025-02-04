using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaDutoanDetailMapper:  Profile
    {
        public VdtDaDutoanDetailMapper()
        {
            CreateMap<Core.Domain.Query.DuToanDetailQuery, DuToanDetailModel>();
        }
    }
}
