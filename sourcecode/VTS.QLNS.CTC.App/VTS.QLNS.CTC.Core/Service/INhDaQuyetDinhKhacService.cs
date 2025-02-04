using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQuyetDinhKhacService
    {
        void AddRange(IEnumerable<NhDaQuyetDinhKhac> data);
        void Add(NhDaQuyetDinhKhac entity);
        void Delete(NhDaQuyetDinhKhac entity);
        void Update(NhDaQuyetDinhKhac entity);
        NhDaQuyetDinhKhac FindById(Guid id);
        IEnumerable<NhDaQuyetDinhKhac> FindAll();
        IEnumerable<NhDaQuyetDinhKhac> FindAll(Expression<Func<NhDaQuyetDinhKhac, bool>> predicate);
        IEnumerable<NhDaQuyetDinhKhacQuery> FindIndex(int iLoai);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<NhDaQuyetDinhKhac> FindByCondition(Expression<Func<NhDaQuyetDinhKhac, bool>> predicate);

    }
}
