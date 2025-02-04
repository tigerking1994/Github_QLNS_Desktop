using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public interface ITlDmCapBacLuongNq104Repository : IRepository<TlDmCapBacLuongNq104>
    {
        TlDmCapBacLuongNq104 FindByMaCapBac(string maCapBac, int? nam);
        TlDmCapBacLuongNq104 FindByXauNoiMa(string xauNoiMa, int? nam);
        IEnumerable<TlDmCapBacLuongNq104> FindByNote();
        IEnumerable<TlDmCapBacLuongNq104> FindParent();
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
        IEnumerable<TlDmCapBacLuongNq104> FindAllByXauNoiMa(string xauNoiMa, int nam);
    }
}
