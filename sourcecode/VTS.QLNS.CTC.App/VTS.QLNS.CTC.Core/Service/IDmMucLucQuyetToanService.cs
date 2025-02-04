using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDmMucLucQuyetToanService
    {
        void Add(NsMucLucQuyetToanNam entity);
        void Update(NsMucLucQuyetToanNam entity);
        void Delete(NsMucLucQuyetToanNam entity);
        void AddRange(IEnumerable<NsMucLucQuyetToanNam> entities);
        void UpdateRange(IEnumerable<NsMucLucQuyetToanNam> entities);
        void AddOrUpdateRange(IEnumerable<NsMucLucQuyetToanNam> entities, AuthenticationInfo authenticationInfo);
        
        IEnumerable<NsMucLucQuyetToanNam> FindByCondition(Expression<Func<NsMucLucQuyetToanNam, bool>> predicate);
        IEnumerable<NsMucLucQuyetToanNamMLNS> GetAllMlns(IEnumerable<string> ids, int yearOfWork);

    }
}
