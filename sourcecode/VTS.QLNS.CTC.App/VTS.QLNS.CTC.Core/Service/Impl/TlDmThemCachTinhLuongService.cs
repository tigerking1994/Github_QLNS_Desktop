using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmThemCachTinhLuongService : ITlDmThemCachTinhLuongService
    {
        private ITlDmThemCachTinhLuongRepository _themCachTinhLuongRepository;

        public TlDmThemCachTinhLuongService(ITlDmThemCachTinhLuongRepository themCachTinhLuongRepository)
        {
            _themCachTinhLuongRepository = themCachTinhLuongRepository;
        }

        public IEnumerable<TlDmThemCachTinhLuong> FindAll()
        {
            return _themCachTinhLuongRepository.FindAll();
        }

        public TlDmThemCachTinhLuong FindByMaCachTinhLuong(string maCachTinhLuong)
        {
            return _themCachTinhLuongRepository.FindByMaCachTinhLuong(maCachTinhLuong);
        }
    }
}
