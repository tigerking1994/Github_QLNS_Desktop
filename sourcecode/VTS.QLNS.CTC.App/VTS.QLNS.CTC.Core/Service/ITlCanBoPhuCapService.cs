using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapService
    {
        int AddRange(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        IEnumerable<TlCanBoPhuCap> FindByMaCanBo(string maCanBo);
        int DeleteByMaCanBo(string maCanBo);
        int UpdateRange(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        IEnumerable<TlCanBoPhuCap> FindAll();
        int UpdateRang(List<TlCanBoPhuCap> entities);
        IEnumerable<TlCanBoPhuCap> FindAll(Expression<Func<TlCanBoPhuCap, bool>> predicate);
        int Update(TlCanBoPhuCap tlCanBoPhuCap);
        void BulkInsert(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        void BulkUpdate(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo);
        int DeleteCanBo(string maCanBo);
        TlCanBoPhuCap FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap);
        IEnumerable<TLCanBoPhuCapQuery> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue);
        IEnumerable<TLCanBoPhuCapQuery> FindCanBoPhuCapByMaCanBo(string maCanBo);
    }
}
