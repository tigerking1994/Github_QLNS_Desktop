using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDeNghiQuyetToanService
    {
        int Add(VdtQtDeNghiQuyetToan entity);
        VdtQtDeNghiQuyetToan Find(params object[] keyValues);
        VdtQtDeNghiQuyetToan FindByDuAnId(Guid duAnId);
        IEnumerable<VdtQtDeNghiQuyetToan> FindLstDeNghiQTByDuAnId(Guid duAnId);
        int Update(VdtQtDeNghiQuyetToan entity);
        int Delete(Guid Id);
        void UpdateTotal(string voucherId);
        List<ReportQuyetToanHoanThanhQuery> GetDataReportQuyetToanHoanThanh(int namLamViec);
        int AddRange(IEnumerable<VdtQtDeNghiQuyetToan> entities);
        int LockOrUnLock(Guid id, bool lockStatus);
        void TongHopDeNghiQuyetToan(VdtQtDeNghiQuyetToan vdtTtDeNghiQT, List<Guid> voucherAgregatesIds);
        List<VdtQtDeNghiQuyetToan> FindDeNghiTongHop();
    }
}
