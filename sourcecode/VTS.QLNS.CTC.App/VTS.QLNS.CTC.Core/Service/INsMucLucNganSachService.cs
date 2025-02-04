using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsMucLucNganSachService
    {
        IEnumerable<NsMucLucNganSach> FindAll(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindAll(int namLamViec, int loaiChungTu);
        IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, int loaiChungTu = 1);
        IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, int status, int loaiChungTu = 1);
        List<NsMucLucNganSach> FindByDefenseBudget(BudgetIndexForBudgetCriteria searchCondition);
        List<NsMucLucNganSach> FindByStateBudget(BudgetIndexForBudgetCriteria searchCondition);
        IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(List<string> lns, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByLnsCondition(string chungTuId, int namLamViec, DateTime ngayChungTu, int type);
        IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, string subLns);
        IEnumerable<NsMucLucNganSach> FindByNamLamViecForTreeLNS(int namLamViec);
        List<NsMucLucNganSach> FindByParentCd(int iNamKeHoach, Guid iIdLoaiNganSach, string sL = "", string sK = "", string sM = "", string sTM = "", string sTTM = "");
        List<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType);
        List<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType, string loaiQuyetToan);
        List<LNSQuery> FindBySettlementEstimateMonth(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType);
        IEnumerable<ReportMLNSQuery> FindChildMlns(Guid mlnsId, IEnumerable<string> mlnsIdInclude, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByVoucherList(int yearOfWork, int yearOfBudget, int budgetSource, VoucherListLNS displayType);
        IEnumerable<NsMucLucNganSach> FindByXauNoiMaAndNamLamViec(IEnumerable<string> enumerable, int yearOfWork, int loaiChungTu = 1);
        IEnumerable<LNSQuery> FindBySummaryYearSettlement(int yearOfWork, int budgetSource, int dataType, string userName);
        IEnumerable<LNSQuery> FindBySummaryYearSettlement(string yearOfBudget, int yearOfWork, int budgetSource, int dataType, string type);
        IEnumerable<NsMucLucNganSach> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, DateTime voucherDate, bool hasDuToan, string userName, string agencyIds);
        IEnumerable<NsMucLucNganSach> FindByLoaiNganSach(string lstNamLamViec);
        IEnumerable<NsMucLucNganSach> FindBySummaryVoucherList(int yearOfWork, int quarterMonth);
        void AddRange(List<NsMucLucNganSach> listMlns);
        IEnumerable<ReportMLNSQuery> FindChildMlnsByParent(string lstMlnsId, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindMLNSByNamLamViec(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByCondition(Expression<Func<NsMucLucNganSach, bool>> predicate);
        Expression<Func<NsMucLucNganSach, bool>> createPredicateAllNull();
        List<NsMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNS, string LNSExcept);
        IEnumerable<NsMucLucNganSach> FindForPhuCap(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByLnsAndNam(string lns, int nam);
        void AddRangeWithMLSKT(List<NsMucLucNganSach> listMlns);
        List<LNSQuery> FindBySoQuyetDinhDuToan(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh);
        IEnumerable<ReportMLNSQuery> FindChildMlnsByParentLNS(string mlnsId, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindForDieuChinh(int namLamViec, int namNganSach, int nguonNganSach, string donVi, int loaiChungTu, DateTime ngayChungTu, string userName);
        IEnumerable<NsMucLucNganSach> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string loaiQuyetToan, string userName);
        DataTable FindLNSByYear(int yearOfWork);
        IEnumerable<MucLucNganSachCheckDataQuery> FindMlnsEstimateSettlementByYearOfBudget(int yearOfBuget);

    }
}
