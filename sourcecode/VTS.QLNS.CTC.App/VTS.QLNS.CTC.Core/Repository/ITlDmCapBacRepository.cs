using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public interface ITlDmCapBacRepository : IRepository<TlDmCapBac>
    {
        TlDmCapBac FindByMaCapBac(string maCapBac);
        IEnumerable<TlDmCapBac> FindByNote();
        IEnumerable<TlDmCapBac> FindParent();
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
    }
}
