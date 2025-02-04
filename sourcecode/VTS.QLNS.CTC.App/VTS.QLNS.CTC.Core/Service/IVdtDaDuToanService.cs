using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDaDuToanService
    {
        #region NamNV
        IEnumerable<VdtDaDuToanQuery> FindByCondition(int namLamViec);
        void DeleteDuToanChiTiet(Guid id);
        VdtDaDuToan Add(VdtDaDuToan entity);
        VdtDaDuToan FindById(Guid id);
        VdtDaDuToan FindDuToanByDuToanGocId(Guid id, Guid duToanId);
        int Update(VdtDaDuToan entity);
        IEnumerable<DuToanDetailQuery> FindListDetail(Guid duToanId, Guid? duAnChiPhiId);
        int AddRangeDuToanChiPhi(IEnumerable<VdtDaDuToanChiPhi> entities);
        int AddRangeDuToanNguonVon(IEnumerable<VdtDaDuToanNguonvon> entities);
        int AddRangeDuToanHangMuc(IEnumerable<VdtDaDuToanHangMuc> entities);
        int UpdateDuToanChiPhi(VdtDaDuToanChiPhi entity);
        int UpdateDuToanNguonVon(VdtDaDuToanNguonvon entity);
        int DeleteDuToanChiPhi(Guid id);
        int DeleteDuToanNguonVon(Guid id);
        int DeleteDuToanHangMucDetail(Guid id);
        VdtDaDuToanChiPhi FindDuToanChiPhi(params object[] keyValues);
        VdtDaDuToanNguonvon FindDuToanNguonVon(params object[] keyValues);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<VdtDaDuAn> FindDuAnByDonViAndLoaiQD(string donviQLId, string loaiQD);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhAdd(Guid duToanId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhUpdate(Guid duToanId);
        VdtDaDuToan FindByDuAnId(Guid duanId);
        List<VdtDaDuToan> FindListByDuAnId(Guid duanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhAdd(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhUpdate(Guid duToanId);
        VdtDaDuToanHangMuc FindDuToanHangMuc(Guid duToanHangMucId);
        int UpdateDuToanHangMuc(VdtDaDuToanHangMuc entity);
        IEnumerable<DuToanDetailQuery> FindListHangMucAllDetail(Guid duToanId);
        IEnumerable<VdtDaDuToanQuery> GetQDDauTuByDuAnIdAndNgayLap(Guid iIdDuAn, DateTime ngayLap);
        #endregion

        IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnId(Guid duToanId);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByDuToanId(Guid iIdDuToan, Guid? iIdChiPhi = null);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByQDDauTuId(Guid iIdQDDauTu, Guid? iIdChiPhi = null);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByChuTruongDauTuId(Guid iIdChuTruongDauTu);
        IEnumerable<DuToanDetailQuery> ListHangMucInitial(Guid qdDauTuId, Guid danhMucDuAnChiPhiId);
        IEnumerable<DuToanDetailQuery> ListHangMucByQDDauTu(Guid qdDauTuId);
        IEnumerable<DuToanDetailQuery> ListHangMucByDuToan(Guid duToanId);
        VdtDaDuToanDmHangMuc FindDuToanDMHangMuc(Guid hangMucId);
        int UpdateDuToanDanhMucHangMuc(VdtDaDuToanDmHangMuc entity);
        int AddDuToanDanhMucHangMuc(VdtDaDuToanDmHangMuc entity);
        IEnumerable<VdtDaDuToanQuery> GetDuToanByKHLCNhaThauId(Guid duToanId);
        IEnumerable<VdtDaDuToanQuery> GetQDDauTuByKHLCNhaThauId(Guid qdDauTuId);
        bool CheckExistInDuToanHangMuc(string listDuToanId, Guid danhMucChiPhiDuAnId);
        IEnumerable<DuToanDetailQuery> FindListDetail(string duToanId, Guid? duAnChiPhiId);
        public string GetDuToanIdByDuAnId(Guid duAnId);
        IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnIdAndActive(Guid iIdDuAn, int bActive);
        public double GetGiaTriDuToanIdByDuAnId(Guid duAnId);
        public VdtDaDuToanDmHangMuc FindDaDuToanHangMucByMa(string ma);
        bool CheckQDDTExistTKTCTDT(Guid qdDtId);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<VdtDaDuToan> FindByCondition(Expression<Func<VdtDaDuToan, bool>> predicate);
        IEnumerable<VdtDaDuToanNguonvon> FindDuToanNguonVonByCondition(Expression<Func<VdtDaDuToanNguonvon, bool>> predicate);
        bool checkExistLoaiQuyetDinh(bool bLaTongDuToan, Guid? idDuAnId);
        double TinhTongPheDuyetDuAn(Guid? idDuAnId, Guid? idDuToanId);
    }
}
