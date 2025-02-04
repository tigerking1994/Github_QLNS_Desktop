using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsPhongBanService
    {
        List<DmBQuanLy> FindByCondition(Expression<Func<DmBQuanLy, bool>> predicate);
        int countPhongBanByNamLamViec(int namLamViec);
    }
}
