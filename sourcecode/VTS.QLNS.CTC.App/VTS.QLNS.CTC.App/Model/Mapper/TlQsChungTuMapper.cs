using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQsChungTuMapper : Profile
    {
        public TlQsChungTuMapper()
        {
            CreateMap<TlQsChungTuModel, TlQsChungTu>();
            CreateMap<TlQsChungTu, TlQsChungTuModel>()
                .ForMember(entity => entity.NgayTaoString, model => model.MapFrom(x => x.NgayTao.ToString("dd/MM/yyyy")));
        }
    }
}
