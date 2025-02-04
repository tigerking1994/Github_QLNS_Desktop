using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapService : ITlCanBoPhuCapService
    {
        private readonly ITlCanBoPhuCapRepository _tlCanBoPhuCapRepository;

        public TlCanBoPhuCapService(ITlCanBoPhuCapRepository tlCanBoPhuCapRepository)
        {
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
        }

        public int AddRange(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            return _tlCanBoPhuCapRepository.AddRange(tlCanBoPhuCaps);
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.DeleteByMaCanBo(maCanBo);
        }

        public IEnumerable<TlCanBoPhuCap> FindAll()
        {
            return _tlCanBoPhuCapRepository.FindAll();
        }

        public IEnumerable<TlCanBoPhuCap> FindByMaCanBo(string maCanBo)
        {
            var predicate = PredicateBuilder.True<TlCanBoPhuCap>();
            predicate = predicate.And(x => maCanBo.Equals(x.MaCbo));
            return _tlCanBoPhuCapRepository.FindAll(predicate);
        }

        public int UpdateRange(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            return _tlCanBoPhuCapRepository.UpdateRange(tlCanBoPhuCaps);
        }

        public int UpdateRang(List<TlCanBoPhuCap> entities)
        {
            return _tlCanBoPhuCapRepository.UpdateRange(entities);
        }

        public IEnumerable<TlCanBoPhuCap> FindAll(Expression<Func<TlCanBoPhuCap, bool>> predicate)
        {
            return _tlCanBoPhuCapRepository.FindAll(predicate);
        }

        public int Update(TlCanBoPhuCap tlCanBoPhuCap)
        {
            return _tlCanBoPhuCapRepository.Update(tlCanBoPhuCap);
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            _tlCanBoPhuCapRepository.BulkInsert(tlCanBoPhuCaps);
        }

        public void BulkUpdate(IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            _tlCanBoPhuCapRepository.BulkUpdate(tlCanBoPhuCaps);
        }

        public IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.FindCanBoPhuCap(maCanBo);
        }

        public int DeleteCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.DeleteCanBo(maCanBo);
        }

        public TlCanBoPhuCap FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap)
        {
            return _tlCanBoPhuCapRepository.FindByMaCanBoAndMaPhuCap(maCanBo, maPhuCap);
        }

        public IEnumerable<TLCanBoPhuCapQuery> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue)
        {
            return _tlCanBoPhuCapRepository.Copy(maCanBo, fromYear, fromMonth, toYear, toMonth, isCopyValue);
        }

        public IEnumerable<TLCanBoPhuCapQuery> FindCanBoPhuCapByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapRepository.FindCanBoPhuCapByMaCanBo(maCanBo);
        }
    }
}
