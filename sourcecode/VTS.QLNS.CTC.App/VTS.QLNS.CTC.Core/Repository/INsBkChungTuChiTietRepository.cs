using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsBkChungTuChiTietRepository : IRepository<NsBkChungTuChiTiet>
    {
        IEnumerable<NsBkChungTuChiTietQuery> FindByVoucherListId(Guid voucherListId, int yearOfWork);
        IEnumerable<ReportBangKeTongHopQuery> FindBySummaryVoucherList(ReportVoucherListCriteria condition);
        void DeleteByVoucherId(Guid voucherId);
        void DeleteByListVoucherId(List<Guid> listVoucherId);
    }
}
