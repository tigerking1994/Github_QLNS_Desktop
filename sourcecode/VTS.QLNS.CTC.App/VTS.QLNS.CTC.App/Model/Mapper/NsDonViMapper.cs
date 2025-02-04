using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsDonViMapper : Profile
    {
        public NsDonViMapper()
        {
            CreateMap<Core.Domain.DonVi, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => $"{z.IIDMaDonVi.PadLeft(3, '0')} - {z.TenDonVi}"))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.Id.ToString()))
                .ForMember(x => x.DisplayItemOption2, y => y.MapFrom(z => z.TenDonVi))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIDMaDonVi));
            CreateMap<DonViModel, Core.Domain.DonVi>();
            CreateMap<Core.Domain.DonVi, DonViModel>();
            CreateMap<DanhMucDonViImportModel, Core.Domain.DonVi>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => string.IsNullOrEmpty(z.Id) ? Guid.Empty : Guid.Parse(z.Id)))
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.IIDMaDonVi, y => y.MapFrom(z => z.IIDMaDonVi))
                .ForMember(x => x.TenDonVi, y => y.MapFrom(z => z.TenDonVi))
                .ForMember(x => x.KyHieu, y => y.MapFrom(z => z.KyHieu))
                .ForMember(x => x.MoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.Loai, y => y.MapFrom(z => string.IsNullOrEmpty(z.Loai) ? 0 : int.Parse(z.Loai)))
                .ForMember(x => x.NamLamViec, y => y.MapFrom(z => int.Parse(z.NamLamViec)));
            CreateMap<Core.Domain.Query.DonViQuery, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.TenDonVi))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IdDonVi));
        }
    }
}
