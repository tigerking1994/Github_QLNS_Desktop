using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaGoiThauDetailMapper : Profile
    {
        public VdtDaGoiThauDetailMapper()
        {
            CreateMap<Core.Domain.Query.VdtDaGoiThauDetailQuery, GoiThauDetailModel>();
        }
    }
}
