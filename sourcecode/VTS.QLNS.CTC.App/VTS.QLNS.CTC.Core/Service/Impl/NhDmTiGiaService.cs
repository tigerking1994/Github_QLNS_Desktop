using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmTiGiaService : IService<NhDmTiGia>, INhDmTiGiaService
    {
        private INhDmTiGiaRepository _nhDmTiGiaRepository;
        private INhDmLoaiTienTeRepository _nhDmLoaiTienTeRepository;
        private INhHopDongRepository _nhHopDongRepository;

        public NhDmTiGiaService(
            INhDmTiGiaRepository nhDmTiGiaRepository, 
            INhDmLoaiTienTeRepository nhDmLoaiTienTeRepository,
            INhHopDongRepository nhHopDongRepository)
        {
            _nhDmTiGiaRepository = nhDmTiGiaRepository;
            _nhDmLoaiTienTeRepository= nhDmLoaiTienTeRepository;
            _nhHopDongRepository = nhHopDongRepository;
        }

        public void Add(NhDmTiGia nhDmTiGia)
        {
            _nhDmTiGiaRepository.Add(nhDmTiGia);
        }

        public void Update(NhDmTiGia nhDmTiGia)
        {
            _nhDmTiGiaRepository.Update(nhDmTiGia);
        }

        public void Delete(Guid id)
        {
            _nhDmTiGiaRepository.Delete(id);
        }

        public IEnumerable<NhDmTiGia> FindAll()
        {
            return _nhDmTiGiaRepository.FindAll();
        }

        public NhDmTiGia FindById(Guid id)
        {
            return _nhDmTiGiaRepository.Find(id);
        }

        public void Remove(NhDmTiGia nhDmTiGia)
        {
            _nhDmTiGiaRepository.Delete(nhDmTiGia);
        }

        public IEnumerable<NhDmTiGia> FindByCondition(Expression<Func<NhDmTiGia, bool>> predicate)
        {
            return _nhDmTiGiaRepository.FindAll(predicate);
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmTiGia> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaTiGia.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => t.IsDeleted).ToList();
            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.All(item => { item.Id = Guid.NewGuid(); return true; });
                foreach (var item in lstInsert)
                {
                    item.Id = Guid.NewGuid();
                    item.DNgayTao = DateTime.Now;
                    item.SMaTienTeGoc = _nhDmLoaiTienTeRepository.Find(item.IIdTienTeGocId).SMaTienTe;
                    _nhDmTiGiaRepository.Add(item);
                }
                foreach (var item in lstInsert)
                {
                    if (_nhDmTiGiaRepository.FirstOrDefault(n => n.SMaTiGia.ToUpper().Equals(item.SMaTiGia.ToUpper())) == null)
                    {
                        _nhDmTiGiaRepository.Add(item);
                    }
                    else
                    {
                        throw new ArgumentException("Mã tỉ giá " + item.SMaTiGia + " bị lặp, vui lòng thử lại!");
                    }
                }
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                foreach (var item in lstUpdate)
                {
                    if (_nhDmTiGiaRepository.FirstOrDefault(n => n.SMaTiGia.ToUpper().Equals(item.SMaTiGia.ToUpper()) && !n.Id.Equals(item.Id)) == null)
                    {
                        _nhDmTiGiaRepository.Update(item);
                    }
                    else
                    {
                        throw new ArgumentException("Mã tỉ giá " + item.SMaTiGia + " bị lặp, vui lòng thử lại!");
                    }
                }
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                foreach (var item in lstDelete)
                {
                    if (_nhDmTiGiaRepository.FirstOrDefault(x => x.Id.Equals(item.Id)) != null) _nhDmTiGiaRepository.Delete(item);
                }
            }
        }

        public override IEnumerable<NhDmTiGia> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDmTiGiaRepository.FindAll();
        }

        public double CurrencyExchange(string sourceCurrency, string destCurrency, string rootCurrency, IEnumerable<NhDmTiGiaChiTiet> tiGiaThamChieu, double value)
        {
            // Nếu qui đổi cùng loại tiền tệ thì giá trị bằng giá trị qui đổi
            if (sourceCurrency.Equals(destCurrency)) return value;

            var sourceTiGia = tiGiaThamChieu.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(sourceCurrency));
            var destTiGia = tiGiaThamChieu.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(destCurrency));

            // Nếu tiền tệ đem qui đổi là tiền tệ gốc thì trả giá trị tương ứng với tỉ giá tham chiếu
            if (sourceCurrency.Equals(rootCurrency) && destTiGia != null) return (double)(value * destTiGia.FTiGia);

            if (sourceTiGia != null)
            {
                // Nếu tiền tệ qui đổi là tiền tệ gốc thì giá trị bằng giá trị qui đổi sang tiền tệ gốc
                double rootValue = value / (double)sourceTiGia.FTiGia;
                if (destCurrency.Equals(rootCurrency)) return rootValue;

                // Trường hợp còn lại thì qui đổi ngược lại với giá trị tiền tệ đích
                if (destTiGia != null) return (double)(rootValue * destTiGia.FTiGia);
            }
            // Trả về 0 nếu không có giá trị qui đổi tương ứng
            return 0;
        }
    }
}
