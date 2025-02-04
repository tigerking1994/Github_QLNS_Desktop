using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsMucLucNganSachService : IService<NsMucLucNganSach>, INsMucLucNganSachService
    {
        private readonly INsMucLucNganSachRepository _nSMucLucNganSachRepository;
        private readonly IMucLucNganSachRepository _mucLucNganSachRepository;

        public NsMucLucNganSachService(INsMucLucNganSachRepository nSMucLucNganSachRepository, IMucLucNganSachRepository mucLucNganSachRepository)
        {
            _nSMucLucNganSachRepository = nSMucLucNganSachRepository;
            _mucLucNganSachRepository = mucLucNganSachRepository;
        }

        public IEnumerable<NsMucLucNganSach> FindAll(int namLamViec)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == namLamViec && x.ITrangThai == 1);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(x => x.XauNoiMa);
        }

        public IEnumerable<NsMucLucNganSach> FindAll(int namLamViec, int loaiChungTu)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            var predicateLoaiChungTu = createPredicateLoaiChungTu(loaiChungTu);
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(x => x.ITrangThai == 1);
            predicate = predicate.And(predicateLoaiChungTu);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(x => x.XauNoiMa);
        }
        public IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, int loaiChungTu)
        {
            var predicate = createPredicateAllNull();
            var predicateLoaiChungTu = createPredicateLoaiChungTu(loaiChungTu);
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(predicateLoaiChungTu);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(n => n.Lns);
        }

        public IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, int status, int loaiChungTu)
        {
            var predicate = createPredicateAllNull();
            var predicateLoaiChungTu = createPredicateLoaiChungTu(loaiChungTu);
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(x => x.ITrangThai == status);
            predicate = predicate.And(predicateLoaiChungTu);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(n => n.Lns);
        }

        public Expression<Func<NsMucLucNganSach, bool>> createPredicateAllNull()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => string.IsNullOrEmpty(x.L));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.K));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.M));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.Tm));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.Ttm));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.Ng));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.Tng));
            return predicate;
        }

        private Expression<Func<NsMucLucNganSach, bool>> createPredicateLoaiChungTu(int loaiChungTu)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            if (VoucherType.NSSD_Key.Equals(loaiChungTu.ToString()))
            {
                predicate = predicate.And(x => !x.Lns.StartsWith("104") && !x.Lns.StartsWith("8"));
            }
            else if (VoucherType.NSBD_Key.Equals(loaiChungTu.ToString()))
            {
                predicate = predicate.And(x => x.Lns.StartsWith("104") || x.Lns == "1");
            }

            return predicate;
        }

        public List<NsMucLucNganSach> FindByStateBudget(BudgetIndexForBudgetCriteria searchCondition)
        {
            return _nSMucLucNganSachRepository.FindByStateBudget(searchCondition);
        }

        public IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec)
        {
            var mlns = _nSMucLucNganSachRepository.FindByListLnsDonVi(lns, namLamViec);
            var dict = mlns.Select(x => x.MlnsIdParent).ToHashSet();
            foreach (var item in mlns)
            {
                item.IsParent = dict.Contains(item.MlnsId);
            }
            return mlns;
        }

        public IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(List<string> lns, int namLamViec)
        {
            var mlns = _nSMucLucNganSachRepository.FindByListLnsDonVi(lns, namLamViec);
            var dict = mlns.Select(x => x.MlnsIdParent).ToHashSet();
            foreach (var item in mlns)
            {
                item.IsParent = dict.Contains(item.MlnsId);
            }
            return mlns;
        }

        public IEnumerable<NsMucLucNganSach> FindByLnsCondition(string chungTuId, int namLamViec, DateTime ngayChungTu, int type)
        {
            return _nSMucLucNganSachRepository.FindByLnsCondition(chungTuId, namLamViec, ngayChungTu, type);
        }

        public List<NsMucLucNganSach> FindByDefenseBudget(BudgetIndexForBudgetCriteria searchCondition)
        {
            return _nSMucLucNganSachRepository.FindByDefenseBudget(searchCondition);
        }

        public IEnumerable<NsMucLucNganSach> FindByMLNS(int namLamViec, string subLns)
        {
            var predicate = createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            return _nSMucLucNganSachRepository.FindAll(predicate).Where(x => x.Lns.StartsWith(subLns)).OrderBy(n => n.Lns);
        }

        public List<NsMucLucNganSach> FindByParentCd(int iNamKeHoach, Guid iIdLoaiNganSach, string sL, string sK, string sM, string sTM, string sTTM)
        {
            return _nSMucLucNganSachRepository.FindByParentCd(iNamKeHoach, iIdLoaiNganSach, sL, sK, sM, sTM, sTTM);
        }

        public List<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType)
        {
            return _nSMucLucNganSachRepository.FindBySettlementMonth(yearOfWork, budgetSource, agencyId, quarterMonth, quarterMonthType).ToList();
        }

        public List<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType, string loaiQuyetToan)
        {
            return _nSMucLucNganSachRepository.FindBySettlementMonth(yearOfWork, budgetSource, agencyId, quarterMonth, quarterMonthType, loaiQuyetToan).ToList();
        }

        public List<LNSQuery> FindBySettlementEstimateMonth(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType)
        {
            return _nSMucLucNganSachRepository.FindBySettlementEstimateMonth(yearOfWork, yearOfBudget, budgetSource, agencyId, quarterMonth, quarterMonthType).ToList();
        }


        public IEnumerable<ReportMLNSQuery> FindChildMlns(Guid mlnsId, IEnumerable<string> mlnsIdInclude, int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindChildMlns(mlnsId, mlnsIdInclude, namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByXauNoiMaAndNamLamViec(IEnumerable<string> xauNoiMa, int yearOfWork, int loaiChungTu)
        {
            if (xauNoiMa == null || !xauNoiMa.Any())
            {
                return new List<NsMucLucNganSach>();
            }

            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            var predicateLoaiChungTu = createPredicateLoaiChungTu(loaiChungTu);
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == 1);
            predicate = predicate.And(x => xauNoiMa.Contains(x.XauNoiMa));

            predicate = predicate.And(predicateLoaiChungTu);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(n => n.Lns);
        }

        public IEnumerable<LNSQuery> FindBySummaryYearSettlement(int yearOfWork, int budgetSource, int dataType, string userName)
        {
            return _nSMucLucNganSachRepository.FindBySummaryYearSettlement(yearOfWork, budgetSource, dataType, userName);
        }

        public IEnumerable<NsMucLucNganSach> FindByNamLamViecForTreeLNS(int namLamViec)
        {
            var predicate = createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(n => n.Lns);
        }

        public IEnumerable<NsMucLucNganSach> FindByLoaiNganSach(string lstNamLamViec)
        {
            return _nSMucLucNganSachRepository.FindByLoaiNganSach(lstNamLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByVoucherList(int yearOfWork, int yearOfBudget, int budgetSource, VoucherListLNS displayType)
        {
            return _nSMucLucNganSachRepository.FindByVoucherlist(yearOfWork, yearOfBudget, budgetSource, displayType);
        }

        public IEnumerable<NsMucLucNganSach> FindBySummaryVoucherList(int yearOfWork, int quarterMonth)
        {
            return _nSMucLucNganSachRepository.FindBySummaryVoucherList(yearOfWork, quarterMonth);
        }

        public void AddRange(List<NsMucLucNganSach> listMlns)
        {
            _nSMucLucNganSachRepository.AddRange(listMlns);
        }

        public IEnumerable<ReportMLNSQuery> FindChildMlnsByParent(string mlnsId, int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindChildMlnsByParent(mlnsId, namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindByNamLamViec(namLamViec);
        }
        public IEnumerable<NsMucLucNganSach> FindMLNSByNamLamViec(int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindMLNSByNamLamViec(namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByCondition(Expression<Func<NsMucLucNganSach, bool>> predicate)
        {
            return _nSMucLucNganSachRepository.FindAll(predicate).OrderBy(x => x.XauNoiMa);
        }

        public List<NsMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNS, string LNSExcept)
        {
            var mlns = _nSMucLucNganSachRepository.FindByUser(userName, yearOfWork, LNS, LNSExcept).ToList();
            var dict = mlns.Select(x => x.MlnsIdParent).ToHashSet();
            foreach (var item in mlns)
            {
                item.IsParent = dict.Contains(item.MlnsId);
            }
            return mlns;
        }

        public IEnumerable<NsMucLucNganSach> FindForPhuCap(int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindForPhuCap(namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByLnsAndNam(string lns, int nam)
        {
            return _nSMucLucNganSachRepository.FindByLnsAndNam(lns, nam);
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nSMucLucNganSachRepository.FindByLnsAndNam("1010000", authenticationInfo.YearOfWork);
        }

        public override void AddOrUpdateRange(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            _nSMucLucNganSachRepository.AddOrUpdateRange(listEntities);
        }

        public bool IsUsedMLNS(Guid mlnsId, int namLamViec)
        {
            return _mucLucNganSachRepository.IsUsedMLNS(mlnsId, namLamViec);
        }

        public void AddRangeWithMLSKT(List<NsMucLucNganSach> listMlns)
        {
            _mucLucNganSachRepository.AddRangeWithMLSKT(listMlns);
        }

        public List<LNSQuery> FindBySoQuyetDinhDuToan(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh)
        {
            return _nSMucLucNganSachRepository.FindBySoQuyetDinhDuToan(yearOfWork, yearOfBudget, budgetSource, soQuyetDinh).ToList();
        }

        public IEnumerable<ReportMLNSQuery> FindChildMlnsByParentLNS(string mlnsId, int namLamViec)
        {
            return _nSMucLucNganSachRepository.FindChildMlnsByParentLNS(mlnsId, namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindForDieuChinh(int namLamViec, int namNganSach, int nguonNganSach, string donVi, int loaiChungTu, DateTime ngayChungTu, string userName)
        {
            return _nSMucLucNganSachRepository.FindForDieuChinh(namLamViec, namNganSach, nguonNganSach, donVi, loaiChungTu, ngayChungTu, userName);
        }

        public IEnumerable<NsMucLucNganSach> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, DateTime voucherDate, bool hasDuToan, string userName, string agencyIds)
        {
            return _nSMucLucNganSachRepository.FindBySummaryAgencySettlement(yearOfWork, yearOfBudget, budgetSource, quarterMonth, voucherDate, hasDuToan, userName, agencyIds);
        }

        public IEnumerable<NsMucLucNganSach> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string loaiQuyetToan, string userName)
        {
            return _nSMucLucNganSachRepository.FindByQtTongHopQuy(yearOfWork, yearOfBudget, budgetSource, agencyId, quarterMonth, loaiQuyetToan, userName);
        }

        public IEnumerable<LNSQuery> FindBySummaryYearSettlement(string yearOfBudget, int yearOfWork, int budgetSource, int dataType, string type)
        {
            return _nSMucLucNganSachRepository.FindBySummaryYearSettlement(yearOfBudget, yearOfWork, budgetSource, dataType, type);
        }

        public DataTable FindLNSByYear(int yearOfWork)
        {
            return _nSMucLucNganSachRepository.FindLNSByYear(yearOfWork);
        }

        public IEnumerable<MucLucNganSachCheckDataQuery> FindMlnsEstimateSettlementByYearOfBudget(int yearOfBuget)
        {
            return _nSMucLucNganSachRepository.FindMlnsEstimateSettlementByYearOfBudget(yearOfBuget);
        }
    }
}
