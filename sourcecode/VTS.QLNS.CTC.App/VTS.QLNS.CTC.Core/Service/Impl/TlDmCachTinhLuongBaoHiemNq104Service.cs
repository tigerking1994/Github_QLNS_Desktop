using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongBaoHiemNq104Service : ITlDmCachTinhLuongBaoHiemNq104Service
    {
        private ITlDmCachTinhLuongBaoHiemNq104Repository _cachTinhLuongBaoHiemRepository;
        public TlDmCachTinhLuongBaoHiemNq104Service(ITlDmCachTinhLuongBaoHiemNq104Repository cachTinhLuongBaoHiemRepository)
        {
            _cachTinhLuongBaoHiemRepository = cachTinhLuongBaoHiemRepository;
        }

        public int Add(TlDmCachTinhLuongBaoHiemNq104 entity)
        {
            return _cachTinhLuongBaoHiemRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongBaoHiemRepository.Delete(id);
        }

        public TlDmCachTinhLuongBaoHiemNq104 Find(Guid id)
        {
            return _cachTinhLuongBaoHiemRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongBaoHiemNq104> FindAll()
        {
            return _cachTinhLuongBaoHiemRepository.FindAll();
        }

        public TlDmCachTinhLuongBaoHiemNq104 FindByMaCot(string maCot)
        {
            return _cachTinhLuongBaoHiemRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongBaoHiemNq104 entity)
        {
            return _cachTinhLuongBaoHiemRepository.Update(entity);
        }
    }
}
