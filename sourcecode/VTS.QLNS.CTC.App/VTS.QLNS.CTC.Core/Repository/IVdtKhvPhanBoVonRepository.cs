using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvPhanBoVonRepository : IRepository<VdtKhvPhanBoVon>
    {
        /// <summary>
        /// get data show in Index View
        /// </summary>
        /// <returns></returns>
        IEnumerable<PhanBoVonQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon);

        /// <summary>
        /// Delete VDT_KHV_PhanBoVon
        /// </summary>
        /// <param name="data">VDT_KHV_PhanBoVon</param>
        /// <returns></returns>
        int RemovePhanBoVon(VdtKhvPhanBoVon data);

        /// <summary>
        /// Check ex
        /// </summary>
        /// <param name="iIdDonViQuanLy"></param>
        /// <param name="iNamKeHoach"></param>
        /// <returns></returns>
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVon objPhanBoVon, int iLoai);

        List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin);

        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVon objPhanBoVon, int iLoai);

        List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet);
        IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon);
        IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid);
        IEnumerable<BcDuToanTheoLoaiCongTrinhQuery> GetBcDuToanTheoLoaiCongTrinh(int iLoaiChungTu, int iNamKeHoach, double fDonViTinh, List<string> lstDonViId);
    }
}
