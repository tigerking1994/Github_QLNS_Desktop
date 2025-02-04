using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoachVonUngRepository : IRepository<VdtKhvKeHoachVonUng>
    {
        IEnumerable<VdtKhvKeHoachVonUngQuery> GetKeHoachVonUngIndex();        
        IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<YearPlanManagerExportCriteria> lstData);
        bool CheckTrungSoQuyetDinh(string sSoQuyetDinh, Guid id);
    }
}
