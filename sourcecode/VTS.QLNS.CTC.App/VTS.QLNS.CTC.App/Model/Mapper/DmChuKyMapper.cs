using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmChuKyMapper : Profile
    {
        public DmChuKyMapper()
        {
            CreateMap<DmChuKyModel, Core.Domain.DmChuKy>();
            CreateMap<Core.Domain.DmChuKy, DmChuKyModel>();
        }
    }
}
