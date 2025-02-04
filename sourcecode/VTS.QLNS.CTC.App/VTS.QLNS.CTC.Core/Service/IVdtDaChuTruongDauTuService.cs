using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDaChuTruongDauTuService
    {
        VdtDaChuTruongDauTu Add(VdtDaChuTruongDauTu entity);
        VdtDaChuTruongDauTu Adjust(VdtDaChuTruongDauTu entity);
        int AddRangeChuTruongChiPhi(IEnumerable<VdtDaChuTruongDauTuChiPhi> entities);
        int AddRangeChuTruongNguonVon(IEnumerable<VdtDaChuTruongDauTuNguonVon> entities);
        int AddRangeChuTruongDMHangMuc(IEnumerable<VdtDaChuTruongDauTuDmHangMuc> entities);
        int AddRangeChuTruongHangMuc(IEnumerable<VdtDaChuTruongDauTuHangMuc> entities);
        VdtDaChuTruongDauTu FindById(Guid id);
        VdtDaChuTruongDauTu FindByDuAnId(Guid id);
        VdtDaChuTruongDauTu FindCTDTDieuChinhByDuAn(Guid id, Guid duAnId);
        int Update(VdtDaChuTruongDauTu entity);
        IEnumerable<ChuTruongDauTuQuery> FindByCondition(int namLamViec, string userlogin);
        IEnumerable<ChuTruongDauTuQuery> FindByConditionUserLogin(string userlogin);
        void DeleteChuTruongDauTu(Guid id, Guid? parentId);
        IEnumerable<VdtDaDuAn> FindDuAnNotExistsInChuTruongDT(Guid chuTruongDT, string maDonVi, int namLV);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<ChuTruongDauTuDetailQuery> FindListDetail(Guid chuTruongDT);
        VdtDaChuTruongDauTuChiPhi FindChuTruongChiPhi(params object[] keyValues);
        VdtDaChuTruongDauTuNguonVon FindChuTruongNguonVon(params object[] keyValues);
        VdtDaChuTruongDauTuDmHangMuc FindChuTruongDMHangMuc(params object[] keyValues);
        VdtDaChuTruongDauTuHangMuc FindChuTruongHangMuc(params object[] keyValues);
        int UpdateChuTruongChiPhi(VdtDaChuTruongDauTuChiPhi entity);
        int UpdateChuTruongNguonVon(VdtDaChuTruongDauTuNguonVon entity);
        int UpdateChuTruongDMHangMuc(VdtDaChuTruongDauTuDmHangMuc entity);
        int UpdateChuTruongHangMuc(VdtDaChuTruongDauTuHangMuc entity);
        int DeleteChuTruongChiPhi(Guid id);
        int DeleteChuTruongNguonVon(Guid id);
        int DeleteChuTruongDMHangMuc(Guid id);
        int DeleteChuTruongHangMuc(Guid id);
        IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetail(Guid chuTruongId);
        IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetailAfterSaveChuTruong(Guid chuTruongId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDetail(Guid chuTruongId);
        ChuTruongDauTuQuery FindChuTruongById(Guid id);
        bool CheckDuAnExistQDDauTu(Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongDauTuNguonVonByDuAn(Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhAdd(Guid chuTruongId);
        IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhAdd(Guid chuTruongId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhUpdate(Guid chuTruongId);
        IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhUpdate(Guid chuTruongId);
        public void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByDuAnInKhlcNhaThauScreen(Guid iIdDuAnId);
        IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByIdInKhlcNhaThauScreen(Guid iIdChuTruongDauTuId);
        void DeleteChuTruongDauTuHangMuc(Guid id);
    }
}
