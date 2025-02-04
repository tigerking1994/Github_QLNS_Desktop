using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongChuanService : ITlDmCachTinhLuongChuanService
    {
        private ITlDmCachTinhLuongChuanRepository _cachTinhLuongChuanRepository;
        public TlDmCachTinhLuongChuanService(ITlDmCachTinhLuongChuanRepository cachTinhLuongChuanRepository)
        {
            _cachTinhLuongChuanRepository = cachTinhLuongChuanRepository;
        }

        public int Add(TlDmCachTinhLuongChuan entity)
        {
            return _cachTinhLuongChuanRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongChuanRepository.Delete(id);
        }

        public TlDmCachTinhLuongChuan Find(Guid id)
        {
            return _cachTinhLuongChuanRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongChuan> FindAll()
        {
            return _cachTinhLuongChuanRepository.FindAll();
        }

        public TlDmCachTinhLuongChuan FindByMaCot(string maCot)
        {
            return _cachTinhLuongChuanRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongChuan entity)
        {
            return _cachTinhLuongChuanRepository.Update(entity);
        }
    }
}
