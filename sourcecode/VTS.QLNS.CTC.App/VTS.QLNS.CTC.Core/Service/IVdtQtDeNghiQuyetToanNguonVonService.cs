using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtDeNghiQuyetToanNguonVonService
    {
        VdtQtDeNghiQuyetToanNguonvon Add(VdtQtDeNghiQuyetToanNguonvon entity);
        VdtQtDeNghiQuyetToanNguonvon Find(params object[] keyValues);
        int Update(VdtQtDeNghiQuyetToanNguonvon entity);
        int Delete(Guid id);
        public IEnumerable<VdtQtDeNghiQuyetToanNguonvon> FindByCondition(Expression<Func<VdtQtDeNghiQuyetToanNguonvon, bool>> predicate);
        int AddRange(IEnumerable<VdtQtDeNghiQuyetToanNguonvon> entities);
        void DeleteByDeNghiQuyetToanId(Guid deNghiQuyetToanId);
        List<VdtQtDeNghiQuyetToanNguonvon> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId);
        List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByDuToanId(string duToanId);
        List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByQDDTId(string duToanId);
    }
}
