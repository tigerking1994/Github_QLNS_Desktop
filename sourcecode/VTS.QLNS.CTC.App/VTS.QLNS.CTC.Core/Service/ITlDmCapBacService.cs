using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCapBacService
    {
        IEnumerable<TlDmCapBac> FindAll();
        TlDmCapBac FindByMaCapBac(string maCapBac);
        TlDmCapBac FindById(Guid id);
        IEnumerable<TlDmCapBac> FindByNote();
        IEnumerable<TlDmCapBac> FindParent();
        IEnumerable<TlDmCapBac> FindAll(Expression<Func<TlDmCapBac, bool>> predicate);
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
    }
}
