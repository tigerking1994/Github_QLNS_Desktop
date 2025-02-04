using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmChuDauTuMapper : Profile
    {
        public DmChuDauTuMapper()
        {
            CreateMap<DmChuDauTu, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(x => string.Format("{0} - {1}", x.IIDMaDonVi, x.STenDonVi)))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(x => x.IIDMaDonVi))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(x => x.Id));
            CreateMap<DmChuDauTuModel, DmChuDauTu>();
            CreateMap<DmChuDauTu, DmChuDauTuModel>();
        }
    }
}
