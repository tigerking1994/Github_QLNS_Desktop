using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtQuyetToanRepository : IRepository<VdtQtQuyetToan>
    {
        IEnumerable<VdtQtQuyetToanQuery> FindAllPheDuyetQuyetToan(int yearOfWork, string userName);
        void UpdateTienQuyetToan(string quyetToanId);
        List<NguonVonQuyetToanQuery> GetNguonVonByDuToanIdDeNghiQuyetToanId(string duToanId, string deNghiQuyetToanId);
        int LockOrUnLock(Guid id, bool lockStatus);

        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
    }
}
