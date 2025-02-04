using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDaNguonVonService
    {
        int AddRange(IEnumerable<VdtDaNguonVon> entities);
        IEnumerable<VdtDaNguonVon> FindAll();
        IEnumerable<VdtDaNguonVon> FindAll(Expression<Func<VdtDaNguonVon, bool>> predicate);
        VdtDaNguonVon FindById(Guid id);
        int Update(VdtDaNguonVon entity);
        int Delete(Guid id);
        VdtDaNguonVon Add(VdtDaNguonVon entity);
        IEnumerable<VdtDaNguonVon> FindByNguonVon(Guid idDuAn, int nguonVon);
        IEnumerable<VdtDaNguonVon> FindByIdDuAn(List<Guid?> idDuAn);
        IEnumerable<VdtDaNguonVon> FindByIdDuAn(Guid idDuAn);
        void DeleteByIdDuAn(Guid idDuAn);
    }
}
