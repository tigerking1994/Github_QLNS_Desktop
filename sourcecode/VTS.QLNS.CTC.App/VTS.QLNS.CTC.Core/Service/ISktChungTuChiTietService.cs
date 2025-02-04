using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktChungTuChiTietService
    {
        NsSktChungTuChiTiet FindById(Guid id);
        int Add(NsSktChungTuChiTiet entity);
        int Delete(NsSktChungTuChiTiet item);
        int Update(NsSktChungTuChiTiet item);
        void BulkInsert(List<NsSktChungTuChiTiet> lstData);

        IEnumerable<SoKyHieuMucLucNganSachQuery> FindSoKyHieus(int namLamViec, string iID_MaBQuanLy, int nguonNganSach = 0);

        IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit(SktChungTuChiTietCriteria searchModel);

        IEnumerable<SktChungTuChiTietQuery> FindReportSoSanhSKT(SktChungTuChiTietCriteria searchModel);
        IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit_1(SktChungTuChiTietCriteria searchModel);

        IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnit(SktChungTuChiTietCriteria searchModel);

        IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnitByIdMucLuc(SktChungTuChiTietCriteria searchModel);

        IEnumerable<NsSktChungTuChiTiet> FindByCondition(Expression<Func<NsSktChungTuChiTiet, bool>> predicate);
        int AddRange(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets);
        int UpdateRange(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets);
        int RemoveRange(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets);

        IEnumerable<SktChungTuChiTietQuery> FindReportNhapSoKiemTra(string idDonViUser, int namLamViec, int namNganSach, int nguonNganSach, int iLoai,
            int donViTinh, int loaiChungTu);

        IEnumerable<ReportSoNhuCauTongHopQuery> FindReportSoNhuCauTongHop(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai);

        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoChiTietMucLucDonVi(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh);

        IEnumerable<ReportSoNhuCauTongHopDonViQuery> FindReportSoNhuCauTongHopDonVi(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai, string maBQuanLy, int loaiNNS);

        IEnumerable<ReportPhanBoSoKiemTraDonViQuery> FindReportPhanBoSoKiemTraDonVi(string idDonVi, string idChungTu,
            int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int iLoai, int loaiNNS);

        IEnumerable<ReportTongHopPhanBoSoKiemTraTrinhKyQuery> FindReportPhanBoSoKiemTraDonViTrinhKy(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, string userName, int loaiNNS);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTra(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTraBVTC(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false);

        IEnumerable<MucLucSoKiemTraTheoNganhQuery> FindMucLucSKTTheoNganh(string nganh, int isNganh, int namLamViec);

        IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopSoKiemTraBenhVienTuChu(string idDonVi, int namLamViec, int loaiNNS);
        public IEnumerable<ReportPhanBoKiemTraTheoNganhQuery> FindReportPhanBoKiemTraTheoNganh(string nganh, string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh);

        IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportNhanSoKiemTraTheoNganhPhuLuc(string nganh,
            string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh);

        public IEnumerable<ReportChiTietPhanBoKiemTraNsbdQuery> FindReportChiTietPhanBoKiemTraNSBD(string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh);

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportSoNhuCauTheoNganhPhuLuc(string nganh,
            string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh);

        public IEnumerable<CanCuSoNhuCauQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu,
            int namLamViec);

        public IEnumerable<CanCuDuToanQtCpSoNhuCauQuery> FindCanCuDuToanSoNhuCau(string lstChungTu, string lstIdMucLuc,
            string idDonVi, string loaiCanCu, int namLamViec);

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoKiemTraTheoNganhPhuLuc(string nganh,
            string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool IsInTheoTongHop);

        public IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec,
            int namNganSach, int nguonNganSach);

        IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuPhanSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, bool isParent);

        void AddAggregate(DemandVoucherDetailCriteria creation);
        void DeleteByVoucherId(Guid voucherId, int loai);

        IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauTongHopPhuLuc3(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);

        IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauTongHopPhuLuc4(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);

        IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauTongHopPhuLuc5(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);
        IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauTongHopPhuLuc6(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS);

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