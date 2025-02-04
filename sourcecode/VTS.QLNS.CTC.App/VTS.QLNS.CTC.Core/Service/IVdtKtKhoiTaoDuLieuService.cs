using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKtKhoiTaoDuLieuService
    {
        VdtKtKhoiTaoDuLieu Add(VdtKtKhoiTaoDuLieu entity);
        VdtKtKhoiTaoDuLieu Find(params object[] keyValues);
        int Update(VdtKtKhoiTaoDuLieu entity);
        int Delete(Guid id);
        public IEnumerable<VdtKtKhoiTaoDuLieu> FindByCondition(Expression<Func<VdtKtKhoiTaoDuLieu, bool>> predicate);
        IEnumerable<KhoiTaoDuLieuQuery> FindByCondition(int namLamViec);
    }
}
