using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IApproveProjectService
    {
        #region NamNV
        IEnumerable<ApproveProjectQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId);
        IEnumerable<ApproveProjectQuery> FindListQDDauTuByDuAnId(Guid duAnId);
        IEnumerable<VdtDaDuAn> FindDuAnByDonVi(string donviQLId);
        IEnumerable<VdtDmNhomDuAn> GetAllNhomDuAn();
        IEnumerable<VdtDmHinhThucQuanLy> GetAllHinhThucQuanLy();
        int Add(VdtDaQddauTu entity);
        VdtDaDuAn FindDuAnById(Guid idDuAn);
        VdtDaQddauTu FindById(Guid id);
        VdtDaQddauTu FindQDDaTuDieuChinhByDuAn(Guid id, Guid duAnId);
        IEnumerable<VdtDmChiPhi> GetAllDmChiPhi();
        IEnumerable<NsNguonNganSach> GetAllNguonNS();
        IEnumerable<ApproveProjectDetailQuery> FindListDetail(Guid quyetDinhDauTuId, Guid duAnId, Guid? duAnChiPhiId);
        
        int AddRangeChiPhi(IEnumerable<VdtDaQddauTuChiPhi> entities);
        int AddRangeNguonVon(IEnumerable<VdtDaQddauTuNguonVon> entities);
        int AddRangeDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> entities);
        int AddRangeQdDauTuDMHangMuc(IEnumerable<VdtDaQddauTuDmHangMuc> entities);
        int AddRangeHangMuc(IEnumerable<VdtDaQddauTuHangMuc> entities);
        int AddRangeDMDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> entities);
        VdtDaQddauTuChiPhi FindChiPhi(params object[] keyValues);
        VdtDaQddauTuNguonVon FindNguonVon(params object[] keyValues);
        VdtDaDuAnHangMuc FindDuAnHangMuc(params object[] keyValues);
        VdtDaQddauTuHangMuc FindQDDTHangMuc(params object[] keyValues);
        VdtDmDuAnChiPhi FindDMDuAnChiPhi(params object[] keyValues);
        VdtDaQddauTuDmHangMuc FindDanhMucHangMuc(params object[] keyValues);
        int UpdateChiPhi(VdtDaQddauTuChiPhi entity);
        int UpdateNguonVon(VdtDaQddauTuNguonVon entity);
        int UpdateHangMuc(VdtDaDuAnHangMuc entity);
        int UpdateQDDTHangMuc(VdtDaQddauTuHangMuc entity);
        int UpdateVdtDuAn(VdtDaDuAn entity);
        int Update(VdtDaQddauTu entity);
        int UpdateVdtDmDuAnChiPhi(VdtDmDuAnChiPhi entity);
        int UpdateVDTDanhMucHangMuc(VdtDaQddauTuDmHangMuc entity);
        int DeleteChiPhi(Guid id);
        int DeleteNguonVon(Guid id);
        int DeleteQDDTHangMuc(Guid id);
        void DeleteQDDauTuChiTiet(Guid id, Guid? parentId);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        VdtDaQddauTu FindByDuAnId(Guid duAnId);
        int DeleteDuAnHangMucByDuAnId(Guid id);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVon(Guid qdDauTuId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhi(Guid qdDauTuId);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonByDuAn(Guid duAnId);
        int Add(VdtDaQddauTuNguonVon entities);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListAllLoaiChiPhi();
        bool CheckExistInQDHangMuc(Guid qdDauTuId, Guid danhMucDuAnChiPhiId);
        IEnumerable<ApproveProjectDetailQuery> FindListDetailBeforeSave(Guid duAnId, Guid quyetDinhDauTuId);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinh(Guid qdDauTuId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhDefault(Guid qdDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListAllHangMucByQDDauTu(Guid quyetDinhDauTuId);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinhUpdate(Guid qdDauTuId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhUpdate(Guid qdDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhAdd(Guid quyetDinhDauTuId, Guid duAnChiPhiId);
        bool CheckChiPhiCoHangMuc(Guid chiPhiId);
        VdtDmDuAnChiPhi FindByNameDuAnChiPhi(string name);
        VdtDmDuAnChiPhi FindByMaDuAnChiPhi(string ma);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucByQDDauTu(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuAdd(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuUpdate(Guid quyetDinhDauTuId);
        IEnumerable<VdtDaDuAn> FindDuAnByMaDonVi(string sMaDonVi);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<VdtDaQddauTu> FindByCondition(Expression<Func<VdtDaQddauTu, bool>> predicate);
        IEnumerable<VdtDaQddauTuNguonVon> FindNguonVonByCondition(Expression<Func<VdtDaQddauTuNguonVon, bool>> predicate);

        #endregion
    }
}
