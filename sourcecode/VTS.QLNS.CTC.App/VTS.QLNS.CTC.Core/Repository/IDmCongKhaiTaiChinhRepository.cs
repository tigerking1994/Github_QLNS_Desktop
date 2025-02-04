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
    public interface IDmCongKhaiTaiChinhRepository : IRepository<NsDanhMucCongKhai>
    {
        void AddOrUpdateRange(IEnumerable<NsDanhMucCongKhai> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NsDmCongKhaiMlns> GetAllMlns(IEnumerable<Guid> ids);
        void AddRangeMlns(IEnumerable<NsDmCongKhaiMlns> entities, AuthenticationInfo authenticationInfo);

        IEnumerable<NsDanhMucCongKhai> FindByCondition(Expression<Func<NsDanhMucCongKhai, bool>> predicate);
        IEnumerable<NsDmCongKhaiMlns> FindByCondition(Expression<Func<NsDmCongKhaiMlns, bool>> predicate);

        IEnumerable<PrintPublicFinanceQuery> ReportPublicFinance(int yearOfWork, string ids, int yearOfBudget, int budgetSource);
        void ClearMlnsByiIdDmCongKhai(List<Guid> lstIdDmCongKhai);
        void UpdateIsHangCha();
        IEnumerable<NsDanhMucCongKhai> GetByXauNoiMaMlns(string xauNoiMa, int namLamViec);
    }
}
