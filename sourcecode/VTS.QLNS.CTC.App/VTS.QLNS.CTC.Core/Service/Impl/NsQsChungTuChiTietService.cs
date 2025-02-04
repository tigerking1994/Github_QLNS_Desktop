using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQsChungTuChiTietService : INsQsChungTuChiTietService
    {
        private INsQsChungTuChiTietRepository _chungTuChiTietRepository;
        public NsQsChungTuChiTietService(INsQsChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuChiTietRepository = chungTuChiTietRepository;
        }

        public void AddRange(List<NsQsChungTuChiTiet> chungTuChiTiets)
        {
            _chungTuChiTietRepository.AddRange(chungTuChiTiets);
        }

        public void CreateDetail(Guid voucherId, int yearOfWork, int month, string userName)
        {
            _chungTuChiTietRepository.CreateDetail(voucherId, yearOfWork, month, userName);
        }

        public void Delete(Guid id)
        {
            NsQsChungTuChiTiet chungTuChiTiet = _chungTuChiTietRepository.Find(id);
            if (chungTuChiTiet != null)
                _chungTuChiTietRepository.Delete(chungTuChiTiet);
        }
        public void RemoveRange(List<NsQsChungTuChiTiet> entities)
        {
            _chungTuChiTietRepository.RemoveRange(entities);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _chungTuChiTietRepository.DeleteByVoucherId(voucherId);
        }

        public void DeleteInputData(Guid armyVoucherId, int month, string agencyId)
        {
            _chungTuChiTietRepository.DeleteInputData(armyVoucherId, month, agencyId);
        }

        public List<QsChungTuChiTietQuery> FindByCondition(ArmyVoucherDetailCriteria condition)
        {
            return _chungTuChiTietRepository.FindByCondition(condition).ToList();
        }

        public List<NsQsChungTuChiTiet> FindByCondition(Expression<Func<NsQsChungTuChiTiet, bool>> predicate)
        {
            return _chungTuChiTietRepository.FindAll(predicate).OrderByDescending(x => x.SKyHieu).ToList();
        }

        public NsQsChungTuChiTiet FindById(Guid id)
        {
            return _chungTuChiTietRepository.Find(id);
        }

        public List<ReportQuanSoDonViQuery> FindForAgencyDetailReport(int yearOfWork, string agencyId, string period)
        {
            return _chungTuChiTietRepository.FindForAgencyDetailReport(yearOfWork, agencyId, period).ToList();
        }

        public List<ReportQuanSoDonViQuery> FindForAgencyReport(int yearOfWork, string agencyId, string period)
        {
            return _chungTuChiTietRepository.FindForAgencyReport(yearOfWork, agencyId, period).ToList();
        }
        public List<string> FindForAgencyHasvalueReport(int yearOfWork, string agencyId, string period)
        {
            return _chungTuChiTietRepository.FindForAgencyHasvalueReport(yearOfWork, agencyId, period).ToList();
        }

        public List<ReportQuanSoTongHopQuery> FindForAverage(int yearOfWork, string agencyId, string period, ReportArmy reportType, int soThang)
        {
            return _chungTuChiTietRepository.FindForAverage(yearOfWork, agencyId, period, reportType, soThang).ToList();
        }

        public List<ReportQuanSoLienThamQuery> FindForJurisprudence(int month, int yearOfWork, string agencyId)
        {
            return _chungTuChiTietRepository.FindForJurisprudence(month, yearOfWork, agencyId).ToList();
        }

        public List<ReportQuanSoRaQuanQuery> FindForLeave(int yearOfWork, string agencyId, string period)
        {
            return _chungTuChiTietRepository.FindForLeave(yearOfWork, agencyId, period).ToList();
        }

        public ReportQuanSoRaQuanQuery FindForLeaveBefore(int month, int yearOfWork, string agencyId)
        {
            return _chungTuChiTietRepository.FindForLeaveBefore(month, yearOfWork, agencyId).FirstOrDefault();
        }

        public List<ReportQuanSoThuongXuyenQuery> FindForRegular(int month1, int month2, int month3, int month4, int yearOfWork, string agencyId)
        {
            return _chungTuChiTietRepository.FindForRegular(month1, month2, month3, month4, yearOfWork, agencyId).ToList();
        }

        public List<ReportQuanSoTongHopQuery> FindForSummaryReport(int yearOfWork, string agencyId, string period)
        {
            return _chungTuChiTietRepository.FindForSummaryReport(yearOfWork, agencyId, period).ToList();
        }

        public void Update(NsQsChungTuChiTiet chungTuChiTiet)
        {
            _chungTuChiTietRepository.Update(chungTuChiTiet);
        }

        public void UpdateRange(List<NsQsChungTuChiTiet> chungTuChiTiet)
        {
            _chungTuChiTietRepository.UpdateRange(chungTuChiTiet);
        }

        public void UpdateDetail(int yearOfWork, int month, string idMaDonVi)
        {
            _chungTuChiTietRepository.UpdateDetail(yearOfWork, month, idMaDonVi);
        }
        public IEnumerable<NsQsChungTuChiTiet> UpdateDetailYearBegin(int yearOfWork, string idMaDonVi)
        {
            return _chungTuChiTietRepository.UpdateDetailYearBegin(yearOfWork, idMaDonVi);
        }
    }
}
