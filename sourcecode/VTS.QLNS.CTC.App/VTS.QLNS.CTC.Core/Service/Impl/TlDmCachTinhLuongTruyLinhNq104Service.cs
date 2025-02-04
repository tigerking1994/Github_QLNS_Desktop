using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongTruyLinhNq104Service : ITlDmCachTinhLuongTruyLinhNq104Service
    {
        private ITlDmCachTinhLuongTruyLinhNq104Repository _cachTinhLuongTruyLinhRepository;
        public TlDmCachTinhLuongTruyLinhNq104Service(ITlDmCachTinhLuongTruyLinhNq104Repository cachTinhLuongTruyLinhRepository)
        {
            _cachTinhLuongTruyLinhRepository = cachTinhLuongTruyLinhRepository;
        }

        public int Add(TlDmCachTinhLuongTruyLinhNq104 entity)
        {
            return _cachTinhLuongTruyLinhRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongTruyLinhRepository.Delete(id);
        }

        public TlDmCachTinhLuongTruyLinhNq104 Find(Guid id)
        {
            return _cachTinhLuongTruyLinhRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongTruyLinhNq104> FindAll(Expression<Func<TlDmCachTinhLuongTruyLinhNq104, bool>> predicate)
        {
            return _cachTinhLuongTruyLinhRepository.FindAll(predicate);
        }

        public TlDmCachTinhLuongTruyLinhNq104 FindByMaCot(string maCot)
        {
            return _cachTinhLuongTruyLinhRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongTruyLinhNq104 entity)
        {
            return _cachTinhLuongTruyLinhRepository.Update(entity);
        }
    }
}
