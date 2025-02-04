using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuNq104Repository : IRepository<TlQtChungTuNq104>
    {
        IEnumerable<TlQtChungTuNq104> FindChungTuExist(int yearOfWork, int thang, string maDonVi);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
