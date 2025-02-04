using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktChungTuChiTietService : ISktChungTuChiTietService
    {
        private readonly ISktChungTuChiTietRepository _sktChungTuChiTietRepository;

        public SktChungTuChiTietService(ISktChungTuChiTietRepository sktChungTuChiTietRepository)
        {
            _sktChungTuChiTietRepository = sktChungTuChiTietRepository;
        }

        public NsSktChungTuChiTiet FindById(Guid id)
        {
            return _sktChungTuChiTietRepository.FindById(id);
        }

        public int Add(NsSktChungTuChiTiet entity)
        {
            return _sktChungTuChiTietRepository.Add(entity);
        }

        public int Delete(NsSktChungTuChiTiet item)
        {
            return _sktChungTuChiTietRepository.Delete(item);
        }

        public int Update(NsSktChungTuChiTiet item)
        {
            return _sktChungTuChiTietRepository.Update(item);
        }

        public void BulkInsert(List<NsSktChungTuChiTiet> lstData)
        {
            _sktChungTuChiTietRepository.BulkInsert(lstData);
        }

        public IEnumerable<SoKyHieuMucLucNganSachQuery> FindSoKyHieus(int namLamViec, string iID_MaBQuanLy, int nguonNganSach)
        {
            return _sktChungTuChiTietRepository.FindSoKyHieus(namLamViec, iID_MaBQuanLy, nguonNganSach);
        }


        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit(SktChungTuChiTietCriteria searchModel)
        {
            return _sktChungTuChiTietRepository.FindByConditionForChildUnit(searchModel);
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit_1(SktChungTuChiTietCriteria searchModel)
        {
            return _sktChungTuChiTietRepository.FindByConditionForChildUnit_1(searchModel);
        }

        public IEnumerable<SktChungTuChiTietQuery> FindReportSoSanhSKT(SktChungTuChiTietCriteria searchModel)
        {
            return _sktChungTuChiTietRepository.FindReportSoSanhSKT(searchModel);
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnit(SktChungTuChiTietCriteria searchModel)
        {
            return _sktChungTuChiTietRepository.FindByConditionForParentUnit(searchModel);
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnitByIdMucLuc(SktChungTuChiTietCriteria searchModel)
        {
            return _sktChungTuChiTietRepository.FindByConditionForParentUnitByIdMucLuc(searchModel);
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByCondition(Expression<Func<NsSktChungTuChiTiet, bool>> predicate)
        {
            return _sktChungTuChiTietRepository.FindAll(predicate);
        }

        public int AddRange(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets)
        {
            return _sktChungTuChiTietRepository.AddRange(sktChungTuChiTiets);
        }

        public int UpdateRange(IEnumerable<NsSktChungTuChiTiet> chungTuChiTiets)
        {
            return _sktChungTuChiTietRepository.UpdateRange(chungTuChiTiets);
        }

        public int RemoveRange(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets)
        {
            return _sktChungTuChiTietRepository.RemoveRange(sktChungTuChiTiets);
        }

        public IEnumerable<SktChungTuChiTietQuery> FindReportNhapSoKiemTra(string idDonViUser, int namLamViec, int namNganSach, int nguonNganSach, int iLoai, int donViTinh, int loaiChungTu)
        {
            return _sktChungTuChiTietRepository.FindReportNhapSoKiemTra(idDonViUser,
                namLamViec, namNganSach, nguonNganSach, iLoai, donViTinh, loaiChungTu);
        }

        public IEnumerable<ReportSoNhuCauTongHopQuery> FindReportSoNhuCauTongHop(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHop(lstIdTongHop, namLamViec, namNganSach, nguonNganSach,
            loaiChungTu, donViTinh, iLoai);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoChiTietMucLucDonVi(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            return _sktChungTuChiTietRepository.FindReportPhanBoChiTietMucLucDonVi(nganh, namLamViec, namNganSach, nguonNganSach, donViTinh);
        }

        public IEnumerable<ReportSoNhuCauTongHopDonViQuery> FindReportSoNhuCauTongHopDonVi(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai, string maBQuanLy, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHopDonVi(lstIdTongHop, namLamViec, namNganSach, nguonNganSach,
                loaiChungTu, donViTinh, iLoai, maBQuanLy, loaiNNS);
        }

        public IEnumerable<ReportPhanBoSoKiemTraDonViQuery> FindReportPhanBoSoKiemTraDonVi(string idDonVi, string idChungTu,
            int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int iLoai, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportPhanBoSoKiemTraDonVi(idDonVi, idChungTu,
                 loaiChungTu, namLamViec, namNganSach, nguonNganSach, donViTinh, iLoai, loaiNNS);
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTraTrinhKyQuery> FindReportPhanBoSoKiemTraDonViTrinhKy(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, string userName, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportPhanBoSoKiemTraDonViTrinhKy(idDonVi,
                loaiChungTu, namLamViec, namNganSach, nguonNganSach, donViTinh, userName, loaiNNS);
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTra(string idDonVi,
            int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone)
        {
            return _sktChungTuChiTietRepository.FindReportTongHopPhanBoSoKiemTra(idDonVi,
                loaiChungTu, namLamViec, namNganSach, nguonNganSach, donViTinh, loaiNNS, typeClone);
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTraBVTC(string idDonVi,
            int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false)
        {
            return _sktChungTuChiTietRepository.FindReportTongHopPhanBoSoKiemTraBVTC(idDonVi,
                loaiChungTu, namLamViec, namNganSach, nguonNganSach, donViTinh, loaiNNS, typeClone);
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopSoKiemTraBenhVienTuChu(string idDonVi, int namLamViec, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportTongHopSoKiemTraBenhVienTuChu(idDonVi, namLamViec, loaiNNS);
        }

        public IEnumerable<MucLucSoKiemTraTheoNganhQuery> FindMucLucSKTTheoNganh(string nganh, int isNganh, int namLamViec)
        {
            return _sktChungTuChiTietRepository.FindMucLucSKTTheoNganh(nganh, isNganh, namLamViec);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhQuery> FindReportPhanBoKiemTraTheoNganh(string nganh, string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            return _sktChungTuChiTietRepository.FindReportPhanBoKiemTraTheoNganh(nganh, idDonVi, namLamViec, namNganSach, nguonNganSach, donViTinh);
        }

        public IEnumerable<ReportChiTietPhanBoKiemTraNsbdQuery> FindReportChiTietPhanBoKiemTraNSBD(string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            return _sktChungTuChiTietRepository.FindReportChiTietPhanBoKiemTraNSBD(idDonVi, namLamViec, namNganSach,
                nguonNganSach, donViTinh);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoKiemTraTheoNganhPhuLuc(string nganh, string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool IsInTheoTongHop)
        {
            return _sktChungTuChiTietRepository.FindReportPhanBoKiemTraTheoNganhPhuLuc(nganh, idDonVi, namLamViec, namNganSach, nguonNganSach, loai, donViTinh, IsInTheoTongHop);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportSoNhuCauTheoNganhPhuLuc(string nganh,
            string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai,
            int donViTinh)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTheoNganhPhuLuc(nganh, idDonVi, lstIdChungTu, namLamViec, namNganSach, nguonNganSach, loai, donViTinh);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportNhanSoKiemTraTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh)
        {
            return _sktChungTuChiTietRepository.FindReportNhanSoKiemTraTheoNganhPhuLuc(nganh, idDonVi, namLamViec, namNganSach, nguonNganSach, loai, donViTinh);
        }
        public IEnumerable<CanCuSoNhuCauQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu,
            int namLamViec)
        {
            return _sktChungTuChiTietRepository.FindCanCuSoNhuCau(lstChungTu, lstIdMucLuc, idDonVi, loaiCanCu, namLamViec);
        }

        public IEnumerable<CanCuDuToanQtCpSoNhuCauQuery> FindCanCuDuToanSoNhuCau(string lstChungTu, string lstIdMucLuc,
            string idDonVi, string loaiCanCu, int namLamViec)
        {
            return _sktChungTuChiTietRepository.FindCanCuDuToanSoNhuCau(lstChungTu, lstIdMucLuc, idDonVi, loaiCanCu, namLamViec);
        }

        public IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach)
        {
            return _sktChungTuChiTietRepository.FindCanCuSoKiemTra(loaiChungTu, idDonVi, namLamViec, namNganSach, nguonNganSach);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauTongHopPhuLuc3(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHopPhuLuc3(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, maBQuanLy, dvt, loaiNNS);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauTongHopPhuLuc4(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHopPhuLuc4(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, maBQuanLy, dvt, loaiNNS);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauTongHopPhuLuc5(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHopPhuLuc5(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, maBQuanLy, dvt, loaiNNS);
        }
        public IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauTongHopPhuLuc6(int loaiChungTu,
            string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauTongHopPhuLuc6(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, maBQuanLy, dvt, loaiNNS);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauChiTietPhuLuc3(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauChiTietPhuLuc3(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, dvt, loaiNNS);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauChiTietPhuLuc4(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauChiTietPhuLuc4(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, dvt, loaiNNS);
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauChiTietPhuLuc5(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauChiTietPhuLuc5(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, dvt, loaiNNS);
        }
        public IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauChiTietPhuLuc6(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.FindReportSoNhuCauChiTietPhuLuc6(loaiChungTu, maDonVi, namLamViec, namNganSach, maNguonNganSach, dvt, loaiNNS);
        }
        public void AddAggregate(DemandVoucherDetailCriteria creation)
        {
            _sktChungTuChiTietRepository.AddAggregate(creation);
        }

        public void DeleteByVoucherId(Guid voucherId, int loai)
        {
            _sktChungTuChiTietRepository.DeleteByVoucherId(voucherId, loai);
        }

        public bool ExistChungTuChiTiet(Guid chungtuId)
        {
            return _sktChungTuChiTietRepository.ExistChungTuChiTiet(chungtuId);
        }

        public IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuPhanSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, bool isParent)
        {
            return _sktChungTuChiTietRepository.FindCanCuPhanSoKiemTra(loaiChungTu, idDonVi, namLamViec, namNganSach, nguonNganSach, isParent);
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> PrintReportPhuongAnPhanBoSKT02A(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.PrintReportPhuongAnPhanBoSKT02A(nganh, namLamViec, namNganSach, nguonNganSach, khoi, donViTinh, bTongHop, loaiNNS);
        }

        public IEnumerable<ReportPhanBoKiemTraPhuongAnPhanBoQuery> PrintReportPhuongAnPhanBoSKT02B(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS)
        {
            return _sktChungTuChiTietRepository.PrintReportPhuongAnPhanBoSKT02B(nganh, namLamViec, namNganSach, nguonNganSach, khoi, donViTinh, bTongHop, loaiNNS);
        }
    }
}