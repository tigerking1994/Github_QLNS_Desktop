using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvPhanBoVonDonViChiTietRepository : IRepository<VdtKhvPhanBoVonDonViChiTiet>
    {
        IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViChiTietByIidPhanBoVonID(Guid iIdParent);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetAllDuAnInPhanBoVon(int iNamKeHoach, DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId, int? filterHasQDDT);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdParent);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid iIdParent);
        IEnumerable<ExportVonNamDonViQuery> GetKeHoachVonNamDonViExport(List<YearPlanManagerExportCriteria> lstPhanboVon);
        int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonDonViChiTiet> datas);
        IEnumerable<VdtKhvVonNamDonViReportQuery> GetReportKeHoachVonNamDonVi(int type, string theLoaiCongTrinh, string lstId, string lstLct, double donViTinh);
        IEnumerable<PhanBoVonDonViDieuChinhReportQuery> GetPhanBoVonDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe);
        IEnumerable<PhanBoVonDonViGocReportQuery> GetPhanBoVonDonViGocReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe);
        IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonChiTietByIdDuAn(Guid idDuAn);
        IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViByIdPhanBoVon(Guid idPhanBoVon);
        IEnumerable<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> GetPhanBoVonDieuChinhNguonVon(int type, string lstId,  string lstLct, string lstNguonVon, double donViTienTe);
        IEnumerable<VdtKhvVonNamDeXuatGocNguonVonQuery> GetPhanBoVonDonViGocNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe);
        IEnumerable<PhanBoVonDonViQuery> GetPhanBoVonDonViDieuChinh(string idPhanBoVonDv);
        IEnumerable<KeHoachVonDauTuTrungHan5NamQuery> GetVonBoTri5Nam(string lstId, int yearPlan);
    }
}
