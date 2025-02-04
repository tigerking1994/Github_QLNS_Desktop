using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQuanLyThuNopBhxhService : ITlQuanLyThuNopBhxhService
    {
        private ITlQuanLyThuNopBhxhChiTietRepository _tlQuanLyThuNopBhxhChiTietRepository;
        private ITlQuanLyThuNopBhxhRepository _tlQuanLyThuNopBhxhRepository;
        public TlQuanLyThuNopBhxhService
        (
            ITlQuanLyThuNopBhxhChiTietRepository tlQuanLyThuNopBhxhChiTietRepository,
            ITlQuanLyThuNopBhxhRepository tlQuanLyThuNopBhxhRepository
         )
        {
            _tlQuanLyThuNopBhxhChiTietRepository = tlQuanLyThuNopBhxhChiTietRepository;
            _tlQuanLyThuNopBhxhRepository = tlQuanLyThuNopBhxhRepository;
        }

        public int Add(TlQuanLyThuNopBhxh entity)
        {
            return _tlQuanLyThuNopBhxhRepository.Add(entity);
        }

        public int Delete(TlQuanLyThuNopBhxh entity)
        {
            return _tlQuanLyThuNopBhxhRepository.Delete(entity);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _tlQuanLyThuNopBhxhRepository.Find(id);
            chungTu.BIsKhoa = isLock;
            _tlQuanLyThuNopBhxhRepository.Update(chungTu);
        }

        public int DeleteDetail(string siIdDetail)
        {
            return _tlQuanLyThuNopBhxhRepository.DeleteDetail(siIdDetail);
        }

        public void DeleteModelAndDetail(int thang, int nam, string maDonVi, string maCachTl, Guid? idTongHop = null, bool isTongHop = false)
        {
            _tlQuanLyThuNopBhxhRepository.DeleteModelAndDetail(thang, nam, maDonVi, maCachTl, idTongHop, isTongHop);
        }

        public IEnumerable<TlQuanLyThuNopBhxh> FindByMonth(int thang)
        {
            return _tlQuanLyThuNopBhxhRepository.FindByMonth(thang);
        }

        public IEnumerable<TlQuanLyThuNopBhxhQuery> FindByThangByNam(int nam)
        {
            return _tlQuanLyThuNopBhxhRepository.FindByThangByNam(nam);
        }

        public void SaveEntitiesAndDetails(List<TlQuanLyThuNopBhxh> entities, List<TlQuanLyThuNopBhxhChiTiet> details)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _tlQuanLyThuNopBhxhRepository.BulkInsert(entities);
                _tlQuanLyThuNopBhxhChiTietRepository.BulkInsert(details);

                transactionScope.Complete();
            }
        }

        public int Update(TlQuanLyThuNopBhxh entity)
        {
            return _tlQuanLyThuNopBhxhRepository.Update(entity);
        }

        public void UpdateDetailBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi)
        {
            _tlQuanLyThuNopBhxhRepository.UpdateDetailBhxhTheoCapBac(iThang, iNam, lstMaDonVi);
        }

        public void UpdateEntitiesAndDetails(string idXoa, List<TlQuanLyThuNopBhxh> entities, List<TlQuanLyThuNopBhxhChiTiet> details)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _tlQuanLyThuNopBhxhRepository.BulkInsert(entities);
                _tlQuanLyThuNopBhxhChiTietRepository.BulkInsert(details);
                transactionScope.Complete();
            }
        }

        public IEnumerable<TlQuanLyThuNopBhxh> FindByCondition(Expression<Func<TlQuanLyThuNopBhxh, bool>> predicate)
        {
            return _tlQuanLyThuNopBhxhRepository.FindAll(predicate);
        }
    }
}
