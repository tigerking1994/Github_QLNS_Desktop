using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtQuyetToanChiTietService
    {
        VdtQtQuyetToanChiTiet Add(VdtQtQuyetToanChiTiet entity);
        VdtQtQuyetToanChiTiet Find(params object[] keyValues);
        int Update(VdtQtQuyetToanChiTiet entity);
        int Delete(Guid id);
        public IEnumerable<VdtQtQuyetToanChiTiet> FindByCondition(Expression<Func<VdtQtQuyetToanChiTiet, bool>> predicate);
        void DeleteByQuyetToanId(Guid deNghiQuyetToanId);
        int AddRange(IEnumerable<VdtQtQuyetToanChiTiet> entities);
        List<VdtQtQuyetToanChiTiet> FindByQuyetToanId(Guid quyetToanId);
        void UpdateTotal(string voucherId);
    }
}
