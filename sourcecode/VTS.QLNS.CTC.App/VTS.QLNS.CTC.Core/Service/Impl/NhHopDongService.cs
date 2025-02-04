using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhHopDongService : INhHopDongService
    {
        private INhHopDongRepository _nhHopDongRepository;

        public NhHopDongService(INhHopDongRepository nhHopDongRepository)
        {
            _nhHopDongRepository = nhHopDongRepository;
        }

        public IEnumerable<NhHopDong> FindAll()
        {
            return _nhHopDongRepository.FindAll();
        }

        public IEnumerable<NhHopDongQuery> FindAllWithTiGia()
        {
            return _nhHopDongRepository.FindAllWithTiGia();
        }

        public void Add(NhHopDong nhHopDong)
        {
            _nhHopDongRepository.Add(nhHopDong);
        }

        public NhHopDong FindById(Guid idHopDong)
        {
          return _nhHopDongRepository.FindById(idHopDong);
        }

        public void Update(NhHopDong nhHopDong)
        {
            _nhHopDongRepository.Update(nhHopDong);
        }

        public NhHopDong FindBySoHopSong(string soHopDong)
        {
            return _nhHopDongRepository.FindBySoHopSong(soHopDong);
        }

    }
}
