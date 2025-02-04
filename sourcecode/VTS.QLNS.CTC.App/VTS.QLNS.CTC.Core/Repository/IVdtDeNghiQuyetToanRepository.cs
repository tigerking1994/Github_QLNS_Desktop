using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDeNghiQuyetToanRepository : IRepository<VdtQtDeNghiQuyetToan>
    {
        VdtQtDeNghiQuyetToan FindByDuAnId(Guid duAnId);
        IEnumerable<VdtQtDeNghiQuyetToan> FindLstDeNghiQTByDuAnId(Guid duAnId);
        void UpdateTotal(string voucherId);
        List<ReportQuyetToanHoanThanhQuery> GetDataReportQuyetToanHoanThanh(int namLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        void TongHopDeNghiQuyetToan(VdtQtDeNghiQuyetToan vdtTtDeNghiQT, List<Guid> voucherAgregatesIds);
        List<VdtQtDeNghiQuyetToan> FindDeNghiTongHop();
    }
}
