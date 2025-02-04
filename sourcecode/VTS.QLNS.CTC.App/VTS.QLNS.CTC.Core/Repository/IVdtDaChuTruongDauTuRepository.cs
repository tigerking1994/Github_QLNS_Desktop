using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaChuTruongDauTuRepository: IRepository<VdtDaChuTruongDauTu>
    {
        IEnumerable<ChuTruongDauTuQuery> FindByCondition(int namLamViec, string userlogin);
        IEnumerable<ChuTruongDauTuQuery> FindByConditionUserLogin(string userlogin);
        ChuTruongDauTuQuery FindChuTruongById(Guid id);
        void DeleteChuTruongDauTu(Guid id, Guid? parentId);
        IEnumerable<VdtDaDuAn> FindDuAnNotExistsInChuTruongDT(Guid chuTruongDT, string maDonVi, int namLV);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<ChuTruongDauTuDetailQuery> FindListDetail(Guid chuTruongDT);
        bool CheckDuAnExistQDDauTu(Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDetail(Guid chuTruongId);
        VdtDaChuTruongDauTu FindByDuAnId(Guid id);
        VdtDaChuTruongDauTu FindCTDTDieuChinhByDuAn(Guid id, Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongDauTuNguonVonByDuAn(Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhAdd(Guid chuTruongId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhUpdate(Guid chuTruongId);
        IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByDuAnInKhlcNhaThauScreen(Guid iIdDuAnId);
        IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByIdInKhlcNhaThauScreen(Guid iIdChuTruongDauTuId);
        void DeleteChuTruongDauTuHangMuc(Guid id);
    }
}
