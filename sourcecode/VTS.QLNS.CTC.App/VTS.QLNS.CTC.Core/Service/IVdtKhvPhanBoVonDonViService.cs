using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonDonViService
    {
        IEnumerable<VdtKhvPhanBoVonDonViQuery> GetDataPhanBoVonDonViIndexView();
        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonVi objPhanBoVon);
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonVi objPhanBoVon);
        bool Insert(VdtKhvPhanBoVonDonVi data, string sUserLogin, ref string sMessError);
        void DeletePhanBoVonDonVi(VdtKhvPhanBoVonDonVi data);
        bool UpdatePhanBoVon(VdtKhvPhanBoVonDonVi data, string sUserLogin, ref string sMessError);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetAllDuAnInPhanBoVon(int iNamKeHoach, DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId, int? filterHasQDDT);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdParent);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid iIdParent);
        IEnumerable<ExportVonNamDonViQuery> GetKeHoachVonNamDonViExport(List<Guid> lstPhanboVonId);
        bool CreatePhanBoVonChiTiet(Guid iIdParentId, List<VdtKhvPhanBoVonDonViChiTiet> lstData);
        IEnumerable<VdtKhvVonNamDonViReportQuery> GetReportKeHoachVonNamDonVi(int type, string theLoaiCongTrinh, string lstId, string lstLct, double donViTinh);
        VdtKhvPhanBoVonDonVi FindById(Guid id);
        int Update(VdtKhvPhanBoVonDonVi item);
        VdtKhvPhanBoVonDonVi Add(VdtKhvPhanBoVonDonVi entity);
        int Delete(Guid id);
        VdtKhvPhanBoVonDonViChiTiet GetPhanBoVonChiTietById(Guid id);
        IEnumerable<VdtKhvPhanBoVonDonVi> FindByCondition(Expression<Func<VdtKhvPhanBoVonDonVi, bool>> predicate);
        IEnumerable<PhanBoVonDonViDieuChinhReportQuery> GetPhanBoVonDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe);
        IEnumerable<PhanBoVonDonViGocReportQuery> GetPhanBoVonDonViGocReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe);
        IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonChiTietByIdDuAn(Guid idDuAn);
        IEnumerable<VdtKhvPhanBoVonDonVi> GetPhanBoVonByListId(List<Guid> lstId, int yearPlan);
        IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViByIdPhanBoVon(Guid idPhanBoVon);
        void CreateVoucherImports(VdtKhvPhanBoVonDonVi itemNew, List<VdtKhvPhanBoVonDonViChiTiet> itemDetailNew);
        int Adjust(VdtKhvPhanBoVonDonVi entity, List<VdtKhvPhanBoVonDonViChiTiet> detail);
        void LockOrUnlock(Guid id, bool isLock);
        VdtKhvPhanBoVonDonVi FindAggregateVoucher(string sTongHop);
        int Agregate(VdtKhvPhanBoVonDonVi entity, List<VdtKhvPhanBoVonDonViChiTiet> details);
        IEnumerable<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> GetPhanBoVonDieuChinhNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe);
        IEnumerable<VdtKhvVonNamDeXuatGocNguonVonQuery> GetPhanBoVonDonViGocNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe);
        IEnumerable<PhanBoVonDonViQuery> GetPhanBoVonDonViDieuChinh(string idPhanBoVonDv);
        IEnumerable<KeHoachVonDauTuTrungHan5NamQuery> GetVonBoTri5Nam(string lstId, int yearPlan);
    }
}
