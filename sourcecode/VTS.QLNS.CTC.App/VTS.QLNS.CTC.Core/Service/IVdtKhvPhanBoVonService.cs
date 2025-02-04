using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonService
    {
        IEnumerable<PhanBoVonQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon);

        bool Insert(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError);
        bool UpdatePhanBoVon(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError, ref MidiumTermPlanCriteria dataDetail);
        bool DeletePhanBoVon(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError);

        /// <summary>
        /// Check exist Vdt_Khv_PhanBoVon
        /// </summary>
        /// <param name="objPhanBoVon"></param>
        /// <param name="iLoai"></param>
        /// <returns></returns>
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVon objPhanBoVon, int iLoai);

        List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin);

        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVon objPhanBoVon, int iLoai);
        IEnumerable<VdtKhvPhanBoVon> FindAll(Expression<Func<VdtKhvPhanBoVon, bool>> predicate);
        List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        IEnumerable<VdtKhvPhanBoVon> FindByCondition(Expression<Func<VdtKhvPhanBoVon, bool>> predicate);
        VdtKhvPhanBoVon FindById(Guid id);
        int Update(VdtKhvPhanBoVon item);
        int Delete(Guid id);
        IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon);
        void CreateVoucherImports(VdtKhvPhanBoVon itemNew, List<VdtKhvPhanBoVonChiTiet> itemDetailNew);
        int Adjust(VdtKhvPhanBoVon entity);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid);
        void AddRange(List<VdtTtDeNghiThanhToanKhv> lstDataKHV);
        IEnumerable<BcDuToanTheoLoaiCongTrinhQuery> GetBcDuToanTheoLoaiCongTrinh(int iLoaiChungTu, int iNamKeHoach, double fDonViTinh, List<string> lstDonViId);
    }
}
