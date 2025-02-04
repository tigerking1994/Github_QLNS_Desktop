using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class KhtmBHYTService : IKhtmBHYTService
    {
        private readonly IKhtmBHYTRepository _iKhtmBHYTRepository;
        public KhtmBHYTService(IKhtmBHYTRepository iKhtmBHYTRepository)
        {
            _iKhtmBHYTRepository = iKhtmBHYTRepository;
        }

        public int Add(BhKhtmBHYT entity)
        {
            return _iKhtmBHYTRepository.Add(entity);
        }

        public void AddAggregate(KhtmBHYTChiTietCriteria creation)
        {
            _iKhtmBHYTRepository.AddAggregate(creation);
        }

        public int Delete(BhKhtmBHYT item)
        {
            return _iKhtmBHYTRepository.Delete(item);
        }

        public BhKhtmBHYT FindAggregateVoucher(int yearOfWork)
        {
            return _iKhtmBHYTRepository.FindAggregateVoucher(yearOfWork);
        }

        public IEnumerable<BhKhtmBHYT> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            return _iKhtmBHYTRepository.FindByAggregateVoucher(voucherNoes, yearOfWork);
        }

        public IEnumerable<BhKhtmBHYTQuery> FindByCondition(int namLamViec)
        {
            return _iKhtmBHYTRepository.FindByCondition(namLamViec);
        }

        public IEnumerable<BhKhtmBHYT> FindByCondition(Expression<Func<BhKhtmBHYT, bool>> predicate)
        {
            return _iKhtmBHYTRepository.FindAll(predicate);
        }

        public IEnumerable<BhKhtmBHYT> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            return _iKhtmBHYTRepository.FindByCondition(namLamViec, maDonVi, loaiChungTu);
        }

        public BhKhtmBHYT FindById(Guid id)
        {
            return _iKhtmBHYTRepository.Find(id);
        }

        public IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietDonVi(int namLamViec, int loaiTongHop, string userName)
        {
            return _iKhtmBHYTRepository.FindChungTuChiTietDonVi(namLamViec, loaiTongHop, userName);
        }

        public IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, bool bDaTongHop, string userName)
        {
            return _iKhtmBHYTRepository.FindChungTuChiTietTongHopByCondition(namLamViec, loaiTongHop, bDaTongHop, userName);
        }

        public IEnumerable<BhKhtmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _iKhtmBHYTRepository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            return _iKhtmBHYTRepository.FindCurrentUnits(namLamViec);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _iKhtmBHYTRepository.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _iKhtmBHYTRepository.IsExistChungTuTongHop(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _iKhtmBHYTRepository.Find(id);
            chungTu.BKhoa = isLock;
            _iKhtmBHYTRepository.Update(chungTu);
        }

        public int Update(BhKhtmBHYT item)
        {
            return _iKhtmBHYTRepository.Update(item);
        }
    }
}
