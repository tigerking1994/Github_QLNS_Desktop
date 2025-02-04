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
    public interface IBhQtcQuyKinhPhiQuanLyService
    {
        void Add(BhQtcQuyKinhPhiQuanLy entity);
        void Delete(Guid id);
        void Update(BhQtcQuyKinhPhiQuanLy entity);
        IEnumerable<BhQtcQuyKinhPhiQuanLyQuery> FindIndex(int iNamChungTu);
        BhQtcQuyKinhPhiQuanLy FindById(Guid id);
        IEnumerable<BhQtcQuyKinhPhiQuanLy> FindByCondition(Expression<Func<BhQtcQuyKinhPhiQuanLy, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);
        int GetSoChungTuIndexByCondition(int namLamViec);
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu);
    }
}
