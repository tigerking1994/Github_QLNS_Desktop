using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCapBacNq104Mapper : Profile
    {
        public TlDmCapBacNq104Mapper()
        {
            CreateMap<TlDmCapBacNq104, TlDmCapBacNq104Model>();
            CreateMap<TlDmCapBacNq104Model, TlDmCapBacNq104>();
            CreateMap<TlDmCapBacNq104, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.TenCb))
                .ForMember(x => x.ValueItem, y => y.MapFrom(y => y.MaCb));
            CreateMap<TlDmCapBacNq104Model, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.TenCb))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.MaCb))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.Note));
            CreateMap<ComboboxItem, TlDmCapBacNq104Model>()
                .ForMember(x => x.TenCb, y => y.MapFrom(z => z.DisplayItem))
                .ForMember(x => x.MaCb, y => y.MapFrom(z => z.ValueItem))
                .ForMember(x => x.Note, y => y.MapFrom(z => z.HiddenValue));

            CreateMap<TlDmCapBacNq104, TlDmCapBacNq104Model>().ReverseMap();
            CreateMap<TlDmCapBacLuongNq104, TlDmCapBacLuongNq104Model>().ReverseMap();

            CreateMap<TlDmCapBacLuongNq104Model, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.LoaiNhom))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.MaLoai))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.MaNhom))
                .ForMember(x => x.DisplayItemOption2, y => y.MapFrom(z => z.CapBacLuong))
                .ForMember(x => x.HiddenValueOption2, y => y.MapFrom(z => z.MaDm))
                .ForMember(x => x.HiddenValueOption3, y => y.MapFrom(x => x.MaLoai + "-" + x.MaNhom));
        }
    }
}
