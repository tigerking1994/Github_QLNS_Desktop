using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoCheDoBHXHChiTietService : ITlCanBoCheDoBHXHChiTietService
    {
        private ITlCanBoCheDoBHXHChiTietRepository _repository;
        public TlCanBoCheDoBHXHChiTietService(ITlCanBoCheDoBHXHChiTietRepository iTlCanBoCheDoBHXHChiTietRepository)
        {
            _repository = iTlCanBoCheDoBHXHChiTietRepository;
        }

        public int AddRangeCBCDChiTiet(IEnumerable<TlCanBoCheDoBHXHChiTiet> entities)
        {
            return _repository.AddRange(entities);
        }

        public int DeleteCBCDChiTiet(Guid id)
        {
            TlCanBoCheDoBHXHChiTiet entity = FindCBCDChiTiet(id);
            if (entity != null)
            {
                return _repository.Delete(entity);
            }
            return 0;
        }

        public int ExistSoNgayHuong(string sMaCanBo, string sMaCheDo, int? iThang, int? iNam)
        {
            return _repository.ExistSoNgayHuong(sMaCanBo, sMaCheDo, iThang, iNam);
        }

        public TlCanBoCheDoBHXHChiTiet FindCBCDChiTiet(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTiet(string maCanBo, string maCheDo, int thang, int nam)
        {
            return _repository.GetCanBoCheDoChiTiet(maCanBo, maCheDo, thang, nam);
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTietInactive(int thang, int nam)
        {
            return _repository.GetCanBoCheDoChiTietInactive(thang, nam);
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTietQuery> GetCanBoCheDoChiTietIndex(string maCanBo, string maCheDo, int thang, int nam)
        {
            return _repository.GetCanBoCheDoChiTietIndex(maCanBo, maCheDo, thang, nam);
        }

        public TlCanBoCheDoBHXHChiTietQuery GetTongSoNgayHuong(string maCanBo, string maCheDo, int thang, int nam)
        {
            return _repository.GetTongSoNgayHuong(maCanBo, maCheDo, thang, nam);
        }

        public int RemoveRange(IEnumerable<TlCanBoCheDoBHXHChiTiet> items)
        {
            return _repository.RemoveRange(items);
        }

        public int UpdateCBCDChiTiet(TlCanBoCheDoBHXHChiTiet entity)
        {
            return _repository.Update(entity);
        }
    }
}
