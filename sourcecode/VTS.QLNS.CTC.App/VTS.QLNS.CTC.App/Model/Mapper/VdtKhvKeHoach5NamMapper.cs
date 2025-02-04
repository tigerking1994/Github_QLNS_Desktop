using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvKeHoach5NamMapper : Profile
    {
        public VdtKhvKeHoach5NamMapper()
        {
            CreateMap<VdtKhvKeHoach5Nam, ComboboxItem>()
              .ForMember(entity => entity.ValueItem, model => model.MapFrom(x => x.Id))
              .ForMember(entity => entity.DisplayItem, model => model.MapFrom(x => string.Format("{0} - {1} - {2}", x.SSoQuyetDinh, x.IGiaiDoanTu, x.IGiaiDoanDen)));

            CreateMap<VdtKhvKeHoach5Nam, VdtKhvKeHoach5NamModel>()
                .ForMember(entity => entity.FGiaTriKeHoach, model => model.MapFrom(x => x.FGiaTriDuocDuyet))
                .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(x => x.IIdMaDonViQuanLy))
                .ForMember(entity => entity.IIdDonViId, model => model.MapFrom(x => x.IIdDonViQuanLyId));

            CreateMap<VdtKhvKeHoach5NamModel, VdtKhvKeHoach5Nam>()
                .ForMember(entity => entity.FGiaTriDuocDuyet, model => model.MapFrom(x => x.FGiaTriKeHoach))
                .ForMember(entity => entity.IIdMaDonViQuanLy, model => model.MapFrom(x => x.IIdMaDonVi))
                .ForMember(entity => entity.IIdDonViQuanLyId, model => model.MapFrom(x => x.IIdDonViId));

            CreateMap<VdtKhvKeHoach5NamModel, VdtKhvKeHoach5NamQuery>()
                .ForMember(entity => entity.IIdKeHoach5NamId, model => model.MapFrom(x => x.Id))
                .ForMember(entity => entity.GiaiDoanTu, model => model.MapFrom(x => x.IGiaiDoanTu))
                .ForMember(entity => entity.GiaiDoanDen, model => model.MapFrom(x => x.IGiaiDoanDen))
                .ForMember(entity => entity.SoKeHoach, model => model.MapFrom(x => x.SSoQuyetDinh))
                .ForMember(entity => entity.NgayLap, model => model.MapFrom(x => x.DNgayQuyetDinh));
            CreateMap<VdtKhvKeHoach5NamQuery, VdtKhvKeHoach5NamModel>()
                .ForMember(entity => entity.GiaiDoan, model => model.MapFrom(x => string.Format("{0}-{1}", x.GiaiDoanTu, x.GiaiDoanDen)))
                .ForMember(entity => entity.Id, model => model.MapFrom(x => x.IIdKeHoach5NamId))
                .ForMember(entity => entity.IGiaiDoanTu, model => model.MapFrom(x => x.GiaiDoanTu))
                .ForMember(entity => entity.IGiaiDoanDen, model => model.MapFrom(x => x.GiaiDoanDen))
                .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(x => x.SoKeHoach))
                .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(x => x.NgayLap));
        }
    }
}
