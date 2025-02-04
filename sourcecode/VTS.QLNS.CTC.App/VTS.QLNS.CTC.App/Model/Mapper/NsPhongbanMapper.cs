using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsPhongbanMapper : Profile
    {
        public NsPhongbanMapper()
        {
            CreateMap<DmBQuanLy, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.SKyHieu + " - " + z.STenBQuanLy))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIDMaBQuanLy));
            CreateMap<NSPhongBanModel, Core.Domain.DmBQuanLy>();
            CreateMap<DmBQuanLy, NSPhongBanModel>();
        }
    }
}
