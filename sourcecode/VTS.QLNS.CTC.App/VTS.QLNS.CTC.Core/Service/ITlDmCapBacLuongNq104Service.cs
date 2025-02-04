using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCapBacLuongNq104Service
    {
        IEnumerable<TlDmCapBacLuongNq104> FindAll();
        TlDmCapBacLuongNq104 FindByMaCapBac(string maCapBac, int? nam);
        TlDmCapBacLuongNq104 FindById(Guid id);
        IEnumerable<TlDmCapBacLuongNq104> FindByNote();
        IEnumerable<TlDmCapBacLuongNq104> FindParent();
        IEnumerable<TlDmCapBacLuongNq104> FindAll(Expression<Func<TlDmCapBacLuongNq104, bool>> predicate);
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
        IEnumerable<TlDmCapBacLuongNq104> FindAllByXauNoiMa(string xauNoiMa, int nam);
    }
}
