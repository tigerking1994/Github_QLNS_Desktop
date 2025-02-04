using System;
using System.Collections.Generic;
using System.Data;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsMucLucNganSachRepository : IRepository<NsMucLucNganSach>
    {
        IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindMLNSByNamLamViec(int namLamViec);
        List<NsMucLucNganSach> FindByDefenseBudget(BudgetIndexForBudgetCriteria searchCondition);
        List<NsMucLucNganSach> FindByStateBudget(BudgetIndexForBudgetCriteria searchCondition);
        IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(List<string> lns, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByLnsCondition(string chungTuId, int namLamViec, DateTime ngayChungTu, int type);
        List<NsMucLucNganSach> FindByParentCd(int iNamKeHoach, Guid iIdLoaiNganSach, string sL, string sK, string sM, string sTM, string sTTM);
        IEnumerable<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType);
        IEnumerable<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType, string loaiQuyetToan);
        IEnumerable<LNSQuery> FindBySettlementEstimateMonth(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType);
        IEnumerable<ReportMLNSQuery> FindChildMlns(Guid mlnsId, IEnumerable<string> mlnsIdInclude, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByVoucherlist(int yearOfWork, int yearOfBudget, int budgetSource, VoucherListLNS displayType);
        IEnumerable<LNSQuery> FindBySummaryYearSettlement(int yearOfWork, int budgetSource, int dataType, string userName);
        IEnumerable<LNSQuery> FindBySummaryYearSettlement(string yearOfBudget, int yearOfWork, int budgetSource, int dataType, string type);
        IEnumerable<NsMucLucNganSach> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, DateTime voucherDate, bool hasDuToan, string userName, string agencyIds);
        IEnumerable<NsMucLucNganSach> FindByLoaiNganSach(string lstNamLamViec);
        IEnumerable<NsMucLucNganSach> FindBySummaryVoucherList(int yearOfWork, int quarterMonth);
        IEnumerable<ReportMLNSQuery> FindChildMlnsByParent(string mlnsId, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNS, string LNSExcept);
        IEnumerable<NsMucLucNganSach> FindForPhuCap(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByLnsAndNam(string lns, int nam);
        IEnumerable<LNSQuery> FindBySoQuyetDinhDuToan(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh);
        IEnumerable<ReportMLNSQuery> FindChildMlnsByParentLNS(string mlnsId, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindForDieuChinh(int namLamViec, int namNganSach, int nguonNganSach, string donVi, int loaiChungTu, DateTime ngayChungTu, string userName);
        IEnumerable<NsMucLucNganSach> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string loaiQuyetToan, string userName);
        DataTable FindLNSByYear(int yearOfWork);
        IEnumerable<MucLucNganSachCheckDataQuery> FindMlnsEstimateSettlementByYearOfBudget(int yearOfBuget);

    }
}
