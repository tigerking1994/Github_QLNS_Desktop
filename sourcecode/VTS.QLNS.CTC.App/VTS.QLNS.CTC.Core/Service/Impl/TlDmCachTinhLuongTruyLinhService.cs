using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongTruyLinhService : ITlDmCachTinhLuongTruyLinhService
    {
        private ITlDmCachTinhLuongTruyLinhRepository _cachTinhLuongTruyLinhRepository;
        public TlDmCachTinhLuongTruyLinhService(ITlDmCachTinhLuongTruyLinhRepository cachTinhLuongTruyLinhRepository)
        {
            _cachTinhLuongTruyLinhRepository = cachTinhLuongTruyLinhRepository;
        }

        public int Add(TlDmCachTinhLuongTruyLinh entity)
        {
            return _cachTinhLuongTruyLinhRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongTruyLinhRepository.Delete(id);
        }

        public TlDmCachTinhLuongTruyLinh Find(Guid id)
        {
            return _cachTinhLuongTruyLinhRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongTruyLinh> FindAll()
        {
            return _cachTinhLuongTruyLinhRepository.FindAll();
        }

        public TlDmCachTinhLuongTruyLinh FindByMaCot(string maCot)
        {
            return _cachTinhLuongTruyLinhRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongTruyLinh entity)
        {
            return _cachTinhLuongTruyLinhRepository.Update(entity);
        }
    }
}
