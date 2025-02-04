using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtQuyetToanService
    {
        IEnumerable<VdtQtQuyetToanQuery> FindAllPheDuyetQuyetToan(int yearOfWork, string userName);
        int Add(VdtQtQuyetToan entity);
        int Update(VdtQtQuyetToan entity);
        VdtQtQuyetToan Find(params object[] keyValues);
        int Delete(Guid Id);
        void UpdateTienQuyetToan(string quyetToanId);
        List<NguonVonQuyetToanQuery> GetNguonVonByDuToanIdDeNghiQuyetToanId(string duToanId, string deNghiQuyetToanId);
        int AddRange(IEnumerable<VdtQtQuyetToan> entities);
        int LockOrUnLock(Guid id, bool lockStatus);

        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
    }
}
