using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktNganhThamDinhChiTietService
    {
        NsSktNganhThamDinhChiTiet Add(NsSktNganhThamDinhChiTiet entity);
        NsSktNganhThamDinhChiTiet Find(params object[] keyValues);
        int Update(NsSktNganhThamDinhChiTiet entity);
        int Delete(Guid id);
        IEnumerable<NsSktNganhThamDinhChiTiet> FindByCondition(Expression<Func<NsSktNganhThamDinhChiTiet, bool>> predicate);
        IEnumerable<ThDChungTuChiTietQuery> FindByCondition(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
        IEnumerable<ThDChungTuChiTietQuery> FindByConditionNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
        IEnumerable<ThDChungTuChiTietQuery> FindByConditionReport(int namLamViec, string nganh, string idChungTu, int namNganSach, int nguonNganSach);
        int AddRange(IEnumerable<NsSktNganhThamDinhChiTiet> entities);
        void DeleteByVoucherId(Guid voucherId);
        IEnumerable<ThDChungTuReportNSBDQuery> GetDataReportNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
    }
}
