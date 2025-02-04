using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ProjectManagerDetailMapper: Profile
    {
        public ProjectManagerDetailMapper()
        {
            CreateMap<Core.Domain.Query.ProjectManagerDetailQuery, ProjectManagerDetailModel>();
        }
    }
}
