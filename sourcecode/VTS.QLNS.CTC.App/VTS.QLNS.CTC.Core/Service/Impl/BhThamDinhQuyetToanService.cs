using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhThamDinhQuyetToanService : IService<BhThamDinhQuyetToan>, IBhThamDinhQuyetToanService
    {
        private readonly IBhThamDinhQuyetToanRepository _bhThamDinhQuyetToanRepository;

        public BhThamDinhQuyetToanService(IBhThamDinhQuyetToanRepository bhThamDinhQuyetToanRepository)
        {
            _bhThamDinhQuyetToanRepository = bhThamDinhQuyetToanRepository;
        }

        public IEnumerable<BhThamDinhQuyetToan> FindAll()
        {
            return _bhThamDinhQuyetToanRepository.FindAll();
        }

        public IEnumerable<BhThamDinhQuyetToanQuery> FindAll(int yearOfWork)
        {
            return _bhThamDinhQuyetToanRepository.FindAll(yearOfWork);
        }

        public int Add(BhThamDinhQuyetToan t)
        {
            return _bhThamDinhQuyetToanRepository.Add(t);
        }

        public int Update(BhThamDinhQuyetToan t)
        {
            return _bhThamDinhQuyetToanRepository.Update(t);
        }

        public int Delete(BhThamDinhQuyetToan t)
        {
            return _bhThamDinhQuyetToanRepository.Delete(t);
        }
        public BhThamDinhQuyetToan Find(Guid t)
        {
            return _bhThamDinhQuyetToanRepository.Find(t);
        }

        public IEnumerable<BhThamDinhQuyetToan> FindAll(Expression<Func<BhThamDinhQuyetToan, bool>> predicate)
        {
            return _bhThamDinhQuyetToanRepository.FindAll(predicate);
        }

        public void UpdateTotalChungTu(string voucherId, string userModify)
        {
            _bhThamDinhQuyetToanRepository.UpdateTotalChungTu(voucherId, userModify);
        }

        public string GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach)
        {
            return _bhThamDinhQuyetToanRepository.GetSoChungTuIndexByCondition(namLamViec, nguonNganSach, namNganSach);
        }
        public IEnumerable<BhThamDinhQuyetToan> FindUnitVoucher(int yearOfWork)
        {
            return _bhThamDinhQuyetToanRepository.FindUnitVoucher(yearOfWork);
        }

        public IEnumerable<BhThamDinhQuyetToan> FindUnitAggregateVoucher(int yearOfWork)
        {
            return _bhThamDinhQuyetToanRepository.FindUnitAggregateVoucher(yearOfWork);
        }
    }
}
