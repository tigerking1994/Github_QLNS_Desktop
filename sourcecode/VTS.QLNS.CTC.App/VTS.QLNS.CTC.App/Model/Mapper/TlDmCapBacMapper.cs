using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCapBacMapper : Profile
    {
        public TlDmCapBacMapper()
        {
            CreateMap<TlDmCapBac, TlDmCapBacModel>();
            CreateMap<TlDmCapBacModel, TlDmCapBac>();
            CreateMap<TlDmCapBac, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.Note))
                .ForMember(x => x.ValueItem, y => y.MapFrom(y => y.MaCb));
            CreateMap<TlDmCapBacModel, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.MaCb))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.TenCb))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.Note));
            CreateMap<ComboboxItem, TlDmCapBacModel>()
                .ForMember(x => x.MaCb, y => y.MapFrom(z => z.DisplayItem))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ValueItem))
                .ForMember(x => x.TenCb, y => y.MapFrom(z => z.HiddenValue));

            //CreateMap<TlDmCapBacNq104, TlDmCapBacNq104Model>().ReverseMap();
            //CreateMap<TlDmCapBacLuongNq104, TlDmCapBacLuongNq104Model>().ReverseMap();
        }
    }
}
