using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktNganhThamDinhChiTietRepository : IRepository<NsSktNganhThamDinhChiTiet>
    {
        IEnumerable<ThDChungTuChiTietQuery> FindByCondition(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
        IEnumerable<ThDChungTuChiTietQuery> FindByConditionNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
        IEnumerable<ThDChungTuChiTietQuery> FindByConditionReport(int namLamViec, string idChungTu, string nganh, int namNganSach, int nguonNganSach);
        void DeleteByVoucherId(Guid voucherId);
        IEnumerable<ThDChungTuReportNSBDQuery> GetDataReportNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach);
    }
}
