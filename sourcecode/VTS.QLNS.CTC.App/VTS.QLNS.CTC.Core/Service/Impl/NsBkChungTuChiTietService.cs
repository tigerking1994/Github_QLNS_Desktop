using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsBkChungTuChiTietService : INsBkChungTuChiTietService
    {
        private INsBkChungTuChiTietRepository _chungTuChiTietRepository;

        public NsBkChungTuChiTietService(INsBkChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuChiTietRepository = chungTuChiTietRepository;
        }

        public void AddRange(List<NsBkChungTuChiTiet> listChungTuChiTiet)
        {
            _chungTuChiTietRepository.AddRange(listChungTuChiTiet);
        }

        public void Delete(Guid id)
        {
            NsBkChungTuChiTiet chungTuChiTiet = _chungTuChiTietRepository.Find(id);
            if (chungTuChiTiet != null)
                _chungTuChiTietRepository.Delete(chungTuChiTiet);
        }

        public void DeleteByListVoucherId(List<Guid> listVoucherId)
        {
            _chungTuChiTietRepository.DeleteByListVoucherId(listVoucherId);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _chungTuChiTietRepository.DeleteByVoucherId(voucherId);
        }

        public NsBkChungTuChiTiet FindById(Guid id)
        {
            return _chungTuChiTietRepository.Find(id);
        }

        public List<ReportBangKeTongHopQuery> FindBySummaryVoucherList(ReportVoucherListCriteria condition)
        {
            return _chungTuChiTietRepository.FindBySummaryVoucherList(condition).ToList();
        }

        public List<NsBkChungTuChiTietQuery> FindByVoucherListId(Guid voucherListId, int yearOfWork)
        {
            return _chungTuChiTietRepository.FindByVoucherListId(voucherListId, yearOfWork).ToList();
        }

        public void Update(NsBkChungTuChiTiet chungTuChiTiet)
        {
            _chungTuChiTietRepository.Update(chungTuChiTiet);
        }

        public List<NsBkChungTuChiTiet> FindByCondition(Expression<Func<NsBkChungTuChiTiet, bool>> predicate)
        {
            return _chungTuChiTietRepository.FindAll(predicate).ToList();
        }
    }
}
