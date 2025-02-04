using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcqBHXHChiTietService
    {
        IEnumerable<BhQtcqBHXHChiTiet> FindByCondition(Expression<Func<BhQtcqBHXHChiTiet, bool>> predicate);
        int AddRange(IEnumerable<BhQtcqBHXHChiTiet> items);
        int Update(BhQtcqBHXHChiTiet item);
        int RemoveRange(IEnumerable<BhQtcqBHXHChiTiet> items);
        int UpdateRange(IEnumerable<BhQtcqBHXHChiTiet> items);
        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary, string sMaDonVi);
        IEnumerable<BhQtcqBHXHChiTietQuery> GetChiTietQuyetToanChiQuyBHXH(Guid idChungTu, string sSLNS, int iNamLamViec, string iIDMaDonVi, bool isPhanBo, DateTime? dNgayChungTu);
        IEnumerable<BhQtcqBHXHChiTietQuery> BaoCaoQuyetToanChiQuyBHXH(int iNamLamViec, string idMaDonVi, string sLNS, bool loai, int iQuy, int donViTinh);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDau(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSan(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh);
        IEnumerable<BhQtcqBHXHChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(string idMaDonVi, string sLNS, int namLamViec, int quyChungTu);
        void CreateVoudcherForQuaterBefore(BhQtcqBHXH entity);
        List<ReportBHQTCQBHXHThongTriQuery> GetDataThongTriForDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh);
        bool ExistVoucherDetail(Guid voucherID);
        IEnumerable<BhQtcqBHXHChiTiet> FindAllVouchers();
        IEnumerable<BhQtcqBHXHChiTiet> FindByVoucherID(Guid voucherID);
        List<BhQtcqGiaiThichTroCapQuery> ExportDanhSachNguoiLaoDongNghiViec(int yearOfWork, int donViTinh, int quy, string donVi);
        List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNan(int yearOfWork, int donViTinh, int quy, string donVi);
        List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanTruyLinh(int yearOfWork, int donViTinh, int iQuy, string donVi);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTT(int yearOfWork, int donViTinh, int iQuy, string donVi);
        List<BhQtcqBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu);
        List<BhQtcqBHXHChiTietQuery> GetDataSummaryBefore(BhQtcqBHXH entity);
        List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoi(int yearOfWork, int donViTinh, int iQuy, string v);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoi(int yearOfWork, string v, int iQuy, int donViTinh);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTDetailXN(int yearOfWork, int donViTinh, int iQuy, string v);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoi(int yearOfWork, int donViTinh, int iQuy, string v);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiDetailXN(int yearOfWork, int donViTinh, int iQuy, string v);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoi(int yearOfWork, string v, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoiNhomDT(int yearOfWork, string donVi, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh);
        IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoiNhomDT(int yearOfWork, string v, int iQuy, int donViTinh);
        List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string donVi);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDTDetail(int yearOfWork, int donViTinh, int iQuy, string v);
        List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string v);
    }
}
