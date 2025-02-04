using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class CauHinhCanCuMapper : Profile
    {
        public CauHinhCanCuMapper()
        {
            CreateMap<CauHinhCanCuModel, Core.Domain.NsCauHinhCanCu>();
            CreateMap<Core.Domain.NsCauHinhCanCu, CauHinhCanCuModel>();
        }
    }
}
