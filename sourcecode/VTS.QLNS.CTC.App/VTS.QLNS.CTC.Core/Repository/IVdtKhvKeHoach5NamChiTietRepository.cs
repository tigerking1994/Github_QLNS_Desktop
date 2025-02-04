using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoach5NamChiTietRepository : IRepository<VdtKhvKeHoach5NamChiTiet>
    {
        void CreateSettlementVoucherDetail(MidiumTermPlanCriteria creation);
        IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5Nam(Guid id);
        IEnumerable<VdtKhvKeHoach5NamReportQuery> FindByReportKeHoachTrungHan(string id, string lct, int idNguonVon, int type, double donViTinh, string lstDonViThucHienDuAn);
        IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindByKeHoach5NamChiTiet(string id);
        IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id);
        IEnumerable<VdtKhvKeHoach5NamChuyenTiepReportQuery> FindByReportKeHoachTrungHanChuyenTiep(string id, string lstBudget, string lstUnit, int type, double donViTinh);
        IEnumerable<VdtKhvKeHoach5NamExportQuery> GetDataExportKeHoachTrungHan(string id);
    }
}
