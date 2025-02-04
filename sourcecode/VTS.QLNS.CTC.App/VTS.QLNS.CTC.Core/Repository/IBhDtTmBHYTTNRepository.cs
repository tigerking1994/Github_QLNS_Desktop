using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDtTmBHYTTNRepository : IRepository<BhDtTmBHYTTN>
    {
        IEnumerable<BhDtTmBHYTTNQuery> FindByCondition(int namLamViec);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhDtTmBHYTTNQuery> GetDanhSachDotNhanPhanBo(int yearOfWork, DateTime date, int iLoaiDuToan);

    }
}
