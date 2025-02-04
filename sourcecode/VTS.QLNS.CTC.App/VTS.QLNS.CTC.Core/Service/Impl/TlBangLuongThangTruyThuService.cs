using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongThangTruyThuService : ITlBangLuongThangTruyThuService
    {
        private readonly ITlBangLuongThangTruyThuRepository _tlBangLuongThangTruyThuRepository;
        private readonly ITlDsCapNhapBangLuongRepository _tlDsCapNhapBangLuongRepository;


        public TlBangLuongThangTruyThuService(ITlBangLuongThangTruyThuRepository tlBangLuongThangRepository, ITlDsCapNhapBangLuongRepository tlDsCapNhapBangLuongRepository)
        {
            _tlBangLuongThangTruyThuRepository = tlBangLuongThangRepository;
            _tlDsCapNhapBangLuongRepository = tlDsCapNhapBangLuongRepository;
        }

        public int Add(IEnumerable<TlBangLuongThangTruyThu> entity)
        {
            return _tlBangLuongThangTruyThuRepository.AddRange(entity);
        }


        public int AddRange(IEnumerable<TlBangLuongThangTruyThu> entities)
        {
            return _tlBangLuongThangTruyThuRepository.AddRange(entities);
        }

        public int AddOrUpdateRange(IEnumerable<TlBangLuongThangTruyThu> entities)
        {
            return _tlBangLuongThangTruyThuRepository.AddOrUpdateRange(entities);
        }

        public int UpdateRange(IEnumerable<TlBangLuongThangTruyThu> entities)
        {
            return _tlBangLuongThangTruyThuRepository.UpdateRange(entities);
        }
        public int Delete(TlBangLuongThangTruyThu entity)
        {
            return _tlBangLuongThangTruyThuRepository.Delete(entity);
        }

        public int Update(TlBangLuongThangTruyThu entity)
        {
            return _tlBangLuongThangTruyThuRepository.Update(entity);
        }

        public int DeleteByParentId(Guid parentId)
        {
            return _tlBangLuongThangTruyThuRepository.DeleteByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangTruyThu> FindByParentId(Guid parentId)
        {
            return _tlBangLuongThangTruyThuRepository.FindByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangTruyThu> FindByCondition(Expression<Func<TlBangLuongThangTruyThu, bool>> predicate)
        {
            return _tlBangLuongThangTruyThuRepository.FindAll(predicate);
        }

        public IEnumerable<TlBangLuongThangTruyThu> FindAll()
        {
            return _tlBangLuongThangTruyThuRepository.FindAll();
        }

        public DataTable GetDataLuongThangTruyThu(Guid id)
        {
            return _tlBangLuongThangTruyThuRepository.GetDataLuongThangTruyThu(id);
        }

        public void CapNhatBangLuong(List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThangTruyThu> tlBangLuongThangs)
        {
            using (var transactionScope = new TransactionScope(
                           TransactionScopeOption.Required,
                           new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                //_tlDsCapNhapBangLuongRepository.DeleteBangLuong(idXoa);
                _tlDsCapNhapBangLuongRepository.BulkInsert(tlDsCapNhapBangLuongs);
                _tlBangLuongThangTruyThuRepository.BulkInsert(tlBangLuongThangs);

                transactionScope.Complete();
            }
        }
    }
}
