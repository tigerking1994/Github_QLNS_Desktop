using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQsChungTuRepository : IRepository<NsQsChungTu>
    {
        IEnumerable<int> FindMonthOfArmy(int yearOfWork);
        NsQsChungTu FindByMonth(int month, int yearOfWork);
    }
}
