using AutoMapper;
using System;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DonViMapper : Profile
    {
        public DonViMapper()
        {
            CreateMap<DonVi, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.IIDMaDonVi.PadLeft(3, '0')} - {item.TenDonVi}"))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.Id.ToString()))
                .ForMember(entity => entity.DisplayItemOption2, model => model.MapFrom(item => item.TenDonVi))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIDMaDonVi));
            CreateMap<DonVi, AgencyModel>()
                .ForMember(entity => entity.AgencyName, model => model.MapFrom(item => $"{item.IIDMaDonVi.PadLeft(3, '0')} - {item.TenDonVi}"))
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IIDMaDonVi));
            CreateMap<DonVi, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.IIDMaDonVi.PadLeft(3,'0')} - {item.TenDonVi}"))
                .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.TenDonVi))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIDMaDonVi));
            CreateMap<CheckBoxItem, DonVi>()
                .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.DisplayItem))
                .ForMember(entity => entity.IIDMaDonVi, model => model.MapFrom(item => item.ValueItem));
            CreateMap<DonViNgayChungTuQuery, CheckBoxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.IdDonVi.PadLeft(3, '0')} - {item.TenDonVi}"))
               .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.TenDonVi))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IdDonVi));
            CreateMap<DonVi, DonViModel>();
            CreateMap<NsCpChungTu, ComboboxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Format("{0} Ngày: {1} QĐ: {2}", item.SSoChungTu, item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty,item.SSoQuyetDinh)))
               .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<DonViModel, DonVi>();
            CreateMap<AgencyModel, DonVi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => string.IsNullOrEmpty(item.Id) ? Guid.Empty : Guid.Empty));
            CreateMap<DonViModel, NhKhTongTheNhiemVuChiQuery>();
            CreateMap<NhKhTongTheNhiemVuChiQuery, DonViModel>();
            CreateMap<DonViModel, NhKhTongTheNhiemVuChi>();
            CreateMap<NhKhTongTheNhiemVuChi, DonViModel>();
            CreateMap<DonVi, NhKhTongTheNhiemVuChi>();
            CreateMap<NhKhTongTheNhiemVuChi, DonVi>();
        }
    }
}
