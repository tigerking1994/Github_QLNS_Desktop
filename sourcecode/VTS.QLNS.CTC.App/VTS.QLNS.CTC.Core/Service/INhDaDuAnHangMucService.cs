using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaDuAnHangMucService
    {
        IEnumerable<NhDaDuAnHangMuc> FindAll(Expression<Func<NhDaDuAnHangMuc, bool>> predicate);
        int AddRange(IEnumerable<NhDaDuAnHangMuc> entities);
        int UpdateRange(IEnumerable<NhDaDuAnHangMuc> entities);
        int Delete(NhDaDuAnHangMuc entity);
        int Update(NhDaDuAnHangMuc entity);
        IEnumerable<NhDaDuAnHangMuc> FindByDuAnId(Guid duAnId);
        NhDaDuAnHangMuc FindDuAnHangMuc(params object[] keyValues);
    }
}
