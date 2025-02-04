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
    public interface IDmCongKhaiTaiChinhService
    {
        void Add(NsDanhMucCongKhai entity);
        void Update(NsDanhMucCongKhai entity);
        void Delete(NsDanhMucCongKhai entity);
        void AddRange(IEnumerable<NsDanhMucCongKhai> entities);
        void UpdateRange(IEnumerable<NsDanhMucCongKhai> entities);
        void AddOrUpdateRange(IEnumerable<NsDanhMucCongKhai> entities, AuthenticationInfo authenticationInfo);
        
        IEnumerable<NsDanhMucCongKhai> FindByCondition(Expression<Func<NsDanhMucCongKhai, bool>> predicate);
        IEnumerable<NsDmCongKhaiMlns> FindByConditionMLNS(Expression<Func<NsDmCongKhaiMlns, bool>> predicate);

        IEnumerable<PrintPublicFinanceQuery> ReportPublicFinance(int yearOfWork, string ids, int yearOfBudget, int budgetSource);

        IEnumerable<NsDmCongKhaiMlns> GetAllMlns(IEnumerable<Guid> ids);

        IEnumerable<NsDanhMucCongKhai> FindByXauNoiMa(string xauNoiMa, int namLamViec);

    }
}
