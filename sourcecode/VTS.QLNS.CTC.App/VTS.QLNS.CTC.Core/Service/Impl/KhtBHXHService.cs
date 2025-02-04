using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class KhtBHXHService : IKhtBHXHService
    {
        private readonly IKhtBHXHRepository _iKhtBHXHRepository;
        public KhtBHXHService(IKhtBHXHRepository iKhtBHXHRepository)
        {
            _iKhtBHXHRepository = iKhtBHXHRepository;
        }

        public BhKhtBHXH FindById(Guid id)
        {
            return _iKhtBHXHRepository.Find(id);
        }
        public int Add(BhKhtBHXH entity)
        {
            return _iKhtBHXHRepository.Add(entity);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _iKhtBHXHRepository.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public IEnumerable<BhKhtBHXHQuery> FindByCondition(int namLamViec)
        {
            return _iKhtBHXHRepository.FindByCondition(namLamViec);
        }

        public int Update(BhKhtBHXH item)
        {
            return _iKhtBHXHRepository.Update(item);
        }

        public int Delete(BhKhtBHXH item)
        {
            return _iKhtBHXHRepository.Delete(item);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuKhtBHXH = _iKhtBHXHRepository.Find(id);
            chungTuKhtBHXH.BIsKhoa = isLock;
            _iKhtBHXHRepository.Update(chungTuKhtBHXH);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _iKhtBHXHRepository.IsExistChungTuTongHop(namLamViec);
        }

        public void AddAggregate(KhtBHXHChiTietCriteria creation)
        {
            _iKhtBHXHRepository.AddAggregate(creation);
        }

        public IEnumerable<BhKhtBHXH> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            return _iKhtBHXHRepository.FindByCondition(namLamViec, maDonVi, loaiChungTu);
        }

        public IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietByCondition(int namLamViec, bool bDaTongHop)
        {
            return _iKhtBHXHRepository.FindChungTuChiTietByCondition(namLamViec, bDaTongHop);
        }

        public IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, string userName)
        {
            return _iKhtBHXHRepository.FindChungTuChiTietTongHopByCondition(namLamViec, loaiTongHop, userName);
        }

        public IEnumerable<BhKhtBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _iKhtBHXHRepository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            return _iKhtBHXHRepository.FindCurrentUnits(namLamViec);
        }

        public IEnumerable<BhKhtBHXH> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            return _iKhtBHXHRepository.FindByAggregateVoucher(voucherNoes, yearOfWork);
        }

        public BhKhtBHXH FindAggregateVoucher(int yearOfWork)
        {
            return _iKhtBHXHRepository.FindAggregateVoucher(yearOfWork);
        }

        public IEnumerable<BhKhtBHXH> FindByVoucherType(int namLamViec)
        {
            return _iKhtBHXHRepository.FindByVoucherType(namLamViec);
        }

        public IEnumerable<BhKhtBHXH> FindByVoucherAggregateType(int namLamViec)
        {
            return _iKhtBHXHRepository.FindByVoucherAggregateType(namLamViec);
        }
    }
}
