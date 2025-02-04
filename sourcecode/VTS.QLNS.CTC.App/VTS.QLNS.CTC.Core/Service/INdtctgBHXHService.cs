using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INdtctgBHXHService
    {
        IEnumerable<BhDtctgBHXH> FindByCondition(Expression<Func<BhDtctgBHXH, bool>> predicate);
        IEnumerable<BhDtctgBHXHQuery> FindByYear(int namLamViec);
        BhDtctgBHXH FindById(Guid Id);
        int Delete(BhDtctgBHXH item);
        int Add(BhDtctgBHXH item);
        int AddRange(IEnumerable<BhDtctgBHXH> items);
        int Update(BhDtctgBHXH item);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        IEnumerable<BhDtctgBHXHQuery> GetDuToanDanhSachDotNhanPhanBo(int iNamLamViec, DateTime date, int iLoaiDuToanNhan);
        bool IsExitsDuToanDaDuocPhanBo(Guid iDuToanNhan, Guid iDuToanPhanBo);
    }
}
