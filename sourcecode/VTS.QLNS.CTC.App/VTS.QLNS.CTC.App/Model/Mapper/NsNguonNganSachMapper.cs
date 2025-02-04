using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsNguonNganSachMapper : Profile
    {
        public NsNguonNganSachMapper()
        {
            CreateMap<NguonNganSachModel, CheckBoxItem>()
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(x => x.IIdMaNguonNganSach.ToString()))
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(x => x.STen));
            CreateMap<NguonNganSachModel, ComboboxItem>()
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(x => x.IIdMaNguonNganSach.ToString()))
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(x => x.STen));
            CreateMap<Core.Domain.NsNguonNganSach, NguonNganSachModel>();
            CreateMap<NguonNganSachModel, Core.Domain.NsNguonNganSach>();
        }
    }
}
