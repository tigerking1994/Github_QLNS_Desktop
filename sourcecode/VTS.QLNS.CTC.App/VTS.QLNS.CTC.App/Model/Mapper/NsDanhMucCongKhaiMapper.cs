using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsDanhMucCongKhaiMapper : Profile
    {
        public NsDanhMucCongKhaiMapper()
        {
            CreateMap<NsDanhMucCongKhai, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => z.sMoTa))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Id));

        }
    }
}
