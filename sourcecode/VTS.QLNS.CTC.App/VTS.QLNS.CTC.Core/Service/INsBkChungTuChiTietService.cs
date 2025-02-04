using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsBkChungTuChiTietService
    {
        NsBkChungTuChiTiet FindById(Guid id);
        List<NsBkChungTuChiTietQuery> FindByVoucherListId(Guid voucherListId, int yearOfWork);
        void AddRange(List<NsBkChungTuChiTiet> listChungTuChiTiet);
        void Update(NsBkChungTuChiTiet chungTuChiTiet);
        void Delete(Guid id);
        List<ReportBangKeTongHopQuery> FindBySummaryVoucherList(ReportVoucherListCriteria condition);
        void DeleteByVoucherId(Guid voucherId);
        void DeleteByListVoucherId(List<Guid> listVoucherId);
        List<NsBkChungTuChiTiet> FindByCondition(Expression<Func<NsBkChungTuChiTiet, bool>> predicate);
    }
}
