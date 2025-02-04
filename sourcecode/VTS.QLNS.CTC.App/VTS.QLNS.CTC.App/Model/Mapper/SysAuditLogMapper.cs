using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SysAuditLogMapper : Profile
    {
        public SysAuditLogMapper()
        {
            CreateMap<Core.Domain.HtNhatKyCapNhatDuLieu, HTNhatKyCapNhatDuLieuModel>();
        }
    }
}
