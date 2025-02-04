using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoach5NamChiTietService
    {
        public void CreateSettlementVoucherDetail(MidiumTermPlanCriteria creation);
        int AddRange(IEnumerable<VdtKhvKeHoach5NamChiTiet> entities);
        void Delete(Guid id);
        VdtKhvKeHoach5NamChiTiet FindById(Guid id);
        int Update(VdtKhvKeHoach5NamChiTiet entity);
        IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5Nam(Guid id);
        //IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5NamDuocDuyet();
        //IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5NamDeXuat();
        //IEnumerable<VdtKhvKeHoach5NamChiTiet> FindAll();
        //IEnumerable<ReportKeHoach5NamQuery> GetReportKeHoach5nam(int iIdNguonVon, string iIdMaDonViQuanLy, Guid iIdLoaiCongTrinh, int iNamBatDau, int iLoai);
        //IEnumerable<VdtKhvKeHoach5NamExportQuery> FindKeHoach5NamExport(MidiumTermExportSearch condition);
        //IEnumerable<VdtKhvKeHoach5NamExportQuery> FindQuyetDinhDauTu(MidiumTermExportSearch condition);
        IEnumerable<VdtKhvKeHoach5NamReportQuery> FindByReportKeHoachTrungHan(string id, string lct, int idNguonVon, int type, double donViTinh, string lstDonViThucHienDuAn);
        //IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindByKeHoach5NamChiTietModified(string idParent);
        IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindByKeHoach5NamChiTiet(string id);
        IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id);
        IEnumerable<VdtKhvKeHoach5NamChuyenTiepReportQuery> FindByReportKeHoachTrungHanChuyenTiep(string id, string lstBudget, string lstUnit, int type, double donViTinh);
        IEnumerable<VdtKhvKeHoach5NamExportQuery> GetDataExportKeHoachTrungHan(string id);
    }
}
