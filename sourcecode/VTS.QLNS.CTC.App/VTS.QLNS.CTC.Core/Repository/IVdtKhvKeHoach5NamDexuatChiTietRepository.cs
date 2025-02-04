using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoach5NamDeXuatChiTietRepository : IRepository<VdtKhvKeHoach5NamDeXuatChiTiet>
    {
        //VdtKhvKeHoach5NamDeXuatChiTiet FindById(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatExportQuery> GetDataExportKeHoachTrungHanDeXuat(Guid iID);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionIndex(string voucherId);
        int FindNextSoChungTuIndex(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByLevel(int level, Guid id, Guid? idParent);
        //IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindBySMaOrder(string sMaOrder);
        //int FindByMaxStt(int level, Guid id);
        IEnumerable<DuAnHangMucQuery> FindListDuAnHangMuc(string lstId, string listIdDuAn);
        IEnumerable<DuAnNguonVonQuery> FindListNguonVon(string lstId, string listIdDuAn);
        IEnumerable<DuAnQuery> FindListDuAn(string lstId);
        IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuat(string id, string lct, string lstNguonVon, int type, double donViTinh, int iNamLamViec);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByIdKeHoach5Nam(Guid id);
        //IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionModifiedIndex(string voucherId);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListVoucherDetailsModified(Guid idKhth);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListKH5NamDeXuatDieuChinhChiTiet(Guid idKhth);
        IEnumerable<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> FindSuggestionReport(int type, string lstId, string lstDonVi, double donViTinh, string lstNgVon);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByListId(List<Guid> lstId);
        IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuatChuyenTiep(string lstId, string lstBudget, string lstLoaiCongTrinh, string lstUnit, int type, double donViTinh);
        IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiep(string idDonVi);
        IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiepDieuChinh(string iIdDonVi);
    }
}
