using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaDuToanRepository : IRepository<VdtDaDuToan>
    {
        IEnumerable<VdtDaDuToanQuery> FindByCondition(int namLamViec);
        void DeleteDuToanChiTiet(Guid id);
        IEnumerable<DuToanDetailQuery> FindListDetail(Guid duToanId, Guid? duAnChiPhiId);
        IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhAdd(Guid duToanId, Guid? duAnChiPhiId);
        IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhUpdate(Guid duToanId, Guid? duAnChiPhiId);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<VdtDaDuAn> FindDuAnByDonViAndLoaiQD(string donviQLId, string loaiQD);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhAdd(Guid duToanId);
        IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhUpdate(Guid duToanId);
        VdtDaDuToan FindByDuAnId(Guid duanId);
        List<VdtDaDuToan> FindListByDuAnId(Guid duanId);
        VdtDaDuToan FindDuToanByDuToanGocId(Guid id, Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhAdd(Guid duToanId);
        IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhUpdate(Guid duToanId);
        void AddDuToanHangMuc(Guid duToanId, Guid qdDauTuId);
        IEnumerable<VdtDmChiPhi> GetListQDChiPhi(Guid duToanId);
        void AddDuToanHangMucDetail(Guid duToanId, Guid qdDauTuId);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn, Guid? iIdChiPhi);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByDuToanId(Guid iIdDuToan, Guid? iIdChiPhi);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByQDDauTuId(Guid iIdQDDauTu, Guid? iIdChiPhi);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByChuTruongDauTuId(Guid iIdChuTruongDauTu);
        void InsertDuToanChiPhi(IEnumerable<VdtDaDuToanChiPhi> datas);
        void UpdateDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas);
        void DeleteDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas);
        void InsertDuToanNguonVon(IEnumerable<VdtDaDuToanNguonvon> datas);
        void UpdateDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas);
        void DeleteDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas);
        void InsertDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas);
        void UpdateDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas);
        void DeleteDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas);
        void InsertDuToanHangMuc(IEnumerable<VdtDaDuToanHangMuc> datas);
        void UpdateDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas);
        void DeleteDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas);
        bool CheckExistInDuToanHangMuc(Guid duToanId, Guid danhMucChiPhiDuAnId);
        bool CheckExistInDuToanHangMuc(string listDuToanId, Guid danhMucChiPhiDuAnId);
        IEnumerable<DuToanDetailQuery> ListHangMucInitial(Guid qdDauTuId, Guid danhMucDuAnChiPhiId);
        IEnumerable<DuToanDetailQuery> ListHangMucByQDDauTu(Guid qdDauTuId);
        IEnumerable<DuToanDetailQuery> ListHangMucByDuToan(Guid duToanId);
        IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnId(Guid iIdDuAn);
        IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnIdAndActive(Guid iIdDuAn, int bActive);
        IEnumerable<VdtDaDuToanQuery> GetQDDauTuByDuAnIdAndNgayLap(Guid iIdDuAn, DateTime ngayLap);
        IEnumerable<VdtDaDuToanQuery> GetDuToanByKHLCNhaThauId(Guid duToanId);
        IEnumerable<VdtDaDuToanQuery> GetQDDauTuByKHLCNhaThauId(Guid qdDauTuId);
        IEnumerable<DuToanDetailQuery> FindListHangMucAllDetail(Guid duToanId);
        IEnumerable<DuToanDetailQuery> FindListDetail(string duToanId, Guid? duAnChiPhiId);
        public string GetDuToanIdByDuAnId(Guid duAnId);
        double GetGiaTriDuToanIdByDuAnId(Guid duAnId);
        bool CheckQDDTExistTKTCTDT(Guid qdDtId);
        bool checkExistLoaiQuyetDinh(bool bLaTongDuToan, Guid? iIdDuAnId);
        double TinhTongPheDuyetDuAn(Guid? iIdDuAnId, Guid? idDuToanId);
    }
}
