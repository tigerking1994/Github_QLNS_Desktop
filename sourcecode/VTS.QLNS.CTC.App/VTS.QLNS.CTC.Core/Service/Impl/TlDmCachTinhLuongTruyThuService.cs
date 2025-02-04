using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongTruyThuService : ITlDmCachTinhLuongTruyThuService
    {
        private ITlDmCachTinhLuongTruyThuRepository _cachTinhLuongRepository;
        public TlDmCachTinhLuongTruyThuService(ITlDmCachTinhLuongTruyThuRepository cachTinhLuongRepository)
        {
            _cachTinhLuongRepository = cachTinhLuongRepository;
        }

        public int Add(TlDmCachTinhLuongTruyThu entity)
        {
            return _cachTinhLuongRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongRepository.Delete(id);
        }

        public TlDmCachTinhLuongTruyThu Find(Guid id)
        {
            return _cachTinhLuongRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongTruyThu> FindAll()
        {
            return _cachTinhLuongRepository.FindAll();
        }

        public TlDmCachTinhLuongTruyThu FindByMaCot(string maCot)
        {
            return _cachTinhLuongRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongTruyThu entity)
        {
            return _cachTinhLuongRepository.Update(entity);
        }
    }
}
