using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQsChungTuChiTietService
    {
        NsQsChungTuChiTiet FindById(Guid id);
        List<QsChungTuChiTietQuery> FindByCondition(ArmyVoucherDetailCriteria condition);
        List<NsQsChungTuChiTiet> FindByCondition(Expression<Func<NsQsChungTuChiTiet, bool>> predicate);
        void AddRange(List<NsQsChungTuChiTiet> chungTuChiTiets);
        void Update(NsQsChungTuChiTiet chungTuChiTiet);
        void UpdateRange(List<NsQsChungTuChiTiet> chungTuChiTiet);
        void Delete(Guid id);
        void RemoveRange(List<NsQsChungTuChiTiet> entities);
        List<ReportQuanSoDonViQuery> FindForAgencyReport(int yearOfWork, string agencyId, string period);
        List<string> FindForAgencyHasvalueReport(int yearOfWork, string agencyId, string period);
        List<ReportQuanSoDonViQuery> FindForAgencyDetailReport(int yearOfWork, string agencyId, string period);
        List<ReportQuanSoTongHopQuery> FindForSummaryReport(int yearOfWork, string agencyId, string period);
        List<ReportQuanSoTongHopQuery> FindForAverage(int yearOfWork, string agencyId, string period, ReportArmy reportType, int soThang);
        List<ReportQuanSoThuongXuyenQuery> FindForRegular(int month1, int month2, int month3, int month4, int yearOfWork, string agencyId);
        List<ReportQuanSoRaQuanQuery> FindForLeave(int yearOfWork, string agencyId, string period);
        ReportQuanSoRaQuanQuery FindForLeaveBefore(int month, int yearOfWork, string agencyId);
        List<ReportQuanSoLienThamQuery> FindForJurisprudence(int month, int yearOfWork, string agencyId);
        void DeleteInputData(Guid armyVoucherId, int month, string agencyId);
        void DeleteByVoucherId(Guid voucherId);
        void CreateDetail(Guid voucherId, int yearOfWork, int month, string userName);
        void UpdateDetail(int yearOfWork, int month, string idMaDonVi);
        IEnumerable<NsQsChungTuChiTiet> UpdateDetailYearBegin(int yearOfWork, string idMaDonVi);
    }
}
