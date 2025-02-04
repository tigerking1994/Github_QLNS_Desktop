using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongChuanNq104Service : ITlDmCachTinhLuongChuanNq104Service
    {
        private ITlDmCachTinhLuongChuanNq104Repository _cachTinhLuongChuanRepository;
        public TlDmCachTinhLuongChuanNq104Service(ITlDmCachTinhLuongChuanNq104Repository cachTinhLuongChuanRepository)
        {
            _cachTinhLuongChuanRepository = cachTinhLuongChuanRepository;
        }

        public int Add(TlDmCachTinhLuongChuanNq104 entity)
        {
            return _cachTinhLuongChuanRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongChuanRepository.Delete(id);
        }

        public TlDmCachTinhLuongChuanNq104 Find(Guid id)
        {
            return _cachTinhLuongChuanRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongChuanNq104> FindAll(Expression<Func<TlDmCachTinhLuongChuanNq104, bool>> predicate)
        {
            return _cachTinhLuongChuanRepository.FindAll(predicate);
        }

        public TlDmCachTinhLuongChuanNq104 FindByMaCot(string maCot)
        {
            return _cachTinhLuongChuanRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongChuanNq104 entity)
        {
            return _cachTinhLuongChuanRepository.Update(entity);
        }
    }
}
