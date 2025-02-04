using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDmMucLucQuyetToanRepository : IRepository<NsMucLucQuyetToanNam>
    {
        void AddOrUpdateRange(IEnumerable<NsMucLucQuyetToanNam> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NsMucLucQuyetToanNamMLNS> GetAllMlns(IEnumerable<string> ids, int yearOfWork);
        void AddRangeMlns(IEnumerable<NsMucLucQuyetToanNamMLNS> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NsMucLucQuyetToanNam> FindByCondition(Expression<Func<NsMucLucQuyetToanNam, bool>> predicate);
        void ClearMlnsByIdMlqt(List<string> lstIdDmCongKhai);
        void UpdateIsHangCha();
    }
}
