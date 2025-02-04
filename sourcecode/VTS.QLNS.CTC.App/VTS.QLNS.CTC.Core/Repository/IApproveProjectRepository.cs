using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IApproveProjectRepository : IRepository<VdtDaQddauTu>
    {
        #region NamNV
        IEnumerable<ApproveProjectQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId);
        IEnumerable<VdtDaDuAn> FindDuAnByDonVi(string donviQLId);
        IEnumerable<VdtDmNhomDuAn> GetAllNhomDuAn();
        IEnumerable<VdtDmHinhThucQuanLy> GetAllHinhThucQuanLy();
        DonVi FindDonViByIdDonVi(string iDDonVi);
        VdtDaDuAn FindDuAnById(Guid idDuAn);
        IEnumerable<VdtDmChiPhi> GetAllDmChiPhi();
        IEnumerable<VdtDmChiPhi> GetListQDChiPhi(Guid qdDauTuId);
        IEnumerable<NsNguonNganSach> GetListNguonVonByQDDauTu(Guid qdDauTuId);
        IEnumerable<NsNguonNganSach> GetAllNguonNS();
        IEnumerable<ApproveProjectDetailQuery> FindListDetail(Guid quyetDinhDauTuId, Guid duAnId, Guid? duAnChiPhiId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucByQDDauTu(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDetail(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListDetailBeforeSave(Guid duAnId,Guid quyetDinhDauTuId);
        //IEnumerable<ApproveProjectDetailQuery> FindListHangMucDetail(Guid quyetDinhDauTuId, Guid duAnId);
        IEnumerable<ApproveProjectDieuChinhDetailQuery> FindListDieuChinhDetail(Guid quyetDinhDauTuId, DateTime ngayQuyetDinh);
        void DeleteQDDauTuChiTiet(Guid id, Guid? parentId);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        bool CheckExistsDuAnInQDDauTu(Guid duAnId);
        VdtDaQddauTu FindByDuAnId(Guid duAnId);
        VdtDaQddauTu FindQDDaTuDieuChinhByDuAn(Guid id, Guid duAnId);
        IEnumerable<QDDauTuChiPhiNguonVonDetailQuery> FindListChiPhiNguonVonDetail(Guid qDDauTu);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVon(Guid qdDauTuId);
        void UpdateQDDauTuIdToDuAnHangMuc(Guid idDuAn, Guid idQDDauTu);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhi(Guid qdDauTuId);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonByDuAn(Guid duAnId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiParent(Guid qdDauTuId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListAllLoaiChiPhi();
        bool CheckExistInQDHangMuc(Guid qdDauTuId, Guid danhMucDuAnChiPhiId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhDefault(Guid qdDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListAllHangMucByQDDauTu(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectQuery> FindListQDDauTuByDuAnId(Guid duAnId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhAdd(Guid quyetDinhDauTuId, Guid duAnChiPhiId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuAdd(Guid quyetDinhDauTuId);
        IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuUpdate(Guid quyetDinhDauTuId);
        bool CheckChiPhiCoHangMuc(Guid chiPhiId);
        #endregion
        
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinh(Guid qdDauTuId);
        IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinhUpdate(Guid qdDauTuId);
        IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhUpdate(Guid qdDauTuId);
        IEnumerable<VdtDaDuAn> FindDuAnByMaDonVi(string sMaDonVi);
    }
}
