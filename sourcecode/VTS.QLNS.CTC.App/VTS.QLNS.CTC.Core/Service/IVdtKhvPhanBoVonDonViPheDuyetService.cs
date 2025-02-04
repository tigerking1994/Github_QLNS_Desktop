using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonDonViPheDuyetService
    {
        IEnumerable<PhanBoVonDonViPheDuyetQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon);

        bool Insert(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError);
        bool UpdatePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError, ref MidiumTermPlanCriteria dataDetail);
        bool DeletePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError);

        /// <summary>
        /// Check exist Vdt_Khv_PhanBoVon
        /// </summary>
        /// <param name="objPhanBoVon"></param>
        /// <param name="iLoai"></param>
        /// <returns></returns>
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai);

        //List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin);

        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai);
        IEnumerable<VdtKhvPhanBoVonDonViPheDuyet> FindAll(Expression<Func<VdtKhvPhanBoVonDonViPheDuyet, bool>> predicate);
        IEnumerable<VdtKhvPhanBoVonDonViPheDuyet> FindByCondition(Expression<Func<VdtKhvPhanBoVonDonViPheDuyet, bool>> predicate);
        VdtKhvPhanBoVonDonViPheDuyet FindById(Guid id);
        int Update(VdtKhvPhanBoVonDonViPheDuyet item);
        int Delete(Guid id);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon);
        void CreateVoucherImports(VdtKhvPhanBoVonDonViPheDuyet itemNew, List<VdtKhvPhanBoVonDonViChiTietPheDuyet> itemDetailNew);
        int Adjust(VdtKhvPhanBoVonDonViPheDuyet entity);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<PhanBoVonDonViPheDuyetReportQuery> GetPhanBoVonDonViPheDuyetReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe);
        IEnumerable<long> GetVonBoTri5Nam(string lstId, int yearPlan);
        IEnumerable<PhanBoVonDonViPheDuyetDieuChinhReportQuery> GetPhanBoVonDonViPheDuyetDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe);
        IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonQuery> GetPhanBoVonDonViPheDuyetNguonVon(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe);
        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonDieuChinhQuery> GetPhanBoVonDonViPheDuyetNguonVonDieuChinh(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe);
    }
}
