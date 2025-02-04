using System;
using System.Collections;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQsChungTuChiTietRepository : IRepository<NsQsChungTuChiTiet>
    {
        IEnumerable<QsChungTuChiTietQuery> FindByCondition(ArmyVoucherDetailCriteria condition);
        IEnumerable<ReportQuanSoDonViQuery> FindForAgencyReport(int yearOfWork, string agencyId, string period);
        List<string> FindForAgencyHasvalueReport(int yearOfWork, string agencyId, string period);        
        IEnumerable<ReportQuanSoDonViQuery> FindForAgencyDetailReport(int yearOfWork, string agencyId, string period);
        IEnumerable<ReportQuanSoTongHopQuery> FindForSummaryReport(int yearOfWork, string agencyId, string period);
        IEnumerable<ReportQuanSoTongHopQuery> FindForAverage(int yearOfWork, string agencyId, string period, ReportArmy reportType, int soThang);
        IEnumerable<ReportQuanSoThuongXuyenQuery> FindForRegular(int month1, int month2, int month3, int month4, int yearOfWork, string agencyId);
        IEnumerable<ReportQuanSoRaQuanQuery> FindForLeave(int yearOfWork, string agencyId, string period);
        IEnumerable<ReportQuanSoRaQuanQuery> FindForLeaveBefore(int month, int yearOfWork, string agencyId);
        IEnumerable<ReportQuanSoLienThamQuery> FindForJurisprudence(int month, int yearOfWork, string agencyId);
        void DeleteInputData(Guid armyVoucherId, int month, string agencyId);
        void DeleteByVoucherId(Guid voucherId);
        void CreateDetail(Guid voucherId, int yearOfWork, int month, string userName);
        void UpdateDetail(int yearOfWork, int month, string idMaDonVi);
        IEnumerable<NsQsChungTuChiTiet> UpdateDetailYearBegin(int yearOfWork, string idMaDonVi);
    }
}
