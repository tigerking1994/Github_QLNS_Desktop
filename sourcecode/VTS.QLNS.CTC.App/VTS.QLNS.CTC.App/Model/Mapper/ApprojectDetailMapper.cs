using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ApprojectDetailMapper : Profile
    {
        public ApprojectDetailMapper()
        {
            CreateMap<Core.Domain.Query.ApproveProjectDetailQuery, ApproveProjectDetailModel>();
        }
    }
}
