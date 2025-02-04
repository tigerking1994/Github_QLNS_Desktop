using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmPhuCapNq104Repository : IRepository<TlDmPhuCapNq104>
    {
        IEnumerable<TlDmPhuCapNq104> FindByCondition();
        IEnumerable<TlDmPhuCapNq104> FindByHeThong();
        IEnumerable<TlDmPhuCapNq104> FindAllPhuCapHeThong();
        TlDmPhuCapNq104 FindByMaPhuCap(string maPhuCap);
        IEnumerable<TlDmPhuCapNq104> GetDmPhuCapInDcTapTheCanBo();
        IEnumerable<TlDmPhuCapNq104> FindHasDataBangLuong(int nam, int thang, string maCachTl);
        void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete);
        bool CheckPhuCapExist(string maPhuCap, Guid iId);
        IEnumerable<TlPhuCapNq104Query> FindAllPhuCapVaCheDoBHXH();
        IEnumerable<TlDmPhuCapNq104> FindByIdThuNopBhxh(Guid id);

    }
}
