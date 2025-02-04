using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhKhChiTietHopDongService : INhKhChiTietHopDongService
    {
        private INhKhChiTietHopDongRepository _nhKhChiTietHopDongRepository;

        public NhKhChiTietHopDongService(INhKhChiTietHopDongRepository nhKhChiTietHopDongRepository)
        {
            _nhKhChiTietHopDongRepository = nhKhChiTietHopDongRepository;
        }
        public void AddRange(List<NhKhChiTietHopDong> listChiTietHopDong)
        {
            _nhKhChiTietHopDongRepository.AddRange(listChiTietHopDong);
        }

        public IEnumerable<NhKhChiTietHopDong> FindChiTietHopDongByKHCT(Guid idKHChiTiet)
        {
            return _nhKhChiTietHopDongRepository.FindChiTietHopDongByKHCT(idKHChiTiet);
        }

        public void Update(NhKhChiTietHopDong nhKhChiTietHopDong)
        {
            _nhKhChiTietHopDongRepository.Update(nhKhChiTietHopDong);
        }
        public void Delete(Guid id)
        {
            _nhKhChiTietHopDongRepository.Delete(id);
        }

        public NhKhChiTietHopDong FindById(Guid id)
        {
           return _nhKhChiTietHopDongRepository.Find(id);
        }

        public void DeleteByIdKhChiTiet(Guid idKhChiTiet)
        {
            List<NhKhChiTietHopDong> entitys = FindChiTietHopDongByKHCT(idKhChiTiet).ToList();
            _nhKhChiTietHopDongRepository.RemoveRange(entitys);
        }
    }
}
