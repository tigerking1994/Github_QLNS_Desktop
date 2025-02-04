using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VDTHinhThucQLMapper : Profile
    {
        public VDTHinhThucQLMapper()
        {
            CreateMap<Core.Domain.VdtDmHinhThucQuanLy, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenHinhThucQuanLy))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdHinhThucQuanLyId));
        }
    }
}
