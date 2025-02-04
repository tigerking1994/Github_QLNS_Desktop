using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsNguonNSMapper: Profile
    {
        public NsNguonNSMapper()
        {
            CreateMap<Core.Domain.NsNguonNganSach, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STen))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdMaNguonNganSach));
        }
    }
}
