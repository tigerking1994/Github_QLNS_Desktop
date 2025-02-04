using AutoMapper;
using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SettlementVoucherMapper : Profile
    {
        public SettlementVoucherMapper()
        {
            var dictDoiTuong = new Dictionary<string, string>()
            {
                ["3.2"] = "Công chức quốc phòng",
                ["2"] = "Quân nhân chuyên nghiệp",
                ["3.1"] = "Công nhân quốc phòng",
                ["4"] = "HSQ-CS",
                ["3.3"] = "Viên chức quốc phòng",
                ["1"] = "Sĩ quan",
                [""] = ""
            };

            CreateMap<NsQtChungTu, SettlementVoucherModel>();
            CreateMap<SettlementVoucherModel, NsQtChungTu>();
            CreateMap<NsQtChungTuQuery, SettlementVoucherModel>();
            CreateMap<SettlementVoucherModel, NsQtChungTuQuery>();

            CreateMap<QtChungTuChiTietQuery, SettlementVoucherDetailModel>()
                .ForMember(x => x.IsHangCha, y => y.MapFrom(z => z.BHangChaQuyetToan))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangChaQuyetToan))
                .ForMember(x => x.FDuToanOrigin, y => y.MapFrom(z => z.FDuToan))
                .ForMember(x => x.SDoiTuong, y => y.MapFrom(z => dictDoiTuong[z.SMaCB.Trim()]));

            CreateMap<SettlementVoucherDetailModel, NsQtChungTuChiTiet>().ReverseMap();
            CreateMap<TlQtChungTuChiTietQuery, SettlementVoucherDetailModel>()
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));
            CreateMap<TlQtChungTuChiTietNq104Query, SettlementVoucherDetailModel>()
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));
            CreateMap<NsQtChungTuChiTietGiaiThich, SettlementVoucherDetailExplainModel>();
            CreateMap<SettlementVoucherDetailExplainModel, NsQtChungTuChiTietGiaiThich>();
            CreateMap<NsQtChungTuChiTietGiaiThichLuongTru, SettlementVoucherDetailExplainSubtractModel>();
            CreateMap<SettlementVoucherDetailExplainSubtractModel, NsQtChungTuChiTietGiaiThichLuongTru>();

            CreateMap<SettlementVoucherDetailImportModel, NsQtChungTuChiTiet>()
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.ConcatenateCode))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => string.IsNullOrEmpty(z.Suggestion) ? 0 : double.Parse(z.Suggestion)))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => string.IsNullOrEmpty(z.Day) ? 0 : double.Parse(z.Day)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => string.IsNullOrEmpty(z.People) ? 0 : double.Parse(z.People)))
                .ForMember(x => x.FSoLuot, y => y.MapFrom(z => string.IsNullOrEmpty(z.Bout) ? 0 : double.Parse(z.Bout)))
                .ForMember(x => x.SGhiChu, y => y.MapFrom(z => z.Note));

            CreateMap<SettlementVoucherDetailImportModel, ImpQuyetToan>()
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.L, y => y.MapFrom(z => z.L))
                .ForMember(x => x.K, y => y.MapFrom(z => z.K))
                .ForMember(x => x.M, y => y.MapFrom(z => z.M))
                .ForMember(x => x.Tm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.Ttm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.Ng, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.Tng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.XauNoiMa, y => y.MapFrom(z => z.ConcatenateCode))
                .ForMember(x => x.MoTa, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.DeNghi, y => y.MapFrom(z => string.IsNullOrEmpty(z.Suggestion) ? 0 : double.Parse(z.Suggestion)))
                .ForMember(x => x.SoNgay, y => y.MapFrom(z => string.IsNullOrEmpty(z.Day) ? 0 : double.Parse(z.Day)))
                .ForMember(x => x.SoNguoi, y => y.MapFrom(z => string.IsNullOrEmpty(z.People) ? 0 : double.Parse(z.People)))
                .ForMember(x => x.SoLuot, y => y.MapFrom(z => string.IsNullOrEmpty(z.Bout) ? 0 : double.Parse(z.Bout)))
                .ForMember(x => x.GhiChu, y => y.MapFrom(z => z.Note));

            CreateMap<NsQtChungTu, ChungTuCanCuModel>()
                .ForMember(model => model.SoChungTu, member => member.MapFrom(entity => entity.SSoChungTu))
                .ForMember(model => model.NgayChungTu, member => member.MapFrom(entity => entity.DNgayChungTu))
                .ForMember(model => model.Month, member => member.MapFrom(entity => entity.SThangQuyMoTa));

            CreateMap<TlQtChungTuChiTiet, NsQtChungTuChiTiet>()
                .ForMember(x => x.IIdQtchungTu, y => y.MapFrom(z => z.IdChungTu))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamNganSach, y => y.MapFrom(z => z.NamNganSach))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IIdMaNguonNganSach, y => y.MapFrom(z => z.NguonNganSach))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.IThangQuy))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.IdDonVi))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));

            CreateMap<TlQtChungTuChiTiet, SettlementVoucherDetailModel>()
                .ForMember(x => x.IIdQtchungTu, y => y.MapFrom(z => z.IdChungTu))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamNganSach, y => y.MapFrom(z => z.NamNganSach))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IIdMaNguonNganSach, y => y.MapFrom(z => z.NguonNganSach))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.IThangQuy))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.IdDonVi))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));
            CreateMap<TlQtChungTuChiTietNq104, NsQtChungTuChiTiet>()
                .ForMember(x => x.IIdQtchungTu, y => y.MapFrom(z => z.IdChungTu))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamNganSach, y => y.MapFrom(z => z.NamNganSach))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IIdMaNguonNganSach, y => y.MapFrom(z => z.NguonNganSach))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.IThangQuy))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.IdDonVi))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));

            CreateMap<TlQtChungTuChiTietNq104, SettlementVoucherDetailModel>()
                .ForMember(x => x.IIdQtchungTu, y => y.MapFrom(z => z.IdChungTu))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent ?? Guid.Empty))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.Tm))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.Ttm))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.Ng))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.Tng))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.Tng1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.Tng2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.Tng3))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha.GetValueOrDefault()))
                .ForMember(x => x.INamNganSach, y => y.MapFrom(z => z.NamNganSach))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.IIdMaNguonNganSach, y => y.MapFrom(z => z.NguonNganSach))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.IThangQuy))
                .ForMember(x => x.IThangQuyLoai, y => y.MapFrom(z => 0))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.IdDonVi))
                .ForMember(x => x.FTuChiDeNghi, y => y.MapFrom(z => decimal.ToDouble(z.DieuChinh ?? 0)))
                .ForMember(x => x.FSoNguoi, y => y.MapFrom(z => z.SoNguoi ?? 0))
                .ForMember(x => x.FSoNgay, y => y.MapFrom(z => z.SoNgay ?? 0));
        }
    }
}
