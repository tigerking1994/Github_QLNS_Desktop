using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ThDChungTuMapper : Profile
    {
        public ThDChungTuMapper()
        {
            CreateMap<ThDChungTuQuery, ExpertiseModel>();
            CreateMap<ExpertiseModel, NsSktNganhThamDinh>()
                .ForMember(model => model.Id, member => member.MapFrom(entity => entity.Id))
                .ForMember(model => model.IIdMaDonVi, member => member.MapFrom(entity => entity.IdDonVi))
                .ForMember(model => model.SSoChungTu, member => member.MapFrom(entity => entity.SoChungTu))
                .ForMember(model => model.DNgayChungTu, member => member.MapFrom(entity => entity.NgayChungTu))
                .ForMember(model => model.SSoQuyetDinh, member => member.MapFrom(entity => entity.SoQuyetDinh))
                .ForMember(model => model.DNgayQuyetDinh, member => member.MapFrom(entity => entity.NgayQuyetDinh))
                .ForMember(model => model.SMoTa, member => member.MapFrom(entity => entity.MoTa))
                .ForMember(model => model.BKhoa, member => member.MapFrom(entity => entity.IsLocked))
                .ForMember(model => model.INamLamViec, member => member.MapFrom(entity => entity.NamLamViec))
                .ForMember(model => model.SNguoiTao, member => member.MapFrom(entity => entity.UserCreator))
                .ForMember(model => model.INamNganSach, member => member.MapFrom(entity => entity.NamNganSach))
                .ForMember(model => model.IIdMaNguonNganSach, member => member.MapFrom(entity => entity.NguonNganSach))
                .ForMember(model => model.FTongTuChiCtc, member => member.MapFrom(entity => entity.TongTuChiCTC))
                .ForMember(model => model.FTongTuChiNganh, member => member.MapFrom(entity => entity.TongTuChiNganh));

            CreateMap<NsSktNganhThamDinh, ExpertiseModel>()
                .ForMember(model => model.Id, member => member.MapFrom(entity => entity.Id))
                .ForMember(model => model.IdDonVi, member => member.MapFrom(entity => entity.IIdMaDonVi))
                .ForMember(model => model.SoChungTu, member => member.MapFrom(entity => entity.SSoChungTu))
                .ForMember(model => model.NgayChungTu, member => member.MapFrom(entity => entity.DNgayChungTu))
                .ForMember(model => model.SoQuyetDinh, member => member.MapFrom(entity => entity.SSoQuyetDinh))
                .ForMember(model => model.NgayQuyetDinh, member => member.MapFrom(entity => entity.DNgayQuyetDinh))
                .ForMember(model => model.MoTa, member => member.MapFrom(entity => entity.SMoTa))
                .ForMember(model => model.IsLocked, member => member.MapFrom(entity => entity.BKhoa))
                .ForMember(model => model.NamLamViec, member => member.MapFrom(entity => entity.INamLamViec))
                .ForMember(model => model.UserCreator, member => member.MapFrom(entity => entity.SNguoiTao))
                .ForMember(model => model.NamNganSach, member => member.MapFrom(entity => entity.INamNganSach))
                .ForMember(model => model.NguonNganSach, member => member.MapFrom(entity => entity.IIdMaNguonNganSach))
                .ForMember(model => model.TongTuChiCTC, member => member.MapFrom(entity => entity.FTongTuChiCtc))
                .ForMember(model => model.TongTuChiNganh, member => member.MapFrom(entity => entity.FTongTuChiNganh));

            CreateMap<ThDChungTuChiTietQuery, ExpertiseModelDetailModel>();
            CreateMap<ExpertiseModelDetailModel, NsSktNganhThamDinhChiTiet>()
                .ForMember(model => model.Id, member => member.MapFrom(entity => entity.Id))
                .ForMember(model => model.IIdCtnganhThamDinh, member => member.MapFrom(entity => entity.IdChungTu))
                .ForMember(model => model.IIdMaDonVi, member => member.MapFrom(entity => entity.IdDonVi))
                .ForMember(model => model.STenDonVi, member => member.MapFrom(entity => entity.TenDonVi))
                .ForMember(model => model.IIdMucLuc, member => member.MapFrom(entity => entity.IdMucLuc))
                .ForMember(model => model.SM, member => member.MapFrom(entity => entity.M))
                .ForMember(model => model.SMoTa, member => member.MapFrom(entity => entity.MoTa))
                .ForMember(model => model.FTuChi, member => member.MapFrom(entity => entity.TuChi))
                .ForMember(model => model.FSuDungTonKho, member => member.MapFrom(entity => entity.SuDungTonKho))
                .ForMember(model => model.FChiDacThuNganhPhanCap, member => member.MapFrom(entity => entity.ChiDacThuNganhPhanCap))
                .ForMember(model => model.SGhiChu, member => member.MapFrom(entity => entity.GhiChu))
                .ForMember(model => model.INamLamViec, member => member.MapFrom(entity => entity.NamLamViec))
                .ForMember(model => model.DNgayTao, member => member.MapFrom(entity => entity.DateCreated))
                .ForMember(model => model.SNguoiTao, member => member.MapFrom(entity => entity.UserCreator))
                .ForMember(model => model.INamNganSach, member => member.MapFrom(entity => entity.NamNganSach))
                .ForMember(model => model.IIdMaNguonNganSach, member => member.MapFrom(entity => entity.NguonNganSach))
                .ForMember(model => model.Id, member => member.MapFrom(entity => entity.IdDb));
        }
    }
}
