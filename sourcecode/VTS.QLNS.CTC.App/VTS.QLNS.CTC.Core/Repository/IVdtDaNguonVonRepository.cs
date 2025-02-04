using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaNguonVonRepository : IRepository<VdtDaNguonVon>
    {
        IEnumerable<VdtDaNguonVon> FindByNguonVon(Guid idDuAn, int nguonVon);
        IEnumerable<VdtDaNguonVon> FindByIdDuAn(List<Guid?> idDuAn);
        IEnumerable<VdtDaNguonVon> FindByIdDuAn(Guid idDuAn);
    }
}
