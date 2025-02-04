using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INdtctgBHXHRepository : IRepository<BhDtctgBHXH>
    {
        IEnumerable<BhDtctgBHXH> FindByCondition(Expression<Func<BhDtctgBHXH, bool>> predicate);
        IEnumerable<BhDtctgBHXHQuery> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        IEnumerable<BhDtctgBHXHQuery> GetDuToanDanhSachDotNhanPhanBo(int iNamLamViec, DateTime date, int iLoaiDuToanNhan);
        bool IsExitsDuToanDaDuocPhanBo(Guid iDuToanNhan, Guid iDuToanPhanBo);
    }
}
