using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmLoaiCongTrinhMapper : Profile
    {
        public NhDmLoaiCongTrinhMapper()
        {
            CreateMap<Core.Domain.NhDmLoaiCongTrinh, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenLoaiCongTrinh))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.SMaLoaiCongTrinh))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));

            CreateMap<NhDmLoaiCongTrinh, NhDmLoaiCongTrinhModel>();
            CreateMap<NhDmLoaiCongTrinhModel, NhDmLoaiCongTrinh>();
        }
    }
}
