using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKtKhoiTaoDuLieuChiTietService
    {
        VdtKtKhoiTaoDuLieuChiTiet Add(VdtKtKhoiTaoDuLieuChiTiet entity);
        VdtKtKhoiTaoDuLieuChiTiet Find(params object[] keyValues);
        int Update(VdtKtKhoiTaoDuLieuChiTiet entity);
        int Delete(Guid id);
        public IEnumerable<VdtKtKhoiTaoDuLieuChiTiet> FindByCondition(Expression<Func<VdtKtKhoiTaoDuLieuChiTiet, bool>> predicate);
        int AddRange(IEnumerable<VdtKtKhoiTaoDuLieuChiTiet> entities);
        IEnumerable<KhoiTaoDuLieuChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao);
    }
}
