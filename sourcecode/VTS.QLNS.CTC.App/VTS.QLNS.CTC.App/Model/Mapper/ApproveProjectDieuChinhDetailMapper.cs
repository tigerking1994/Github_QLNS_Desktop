using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ApproveProjectDieuChinhDetailMapper : Profile
    {
        public ApproveProjectDieuChinhDetailMapper()
        {
            CreateMap<Core.Domain.Query.ApproveProjectDieuChinhDetailQuery, ApproveProjectDieuChinhDetailModel>();
        }
    }
}
