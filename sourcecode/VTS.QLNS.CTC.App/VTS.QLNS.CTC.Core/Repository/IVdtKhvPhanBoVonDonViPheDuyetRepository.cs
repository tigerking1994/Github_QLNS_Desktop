using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{ 
    public interface IVdtKhvPhanBoVonDonViPheDuyetRepository : IRepository<VdtKhvPhanBoVonDonViPheDuyet>
    {
        /// <summary>
        /// get data show in Index View
        /// </summary>
        /// <returns></returns>
        IEnumerable<PhanBoVonDonViPheDuyetQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon);

        /// <summary>
        /// Delete VDT_KHV_PhanBoVon
        /// </summary>
        /// <param name="data">VDT_KHV_PhanBoVon</param>
        /// <returns></returns>
        int RemovePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data);

        /// <summary>
        /// Check ex
        /// </summary>
        /// <param name="iIdDonViQuanLy"></param>
        /// <param name="iNamKeHoach"></param>
        /// <returns></returns>
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai);

        //List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin);

        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai);

        //List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        //List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon);
        //IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid);
    }
}
