using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AllocationMapper : Profile
    {
        public AllocationMapper()
        {
            CreateMap<Core.Domain.NsCpChungTu, AllocationModel>()
                .ForMember(entity => entity.SoChungTu, model => model.MapFrom(item => item.SSoChungTu))
                .ForMember(entity => entity.SoChungTuIndex, model => model.MapFrom(item => item.ISoChungTuIndex))
                .ForMember(entity => entity.NgayChungTu, model => model.MapFrom(item => item.DNgayChungTu))
                .ForMember(entity => entity.SoQuyetDinh, model => model.MapFrom(item => item.SSoQuyetDinh))
                .ForMember(entity => entity.NgayQuyetDinh, model => model.MapFrom(item => item.DNgayQuyetDinh))
                .ForMember(entity => entity.IdDonVi, model => model.MapFrom(item => item.SDsidMaDonVi))
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.SDslns))
                .ForMember(entity => entity.ITypeMoTa, model => model.MapFrom(item => item.ITypeMoTa))
                .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.ILoai))
                .ForMember(entity => entity.NamLamViec, model => model.MapFrom(item => item.INamLamViec))
                .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                .ForMember(entity => entity.UserCreator, model => model.MapFrom(item => item.SNguoiTao))
                .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                .ForMember(entity => entity.UserModifier, model => model.MapFrom(item => item.SNguoiSua))
                .ForMember(entity => entity.IsLocked, model => model.MapFrom(item => item.BKhoa))
                .ForMember(entity => entity.ChiTietToi, model => model.MapFrom(item => item.NChiTietToi))
                .ForMember(entity => entity.NguonNganSach, model => model.MapFrom(item => item.IIdMaNguonNganSach))
                .ForMember(entity => entity.NamNganSach, model => model.MapFrom(item => item.INamNganSach))
                .ForMember(entity => entity.NgayChungTuString, model => model.MapFrom(item => item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<Core.Domain.Query.CpChungTuQuery, AllocationModel>()
                .ForMember(entity => entity.NgayChungTuString, model => model.MapFrom(item => item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayQuyetDinhString, model => model.MapFrom(item => item.NgayQuyetDinh.HasValue ? item.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<Core.Domain.Query.CpChungTuQuery, ComboboxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Format("{0} Ngày: {1}", item.SoChungTu, item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty)))
               .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SoChungTu));
            CreateMap<AllocationModel, Core.Domain.NsCpChungTu>()
                .ForMember(entity => entity.SSoChungTu, model => model.MapFrom(item => item.SoChungTu))
                .ForMember(entity => entity.ISoChungTuIndex, model => model.MapFrom(item => item.SoChungTuIndex))
                .ForMember(entity => entity.DNgayChungTu, model => model.MapFrom(item => item.NgayChungTu))
                .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(item => item.SoQuyetDinh))
                .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(item => item.NgayQuyetDinh))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.SDsidMaDonVi, model => model.MapFrom(item => item.IdDonVi))
                .ForMember(entity => entity.SDslns, model => model.MapFrom(item => item.Lns))
                .ForMember(entity => entity.ITypeMoTa, model => model.MapFrom(item => item.ITypeMoTa))
                .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.ILoai))
                .ForMember(entity => entity.INamLamViec, model => model.MapFrom(item => item.NamLamViec))
                .ForMember(entity => entity.DNgayTao, model => model.MapFrom(item => item.DateCreated))
                .ForMember(entity => entity.SNguoiTao, model => model.MapFrom(item => item.UserCreator))
                .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
                .ForMember(entity => entity.SNguoiSua, model => model.MapFrom(item => item.UserModifier))
                .ForMember(entity => entity.BKhoa, model => model.MapFrom(item => item.IsLocked))
                .ForMember(entity => entity.NChiTietToi, model => model.MapFrom(item => item.ChiTietToi))
                .ForMember(entity => entity.IIdMaNguonNganSach, model => model.MapFrom(item => item.NguonNganSach))
                .ForMember(entity => entity.INamNganSach, model => model.MapFrom(item => item.NamNganSach));

            CreateMap<NsCpChungTu, ChungTuCanCuModel>()
                .ForMember(model => model.SoChungTu, member => member.MapFrom(entity => entity.SSoChungTu))
                .ForMember(model => model.NgayChungTu, member => member.MapFrom(entity => entity.DNgayChungTu))
                .ForMember(model => model.SoQuyetDinh, member => member.MapFrom(entity => entity.SSoQuyetDinh))
                .ForMember(model => model.NgayQuyetDinh, member => member.MapFrom(entity => entity.DNgayQuyetDinh));
        }
    }
}
