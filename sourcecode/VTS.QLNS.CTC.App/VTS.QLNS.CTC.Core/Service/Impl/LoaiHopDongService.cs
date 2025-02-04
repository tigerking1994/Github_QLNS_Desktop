using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class LoaiHopDongService : ILoaiHopDongService
    {
        private readonly ILoaiHopDongRepository _loaiHopDongRepository;

        public LoaiHopDongService(ILoaiHopDongRepository loaiHopDongRepository)
        {
            _loaiHopDongRepository = loaiHopDongRepository;
        }

        public IEnumerable<VdtDmLoaiHopDong> FindAll()
        {
            return _loaiHopDongRepository.FindAll();
        }
    }
}
