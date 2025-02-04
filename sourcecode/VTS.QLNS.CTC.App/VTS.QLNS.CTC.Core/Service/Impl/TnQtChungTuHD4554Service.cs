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
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnQtChungTuHD4554Service: ITnQtChungTuHD4554Service
    {
        private ITnQtChungTuHD4554Repository _repository;
        public TnQtChungTuHD4554Service(ITnQtChungTuHD4554Repository repository)
        {
            _repository = repository;
        }

        public TnQtChungTuHD4554 Add(TnQtChungTuHD4554 entity)
        {
            _repository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            TnQtChungTuHD4554 entity = _repository.Find(id);
            return _repository.Delete(entity);
        }

        public TnQtChungTuHD4554 FindAggregateVoucher(string voucherNoes)
        {
            return _repository.FindAggregateVoucher(voucherNoes);
        }

        public IEnumerable<TnQtChungTuHD4554> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<TnQtChungTuHD4554> FindByCondition(Expression<Func<TnQtChungTuHD4554, bool>> predicate)
        {

            return _repository.FindAll(predicate);
        }

        public TnQtChungTuHD4554 FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TnQtChungTuHD4554> FindByIdDonVi(string idDonVi, int iThangQuyLoai)
        {
            return _repository.FindByIdDonVi(idDonVi, iThangQuyLoai);
        }

        public List<string> FindLNSExist(SettlementVoucherCriteria condition, Guid voucherId, List<string> listLNSSelected)
        {
            var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
            predicate = predicate.And(x => x.INamLamViec == condition.YearOfWork && x.INamNganSach == condition.YearOfBudget
                                    && x.INguonNganSach == condition.BudgetSource && x.IIdMaDonVi == condition.AgencyId
                                    && x.IThangQuy == condition.QuarterMonth && x.IThangQuyLoai == condition.QuarterMonthType);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);
            List<string> listLNSExist = new List<string>();
            List<TnQtChungTuHD4554> chungTus = _repository.FindAll(predicate).ToList();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SDSLNS.Split(','));
            });

            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnQtChungTuHD4554, bool>> predicate)
        {

            return _repository.FindNextSoChungTuIndex(predicate);
        }

        public IEnumerable<TnQtChungTuHD4554Query> GetChungTuHD4554(int iNamLamViec)
        {
            return _repository.GetChungTuHD4554(iNamLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _repository.LockOrUnLock(id, lockStatus);
        }

        public int Update(TnQtChungTuHD4554 item)
        {
            return _repository.Update(item);
        }
    }
}
