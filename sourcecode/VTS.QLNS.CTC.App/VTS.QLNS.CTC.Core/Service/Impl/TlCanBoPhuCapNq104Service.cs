using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapNq104Service : ITlCanBoPhuCapNq104Service
    {
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapRepository;
        private readonly ITlCanBoPhuCapBridgeNq104Repository _tlCanBoPhuCapBridgeRepository;

        public TlCanBoPhuCapNq104 FirstOrDefault(Expression<Func<TlCanBoPhuCapNq104, bool>> predicate)
        {
            return _tlCanBoPhuCapRepository.FirstOrDefault(predicate);
        }
        public TlCanBoPhuCapNq104Service(ITlCanBoPhuCapNq104Repository tlCanBoPhuCapRepository, ITlCanBoPhuCapBridgeNq104Repository tlCanBoPhuCapBridgeNq104Repository)
        {
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlCanBoPhuCapBridgeRepository = tlCanBoPhuCapBridgeNq104Repository;
        }

        public int Add(TlCanBoPhuCapNq104 tlCanBoPhuCap)
        {
            return _tlCanBoPhuCapRepository.Add(tlCanBoPhuCap);
        }

        public int AddRange(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            return _tlCanBoPhuCapRepository.AddRange(tlCanBoPhuCaps);
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.DeleteByMaCanBo(maCanBo);
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindAll()
        {
            return _tlCanBoPhuCapRepository.FindAll();
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindByMaCanBo(string maCanBo)
        {
            var predicate = PredicateBuilder.True<TlCanBoPhuCapNq104>();
            predicate = predicate.And(x => maCanBo.Equals(x.MaCbo));
            return _tlCanBoPhuCapRepository.FindAll(predicate);
        }

        public int UpdateRange(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            return _tlCanBoPhuCapRepository.UpdateRange(tlCanBoPhuCaps);
        }

        public int UpdateRang(List<TlCanBoPhuCapNq104> entities)
        {
            return _tlCanBoPhuCapRepository.UpdateRange(entities);
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindAll(Expression<Func<TlCanBoPhuCapNq104, bool>> predicate)
        {
            return _tlCanBoPhuCapRepository.FindAll(predicate);
        }

        public int Update(TlCanBoPhuCapNq104 tlCanBoPhuCap)
        {
            return _tlCanBoPhuCapRepository.Update(tlCanBoPhuCap);
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            _tlCanBoPhuCapRepository.BulkInsert(tlCanBoPhuCaps);
        }

        public void BulkUpdate(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            _tlCanBoPhuCapRepository.BulkUpdate(tlCanBoPhuCaps);
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.FindCanBoPhuCap(maCanBo);
        }

        public int DeleteCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.DeleteCanBo(maCanBo);
        }

        public TlCanBoPhuCapNq104 FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap)
        {
            return _tlCanBoPhuCapRepository.FindByMaCanBoAndMaPhuCap(maCanBo, maPhuCap);
        }

        public IEnumerable<TLCanBoPhuCapNq104Query> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue)
        {
            return _tlCanBoPhuCapRepository.Copy(maCanBo, fromYear, fromMonth, toYear, toMonth, isCopyValue);
        }

        public void UpdateMulti(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps, IEnumerable<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges)
        {
            using (var transactionScope = new TransactionScope(
                           TransactionScopeOption.Required,
                           new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _tlCanBoPhuCapRepository.BulkUpdate(tlCanBoPhuCaps);
                    _tlCanBoPhuCapBridgeRepository.BulkUpdate(tlCanBoPhuCapBridges);
                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }

            }
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindByMaCbo(string maCanbo)
        {
            return _tlCanBoPhuCapRepository.FindByMaCbo(maCanbo);
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindDataCanBoPhuCap(string lstCBo)
        {
            return _tlCanBoPhuCapRepository.FindDataCanBoPhuCap(lstCBo);
        }
    }
}
