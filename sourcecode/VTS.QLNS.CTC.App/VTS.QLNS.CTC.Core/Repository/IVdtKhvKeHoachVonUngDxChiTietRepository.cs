using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoachVonUngDxChiTietRepository : IRepository<VdtKhvKeHoachVonUngDxChiTiet>
    {
        IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetDuAnInKeHoachVonUngDetail(string iIdDonVi, DateTime dNgayLap, string sTongHop);
        IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        void DeleteKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        void InsertKhVonUngDeXuatTongHop(Guid iIdKeHoachTongHop, List<Guid> lstIdChild);
        IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<YearPlanManagerExportCriteria> lstPhanboVon);
        bool CheckExistSoKeHoach(string sSoQuyetDinh, int iNamLamViec, Guid? iId);
    }
}
