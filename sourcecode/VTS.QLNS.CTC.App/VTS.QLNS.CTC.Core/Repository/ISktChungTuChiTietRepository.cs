using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktChungTuChiTietRepository : IRepository<NsSktChungTuChiTiet>
    {
        NsSktChungTuChiTiet FindById(Guid id);
        IEnumerable<NsSktChungTuChiTiet> FindByCondition(Expression<Func<NsSktChungTuChiTiet, bool>> predicate);
        IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit(SktChungTuChiTietCriteria searchModel);
        IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit_1(SktChungTuChiTietCriteria searchModel);
        IEnumerable<SktChungTuChiTietQuery> FindReportSoSanhSKT(SktChungTuChiTietCriteria searchModel);
        IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnit(SktChungTuChiTietCriteria searchModel);
        IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnitByIdMucLuc(SktChungTuChiTietCriteria searchModel);
        IEnumerable<SoKyHieuMucLucNganSachQuery> FindSoKyHieus(int namLamViec, string iID_MaBQuanLy, int nguonNganSach = 0);

        IEnumerable<SktChungTuChiTietQuery> FindReportNhapSoKiemTra(string idDonViUser, int namLamViec, int namNganSach, int nguonNganSach,
            int iLoai, int donViTinh, int loaiChungTu);
        IEnumerable<ReportPhanBoSoKiemTraDonViQuery> FindReportPhanBoSoKiemTraDonVi(string idDonVi, string idChungTu,
            int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int iLoai, int loaiNNS);

        IEnumerable<ReportTongHopPhanBoSoKiemTraTrinhKyQuery> FindReportPhanBoSoKiemTraDonViTrinhKy(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, string userName, int loaiNNS);
        IEnumerable<ReportSoNhuCauTongHopQuery> FindReportSoNhuCauTongHop(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai);

        IEnumerable<ReportSoNhuCauTongHopDonViQuery> FindReportSoNhuCauTongHopDonVi(string lstIdTongHop, int namLamViec,
            int namNganSach, int nguonNganSach, int loaiChungTu, int donViTinh, int iLoai, string maBQuanLy, int loaiNNS);
        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoChiTietMucLucDonVi(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTra(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTraBVTC(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopSoKiemTraBenhVienTuChu(string idDonVi, int namLamViec, int loaiNNS);

        IEnumerable<MucLucSoKiemTraTheoNganhQuery> FindMucLucSKTTheoNganh(string nganh, int isNganh, int namLamViec);

        IEnumerable<ReportPhanBoKiemTraTheoNganhQuery> FindReportPhanBoKiemTraTheoNganh(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach,
            int donViTinh);

        IEnumerable<ReportChiTietPhanBoKiemTraNsbdQuery> FindReportChiTietPhanBoKiemTraNSBD(string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh);
        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoKiemTraTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai,
            int donViTinh, bool IsInTheoTongHop);

        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportSoNhuCauTheoNganhPhuLuc(string nganh,
            string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai,
            int donViTinh);

        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportNhanSoKiemTraTheoNganhPhuLuc(string nganh,
            string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh);

        IEnumerable<CanCuSoNhuCauQuery> FindCanCuSoNhuCau(string lstChungTu, string listIdMucLuc, string idDonVi, string loaiCanCu,
            int namLamViec);

        IEnumerable<CanCuDuToanQtCpSoNhuCauQuery> FindCanCuDuToanSoNhuCau(string lstChungTu, string lstIdMucLuc,
            string idDonVi, string loaiCanCu, int namLamViec);

        IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec,
            int namNganSach, int nguonNganSach);

        IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuPhanSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, bool isParent);

        void AddAggregate(DemandVoucherDetailCriteria creation);
        void DeleteByVoucherId(Guid voucherId, int loai);

        IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauTongHopPhuLuc3(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauTongHopPhuLuc4(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauTongHopPhuLuc5(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauTongHopPhuLuc6(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauChiTietPhuLuc3(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauChiTietPhuLuc4(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauChiTietPhuLuc5(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauChiTietPhuLuc6(int loaiChungTu, string maDonVi,
            int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS);
        
        bool ExistChungTuChiTiet(Guid chungtuId);

        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> PrintReportPhuongAnPhanBoSKT02A(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS);

        IEnumerable<ReportPhanBoKiemTraPhuongAnPhanBoQuery> PrintReportPhuongAnPhanBoSKT02B(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS);
    }
}