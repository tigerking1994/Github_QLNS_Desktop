using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmLoaiCongTrinhMapper : Profile
    {
        public DmLoaiCongTrinhMapper()
        {
            CreateMap<Core.Domain.VdtDmLoaiCongTrinh, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenLoaiCongTrinh))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdLoaiCongTrinh));
        }
    }
}
