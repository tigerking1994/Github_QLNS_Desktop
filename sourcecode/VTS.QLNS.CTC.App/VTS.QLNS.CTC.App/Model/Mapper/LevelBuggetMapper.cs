using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class LevelBuggetMapper : Profile
    {
        public LevelBuggetMapper()
        {
            CreateMap<Core.Domain.Query.LbChungTuQuery, LevelBuggetModel>()
                .ForMember(entity => entity.NgayChungTuString, model => model.MapFrom(item => item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayQuyetDinhString, model => model.MapFrom(item => item.NgayQuyetDinh.HasValue ? item.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<LevelBuggetModel, Core.Domain.NsNganhChungTu>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.SSoChungTu, model => model.MapFrom(item => item.SoChungTu))
                .ForMember(entity => entity.ISoChungTuIndex, model => model.MapFrom(item => item.SoChungTuIndex))
                .ForMember(entity => entity.DNgayChungTu, model => model.MapFrom(item => item.NgayChungTu))
                //.ForMember(entity => entity.SSoCongVan, model => model.MapFrom(item => item.SoCongVan))
                .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(item => item.NgayQuyetDinh))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))
                .ForMember(entity => entity.SDslns, model => model.MapFrom(item => item.Lns))
                .ForMember(entity => entity.INamLamViec, model => model.MapFrom(item => item.NamLamViec))
                .ForMember(entity => entity.DNgayTao, model => model.MapFrom(item => item.DateCreated))
                .ForMember(entity => entity.SNguoiTao, model => model.MapFrom(item => item.UserCreator))
                .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
                .ForMember(entity => entity.SNguoiSua, model => model.MapFrom(item => item.UserModifier))
                .ForMember(entity => entity.BKhoa, model => model.MapFrom(item => item.IsLocked))
                .ForMember(entity => entity.IIdMaNguonNganSach, model => model.MapFrom(item => item.NguonNganSach))
                .ForMember(entity => entity.FTongTuChi, model => model.MapFrom(item => item.TongTuChi))
                .ForMember(entity => entity.ILoaiChungTu, model => model.MapFrom(item => item.LoaiChungTu))
                .ForMember(entity => entity.INamNganSach, model => model.MapFrom(item => item.NamNganSach))
                .ForMember(entity => entity.FTongHangNhap, model => model.MapFrom(item => item.TongHangNhap))
                .ForMember(entity => entity.FTongHangMua, model => model.MapFrom(item => item.TongHangMua))
                .ForMember(entity => entity.FTongPhanCap, model => model.MapFrom(item => item.TongPhanCap));
            CreateMap<Core.Domain.NsNganhChungTu, LevelBuggetModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.SoChungTu, model => model.MapFrom(item => item.SSoChungTu))
                .ForMember(entity => entity.SoChungTuIndex, model => model.MapFrom(item => item.ISoChungTuIndex))
                .ForMember(entity => entity.NgayChungTu, model => model.MapFrom(item => item.DNgayChungTu))
                //.ForMember(entity => entity.SoCongVan, model => model.MapFrom(item => item.SSoCongVan))
                .ForMember(entity => entity.NgayQuyetDinh, model => model.MapFrom(item => item.DNgayQuyetDinh))
                .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.SMoTa))
                .ForMember(entity => entity.IdDonVi, model => model.MapFrom(item => item.IIdMaDonVi))
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.SDslns))
                .ForMember(entity => entity.NamLamViec, model => model.MapFrom(item => item.INamLamViec))
                .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                .ForMember(entity => entity.UserCreator, model => model.MapFrom(item => item.SNguoiTao))
                .ForMember(entity => entity.DateModified, model => model.MapFrom(item => item.DNgaySua))
                .ForMember(entity => entity.UserModifier, model => model.MapFrom(item => item.SNguoiSua))
                .ForMember(entity => entity.IsLocked, model => model.MapFrom(item => item.BKhoa))
                .ForMember(entity => entity.NguonNganSach, model => model.MapFrom(item => item.IIdMaNguonNganSach))
                .ForMember(entity => entity.TongTuChi, model => model.MapFrom(item => item.FTongTuChi))
                .ForMember(entity => entity.LoaiChungTu, model => model.MapFrom(item => item.ILoaiChungTu))
                .ForMember(entity => entity.NamNganSach, model => model.MapFrom(item => item.INamNganSach))
                .ForMember(entity => entity.TongHangNhap, model => model.MapFrom(item => item.FTongHangNhap))
                .ForMember(entity => entity.TongHangMua, model => model.MapFrom(item => item.FTongHangMua))
                .ForMember(entity => entity.TongPhanCap, model => model.MapFrom(item => item.FTongPhanCap));
        }
    }
}
