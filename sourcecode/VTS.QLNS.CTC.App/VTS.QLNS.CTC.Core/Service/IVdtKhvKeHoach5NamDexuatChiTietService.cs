using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoach5NamDeXuatChiTietService
    {
        //IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindAll();
        int AddRange(IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> entities);
        int Update(VdtKhvKeHoach5NamDeXuatChiTiet entity);
        void Delete(Guid id);
        VdtKhvKeHoach5NamDeXuatChiTiet FindById(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatExportQuery> GetDataExportKeHoachTrungHanDeXuat(Guid iID);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionIndex(string voucherId);
        //IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionModifiedIndex(string voucherId);
        int FindNextSoChungTuIndex(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByLevel(int level, Guid id, Guid? idParent);
        //int FindByMaxStt(int level, Guid id);
        //IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindBySMaOrder(string sMaOrder);
        IEnumerable<DuAnHangMucQuery> FindListDuAnHangMuc(string lstId, string listId);
        IEnumerable<DuAnNguonVonQuery> FindListNguonVon(string lstId, string listIdDuAn);
        IEnumerable<DuAnQuery> FindListDuAn(string lstId);
        IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuat(string id, string lct, string lstNguonVon, int type, double donViTinh, int iNamLamViec);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByIdKeHoach5Nam(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListVoucherDetailsModified(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListKH5NamDeXuatDieuChinhChiTiet(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> FindSuggestionReport(int type, string lstId, string lstDonVi, double donViTinh, string lstNgVon);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByListId(List<Guid> lstId);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuatChuyenTiep(string lstId, string lstBudget, string lstLoaiCongTrinh, string lstUnit, int type, double donViTinh);
        IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiep(string idDonVi);
        IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiepDieuChinh(string iIdDonVi);
    }
}
