using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AllocationDetailMapper : Profile
    {
        public AllocationDetailMapper()
        {
            CreateMap<Core.Domain.NsCpChungTuChiTiet, AllocationDetailModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IdChungTu, model => model.MapFrom(item => item.IIdCtcapPhat))
                .ForMember(entity => entity.MlnsId, model => model.MapFrom(item => item.IIdMlns))
                .ForMember(entity => entity.MlnsIdParent, model => model.MapFrom(item => item.IIdParentCha))
                .ForMember(entity => entity.XauNoiMa, model => model.MapFrom(item => item.SXauNoiMa))
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.SLns))
                .ForMember(entity => entity.L, model => model.MapFrom(item => item.SL))
                .ForMember(entity => entity.K, model => model.MapFrom(item => item.SK))
                .ForMember(entity => entity.M, model => model.MapFrom(item => item.SM))
                .ForMember(entity => entity.Tm, model => model.MapFrom(item => item.STm))
                .ForMember(entity => entity.Ttm, model => model.MapFrom(item => item.STtm))
                .ForMember(entity => entity.Ng, model => model.MapFrom(item => item.SNg))
                .ForMember(entity => entity.Tng, model => model.MapFrom(item => item.STng))
                .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.SMoTa))
                .ForMember(entity => entity.Chuong, model => model.MapFrom(item => item.SChuong))
                .ForMember(entity => entity.NamLamViec, model => model.MapFrom(item => item.INamLamViec))
                .ForMember(entity => entity.NamNganSach, model => model.MapFrom(item => item.INamNganSach))
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(item => item.BHangCha))
                .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.ILoai))
                .ForMember(entity => entity.IdDonVi, model => model.MapFrom(item => item.IIdMaDonVi))
                .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.STenDonVi))
                .ForMember(entity => entity.TuChi, model => model.MapFrom(item => item.FTuChi))
                .ForMember(entity => entity.HienVat, model => model.MapFrom(item => item.FHienVat))
                .ForMember(entity => entity.GhiChu, model => model.MapFrom(item => item.SGhiChu))
                .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                .ForMember(entity => entity.UserCreator, model => model.MapFrom(item => item.SNguoiTao))
                .ForMember(entity => entity.DateModified, model => model.MapFrom(item => item.DNgaySua))
                .ForMember(entity => entity.UserModifier, model => model.MapFrom(item => item.SNguoiSua))
                .ForMember(entity => entity.NguonNganSach, model => model.MapFrom(item => item.IIdMaNguonNganSach));
            CreateMap<CpChungTuChiTietQuery, AllocationDetailModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(item => item.BHangCha));
            CreateMap<AllocationDetailModel, Core.Domain.NsCpChungTuChiTiet>()
              .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
              .ForMember(entity => entity.IIdCtcapPhat, model => model.MapFrom(item => item.IdChungTu))
              .ForMember(entity => entity.IIdMlns, model => model.MapFrom(item => item.MlnsId))
              .ForMember(entity => entity.IIdParentCha, model => model.MapFrom(item => item.MlnsIdParent))
              .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
              .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.Lns))
              .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
              .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
              .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
              .ForMember(entity => entity.STm, model => model.MapFrom(item => item.Tm))
              .ForMember(entity => entity.STtm, model => model.MapFrom(item => item.Ttm))
              .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.Ng))
              .ForMember(entity => entity.STng, model => model.MapFrom(item => item.Tng))
              .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
              .ForMember(entity => entity.SChuong, model => model.MapFrom(item => item.Chuong))
              .ForMember(entity => entity.INamLamViec, model => model.MapFrom(item => item.NamLamViec))
              .ForMember(entity => entity.INamNganSach, model => model.MapFrom(item => item.NamNganSach))
              .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.IsHangCha))
              .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.ILoai))
              .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))
              .ForMember(entity => entity.STenDonVi, model => model.MapFrom(item => item.TenDonVi))
              .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.TuChi))
              .ForMember(entity => entity.FDeNghiDonVi, model => model.MapFrom(item => item.DeNghiDonVi))
              .ForMember(entity => entity.FHienVat, model => model.MapFrom(item => item.HienVat))
              .ForMember(entity => entity.SGhiChu, model => model.MapFrom(item => item.GhiChu))
              .ForMember(entity => entity.DNgayTao, model => model.MapFrom(item => item.DateCreated))
              .ForMember(entity => entity.SNguoiTao, model => model.MapFrom(item => item.UserCreator))
              .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
              .ForMember(entity => entity.SNguoiSua, model => model.MapFrom(item => item.UserModifier))
              .ForMember(entity => entity.IIdMaNguonNganSach, model => model.MapFrom(item => item.NguonNganSach));

            CreateMap<NsCpChungTuChiTiet, NsCpChungTuChiTietCustomModel>();

            CreateMap<NsCpChungTuChiTiet, CpChungTuChiTietQuery>()
                  .ForMember(entity => entity.IdChungTu, model => model.MapFrom(item => item.IIdCtcapPhat))
                  .ForMember(entity => entity.MlnsId, model => model.MapFrom(item => item.IIdMlns))
                  .ForMember(entity => entity.MlnsIdParent, model => model.MapFrom(item => item.IIdParentCha))
                  .ForMember(entity => entity.XauNoiMa, model => model.MapFrom(item => item.SXauNoiMa))
                  .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.SLns))
                  .ForMember(entity => entity.L, model => model.MapFrom(item => item.SL))
                  .ForMember(entity => entity.K, model => model.MapFrom(item => item.SK))
                  .ForMember(entity => entity.M, model => model.MapFrom(item => item.SM))
                  .ForMember(entity => entity.Tm, model => model.MapFrom(item => item.STm))
                  .ForMember(entity => entity.Ttm, model => model.MapFrom(item => item.STtm))
                  .ForMember(entity => entity.Ng, model => model.MapFrom(item => item.SNg))
                  .ForMember(entity => entity.Tng, model => model.MapFrom(item => item.STng))
                  .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.SMoTa))
                  .ForMember(entity => entity.Chuong, model => model.MapFrom(item => item.SChuong))
                  .ForMember(entity => entity.NamLamViec, model => model.MapFrom(item => item.INamLamViec))
                  .ForMember(entity => entity.NamNganSach, model => model.MapFrom(item => item.INamNganSach))
                  .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.BHangCha))
                  .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.ILoai))
                  .ForMember(entity => entity.IdDonVi, model => model.MapFrom(item => item.IIdMaDonVi))
                  .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.STenDonVi))
                  .ForMember(entity => entity.TuChi, model => model.MapFrom(item => item.FTuChi))
                  .ForMember(entity => entity.HienVat, model => model.MapFrom(item => item.FHienVat))
                  .ForMember(entity => entity.GhiChu, model => model.MapFrom(item => item.SGhiChu))
                  .ForMember(entity => entity.UserCreator, model => model.MapFrom(item => item.SNguoiTao))
                  .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                  .ForMember(entity => entity.NguonNganSach, model => model.MapFrom(item => item.IIdMaNguonNganSach))
                  .ForMember(entity => entity.DeNghiDonVi, model => model.MapFrom(item => item.FDeNghiDonVi))
                  .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.BHangCha));
        }
    }
}
