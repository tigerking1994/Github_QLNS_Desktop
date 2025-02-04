using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaDuAnNguonVonService
    {
        IEnumerable<NhDaDuAnNguonVon> FindAll(Expression<Func<NhDaDuAnNguonVon, bool>> predicate);
        int AddRange(List<NhDaDuAnNguonVon> entities);
        int InsertDuAnNguonVon(IEnumerable<NhDaDuAnNguonVon> entities);
        int Update(NhDaDuAnNguonVon entity);
        int Delete(NhDaDuAnNguonVon entity);
        IEnumerable<NhDaDuAnNguonVon> FindByDuAnId(Guid IdDuAn);
        NhDaDuAnNguonVon FindById(params object[] keyValues);
    }
}
