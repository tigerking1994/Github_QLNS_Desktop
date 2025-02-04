using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmPhuCapRepository : IRepository<TlDmPhuCap>
    {
        IEnumerable<TlDmPhuCap> FindByCondition();
        IEnumerable<TlDmPhuCap> FindByHeThong();
        IEnumerable<TlDmPhuCap> FindAllPhuCapHeThong();
        IEnumerable<TlDmPhuCap> FindByBHXHPhuCapNotIn(int namLamViec, string pcLoai, string mlnsBHXH, string pcChosen);
        TlDmPhuCap FindByMaPhuCap(string maPhuCap);
        IEnumerable<TlDmPhuCap> GetDmPhuCapInDcTapTheCanBo();
        IEnumerable<TlDmPhuCap> FindHasDataBangLuong(int nam, int thang, string maCachTl);
        void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete);
        bool CheckPhuCapExist(string maPhuCap, Guid iId);
        IEnumerable<TlPhuCapQuery> FindAllPhuCapVaCheDoBHXH();
        IEnumerable<TlDmPhuCap> FindByIdThuNopBhxh(Guid id);
        string FindByBHXHPhuCapIn(int namLamViec, string mlnsLoai, string mlnsBhxh);

    }
}
