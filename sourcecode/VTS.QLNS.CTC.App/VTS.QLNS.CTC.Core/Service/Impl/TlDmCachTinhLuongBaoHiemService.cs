using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCachTinhLuongBaoHiemService : ITlDmCachTinhLuongBaoHiemService
    {
        private ITlDmCachTinhLuongBaoHiemRepository _cachTinhLuongBaoHiemRepository;
        public TlDmCachTinhLuongBaoHiemService(ITlDmCachTinhLuongBaoHiemRepository cachTinhLuongBaoHiemRepository)
        {
            _cachTinhLuongBaoHiemRepository = cachTinhLuongBaoHiemRepository;
        }

        public int Add(TlDmCachTinhLuongBaoHiem entity)
        {
            return _cachTinhLuongBaoHiemRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _cachTinhLuongBaoHiemRepository.Delete(id);
        }

        public TlDmCachTinhLuongBaoHiem Find(Guid id)
        {
            return _cachTinhLuongBaoHiemRepository.Find(id);
        }

        public IEnumerable<TlDmCachTinhLuongBaoHiem> FindAll()
        {
            return _cachTinhLuongBaoHiemRepository.FindAll();
        }

        public TlDmCachTinhLuongBaoHiem FindByMaCot(string maCot)
        {
            return _cachTinhLuongBaoHiemRepository.FindByMaCot(maCot);
        }

        public int Update(TlDmCachTinhLuongBaoHiem entity)
        {
            return _cachTinhLuongBaoHiemRepository.Update(entity);
        }
    }
}
