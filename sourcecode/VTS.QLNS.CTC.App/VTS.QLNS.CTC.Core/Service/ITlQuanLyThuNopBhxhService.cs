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
    public interface ITlQuanLyThuNopBhxhService
    {
        void SaveEntitiesAndDetails(List<TlQuanLyThuNopBhxh> entities, List<TlQuanLyThuNopBhxhChiTiet> details);
        void UpdateDetailBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);
        void DeleteModelAndDetail(int thang, int nam, string maDonVi, string maCachTl, Guid? idTongHop = null, bool isTongHop = false);
        int DeleteDetail(string siIdDetail);
        IEnumerable<TlQuanLyThuNopBhxhQuery> FindByThangByNam(int nam);
        IEnumerable<TlQuanLyThuNopBhxh> FindByMonth(int thang);
        IEnumerable<TlQuanLyThuNopBhxh> FindByCondition(Expression<Func<TlQuanLyThuNopBhxh, bool>> predicate);
        void UpdateEntitiesAndDetails(string idXoa, List<TlQuanLyThuNopBhxh> entities, List<TlQuanLyThuNopBhxhChiTiet> details);
        int Add(TlQuanLyThuNopBhxh entity);
        int Update(TlQuanLyThuNopBhxh entity);
        int Delete(TlQuanLyThuNopBhxh entity);
        void LockOrUnlock(Guid id, bool isLock);
    }
}
