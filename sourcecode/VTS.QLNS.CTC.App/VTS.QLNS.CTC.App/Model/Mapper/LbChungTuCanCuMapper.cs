using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class LbChungTuCanCuMapper : Profile
    {
        public LbChungTuCanCuMapper()
        {
            CreateMap<Core.Domain.Query.LbChungTuCanCuQuery, LbChungTuCanCuModel>();
        }
    }
}
