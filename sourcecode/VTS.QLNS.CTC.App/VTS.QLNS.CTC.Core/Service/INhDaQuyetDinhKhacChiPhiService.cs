using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQuyetDinhKhacChiPhiService
    {
        void AddRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data);
        void Add(NhDaQuyetDinhKhacChiPhi entity);
        void Delete(NhDaQuyetDinhKhacChiPhi entity);
        void Update(NhDaQuyetDinhKhacChiPhi entity);
        NhDaQuyetDinhKhacChiPhi FindById(Guid id);
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByQuyetDinhKhacId(Guid id);
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll();
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll(AuthenticationInfo authentication);
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll(Expression<Func<NhDaQuyetDinhKhacChiPhi, bool>> predicate);
        void UpdateRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data);
        void RemoveRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data);
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByCondition(Expression<Func<NhDaQuyetDinhKhacChiPhi, bool>> predicate);

    }
}
