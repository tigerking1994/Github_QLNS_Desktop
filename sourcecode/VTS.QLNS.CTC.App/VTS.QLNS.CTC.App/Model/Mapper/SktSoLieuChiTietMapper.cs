using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SktSoLieuChiTietMapper : Profile
    {
        public SktSoLieuChiTietMapper()
        {
            CreateMap<Core.Domain.Query.SktSoLieuChiTietMlnsQuery, SktSoLieuChiTietMLNSModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IdDb, model => model.MapFrom(item => item.IdDb))
                .ForMember(entity => entity.MlnsId, model => model.MapFrom(item => item.MlnsId))
                .ForMember(entity => entity.MlnsIdParent, model => model.MapFrom(item => item.MlnsIdParent))
                .ForMember(entity => entity.XauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
                .ForMember(entity => entity.LNS, model => model.MapFrom(item => item.LNS))
                .ForMember(entity => entity.L, model => model.MapFrom(item => item.L))
                .ForMember(entity => entity.K, model => model.MapFrom(item => item.K))
                .ForMember(entity => entity.M, model => model.MapFrom(item => item.M))
                .ForMember(entity => entity.TM, model => model.MapFrom(item => item.TM))
                .ForMember(entity => entity.TTM, model => model.MapFrom(item => item.TTM))
                .ForMember(entity => entity.NG, model => model.MapFrom(item => item.NG))
                .ForMember(entity => entity.TNG, model => model.MapFrom(item => item.TNG))
                .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.Chuong, model => model.MapFrom(item => item.Chuong))
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(item => item.BHangCha))
                .ForMember(entity => entity.IdDonVi, model => model.MapFrom(item => item.IdDonVi))
                .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.TenDonVi))
                .ForMember(entity => entity.ChiTiet, model => model.MapFrom(item => item.ChiTiet))
                .ForMember(entity => entity.MucTienPhanBo, model => model.MapFrom(item => item.MucTienPhanBo))
                .ForMember(entity => entity.UocThucHien, model => model.MapFrom(item => item.UocThucHien))
                .ForMember(entity => entity.SKT_KyHieu, model => model.MapFrom(item => item.SKT_KyHieu));

            CreateMap<SktSoLieuChiTietMLNSModel, NsDtdauNamChungTuChiTiet>()
               .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
               .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.LNS))
               .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
               .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
               .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
               .ForMember(entity => entity.STm, model => model.MapFrom(item => item.TM))
               .ForMember(entity => entity.STtm, model => model.MapFrom(item => item.TTM))
               .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.NG))
               .ForMember(entity => entity.STng, model => model.MapFrom(item => item.TNG))
               .ForMember(entity => entity.STng1, model => model.MapFrom(item => item.TNG1))
               .ForMember(entity => entity.STng2, model => model.MapFrom(item => item.TNG2))
               .ForMember(entity => entity.STng3, model => model.MapFrom(item => item.TNG3))
               .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
               .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.ChiTiet))
               .ForMember(entity => entity.SChuong, model => model.MapFrom(item => item.Chuong))
               .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.IsHangCha))
               .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))

               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
               .ForMember(entity => entity.SChuong, model => model.MapFrom(item => item.Chuong))
               .ForMember(entity => entity.FHangNhap, model => model.MapFrom(item => item.HangNhap))
               .ForMember(entity => entity.FHangMua, model => model.MapFrom(item => item.HangMua))
               .ForMember(entity => entity.FPhanCap, model => model.MapFrom(item => item.PhanCap))
               .ForMember(entity => entity.FMucTienPhanBo, model => model.MapFrom(item => item.MucTienPhanBo))
               .ForMember(entity => entity.BKhoa, model => model.MapFrom(item => item.IsLocked))
               .ForMember(entity => entity.ILoaiChungTu, model => model.MapFrom(item => item.LoaiChungTu))
               .ForMember(entity => entity.FChuaPhanCap, model => model.MapFrom(item => item.ChuaPhanCap))
               .ForMember(entity => entity.STenDonVi, model => model.MapFrom(item => item.TenDonVi))
               .ForMember(entity => entity.FUocThucHien, model => model.MapFrom(item => item.UocThucHien));

            CreateMap<SktSoLieuChiTietMLNSModel, NsDtdauNamChungTuChiTietCanCu>()
              .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
              .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.LNS))
              .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
              .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
              .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
              .ForMember(entity => entity.STm, model => model.MapFrom(item => item.TM))
              .ForMember(entity => entity.STtm, model => model.MapFrom(item => item.TTM))
              .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.NG))
              .ForMember(entity => entity.STng, model => model.MapFrom(item => item.TNG))
              .ForMember(entity => entity.STng1, model => model.MapFrom(item => item.TNG1))
              .ForMember(entity => entity.STng2, model => model.MapFrom(item => item.TNG2))
              .ForMember(entity => entity.STng3, model => model.MapFrom(item => item.TNG3))
              .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
              .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.ChiTiet))
              .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.IsHangCha))
              .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))
              .ForMember(entity => entity.STenDonVi, model => model.MapFrom(item => item.TenDonVi));

            CreateMap<PlanBeginYearImportModel, NsDtdauNamChungTuChiTiet>()
                .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.LNS))
                .ForMember(entity => entity.STm, model => model.MapFrom(item => item.TM))
                .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
                .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
                .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
                .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.STtm, model => model.MapFrom(item => item.TTM))
                .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.NG))
                .ForMember(entity => entity.FUocThucHien, model => model.MapFrom(item => item.UocThucHien))

                .ForMember(entity => entity.FHangNhap, model => model.MapFrom(item => item.HangNhap))
                .ForMember(entity => entity.FHangMua, model => model.MapFrom(item => item.HangMua))
                .ForMember(entity => entity.FPhanCap, model => model.MapFrom(item => item.PhanCap))
                .ForMember(entity => entity.FChuaPhanCap, model => model.MapFrom(item => item.ChuaPhanCap))

                .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.ChiTiet));

            CreateMap<PlanBeginYearImportModel, ImpSktSoLieuChiTiet>()
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.LNS))
                .ForMember(entity => entity.Tm, model => model.MapFrom(item => item.TM))
                .ForMember(entity => entity.Ttm, model => model.MapFrom(item => item.TTM))
                .ForMember(entity => entity.Ng, model => model.MapFrom(item => item.NG))
                .ForMember(entity => entity.DuPhong, model => model.MapFrom(item => item.UocThucHien))
                .ForMember(entity => entity.HangNhap, model => model.MapFrom(item => item.HangNhap))
                .ForMember(entity => entity.HangMua, model => model.MapFrom(item => item.HangMua))
                .ForMember(entity => entity.PhanCap, model => model.MapFrom(item => item.PhanCap))
                .ForMember(entity => entity.ChuaPhanCap, model => model.MapFrom(item => item.ChuaPhanCap))
                .ForMember(entity => entity.TuChi, model => model.MapFrom(item => item.ChiTiet));

            CreateMap<PlanBeginYearModel, NsDtdauNamChungTu>()
                .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.Id_DonVi));

            CreateMap<NsDtdauNamChungTu, PlanBeginYearModel>()
                .ForMember(entity => entity.LoaiNganSach, model => model.MapFrom(item => item.ILoaiChungTu))
                .ForMember(entity => entity.DsLNS, model => model.MapFrom(item => item.SDslns))
                .ForMember(entity => entity.Id_DonVi, model => model.MapFrom(item => item.IIdMaDonVi));

            CreateMap<NsDtdauNamChungTuChiTiet, NsDtdauNamChungTuChiTietReportModel>();
            CreateMap<ReportChungTuDacThuDauNamPhanCapQuery, NsDtdauNamChungTuChiTietReportModel>();

            CreateMap<NsDtdauNamChungTuChiTietCanCu, JsonNsDtDauNamChungTuChiTietCanCuQuery>();
            CreateMap<JsonNsDtDauNamChungTuChiTietCanCuQuery, NsDtdauNamChungTuChiTietCanCu>();
        }
    }
}
