using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDsCBNHKeHoachMapper : Profile
    {
        public TlDsCBNHKeHoachMapper()
        {
            CreateMap<TlDsCBNHKeHoach, TlDsCBNHKeHoachModel>()
                .ForMember(entity => entity.CapBac, model => model.MapFrom(item => item.TlDmCapBac != null ? item.TlDmCapBac.Note : string.Empty))
                .ForMember(entity => entity.ChucVu, model => model.MapFrom(item => item.TlDmChucVu != null ? item.TlDmChucVu.TenCv : string.Empty));
            CreateMap<TlDsCBNHKeHoachModel, TlDsCBNHKeHoach>();
        }
    }
}
