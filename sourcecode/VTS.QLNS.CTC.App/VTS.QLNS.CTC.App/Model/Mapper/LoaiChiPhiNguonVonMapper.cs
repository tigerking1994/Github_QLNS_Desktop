using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class LoaiChiPhiNguonVonMapper : Profile
    {
        public LoaiChiPhiNguonVonMapper()
        {
            CreateMap<Model.LoaiChiPhiNguonVonModel, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.TenLoai))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Loai));
        }
    }
}
