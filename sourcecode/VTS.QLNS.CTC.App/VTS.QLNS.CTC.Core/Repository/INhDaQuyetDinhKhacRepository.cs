using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaQuyetDinhKhacRepository : IRepository<NhDaQuyetDinhKhac>
    {
        IEnumerable<NhDaQuyetDinhKhac> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaQuyetDinhKhacQuery> FindIndex(int iLoai);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
    }
}
