using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtDeNghiQuyetToanChiTietService
    {
        VdtQtDeNghiQuyetToanChiTiet Add(VdtQtDeNghiQuyetToanChiTiet entity);
        VdtQtDeNghiQuyetToanChiTiet Find(params object[] keyValues);
        int Update(VdtQtDeNghiQuyetToanChiTiet entity);
        int Delete(Guid id);
        public IEnumerable<VdtQtDeNghiQuyetToanChiTiet> FindByCondition(Expression<Func<VdtQtDeNghiQuyetToanChiTiet, bool>> predicate);
        List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAn(Guid duAnId);
        int AddRange(IEnumerable<VdtQtDeNghiQuyetToanChiTiet> entities);
        void DeleteByDeNghiQuyetToanId(Guid deNghiQuyetToanId);
        List<VdtQtDeNghiQuyetToanChiTiet> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId);
        List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAnNew(Guid duAnId);
    }
}
