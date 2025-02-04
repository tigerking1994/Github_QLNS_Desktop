using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Transactions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsCapNhapbangLuongNq104Service : ITlDsCapNhapBangLuongNq104Service
    {
        private ITlDsCapNhapBangLuongNq104Repository _tlDsCapNhapBangLuongRepository;
        private ITlBangLuongThangNq104Repository _tlBangLuongThangRepository;

        public TlDsCapNhapbangLuongNq104Service(ITlDsCapNhapBangLuongNq104Repository tlDsCapNhapBangLuongRepository,
            ITlBangLuongThangNq104Repository tlBangLuongThangRepository)
        {
            _tlDsCapNhapBangLuongRepository = tlDsCapNhapBangLuongRepository;
            _tlBangLuongThangRepository = tlBangLuongThangRepository;
        }

        public int Add(TlDsCapNhapBangLuongNq104 entity)
        {
            return _tlDsCapNhapBangLuongRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            TlDsCapNhapBangLuongNq104 entity = _tlDsCapNhapBangLuongRepository.Find(id);
            return _tlDsCapNhapBangLuongRepository.Delete(entity);
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindByMonth(int month)
        {
            return _tlDsCapNhapBangLuongRepository.FindByMonth(month);
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindAll()
        {
            return _tlDsCapNhapBangLuongRepository.FindAll();
        }

        public TlDsCapNhapBangLuongNq104 Find(Guid id)
        {
            return _tlDsCapNhapBangLuongRepository.Find(id);
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindByMaCachTinhLuong(string maCachTinhLuong)
        {
            return _tlDsCapNhapBangLuongRepository.FindByMaCachTinhluong(maCachTinhLuong);
        }

        public TlDsCapNhapBangLuongNq104 FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            return _tlDsCapNhapBangLuongRepository.FindByCondition(maCachTinhLuong, maDonVi, thang, nam);
        }


        public IEnumerable<TlDsCapNhapBangLuongNq104> FindByCondition(Expression<Func<TlDsCapNhapBangLuongNq104, bool>> predicate)
        {
            return _tlDsCapNhapBangLuongRepository.FindAll(predicate);
        }

        public int Update(TlDsCapNhapBangLuongNq104 entity)
        {
            return _tlDsCapNhapBangLuongRepository.Update(entity);
        }

        int ITlDsCapNhapBangLuongNq104Service.UpdateRange(List<TlDsCapNhapBangLuongNq104> entites)
        {
            return _tlDsCapNhapBangLuongRepository.UpdateRange(entites);
        }

        public TlDsCapNhapBangLuongNq104 FindByMaCanBo(string MaCanBo)
        {
            var idBangLuong = _tlBangLuongThangRepository.FindMaCanBo(MaCanBo).Select(x => x.Parent).Distinct().ToList();
            if (idBangLuong == null || idBangLuong.Count() == 0)
            {
                return null;
            }
            foreach (var item in idBangLuong)
            {
                var bangLuong = _tlDsCapNhapBangLuongRepository.Find(item);
                if (bangLuong.Status == true)
                {
                    return bangLuong;
                }
            }
            return null;
        }

        public TlDsCapNhapBangLuongNq104 FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status)
        {
            return _tlDsCapNhapBangLuongRepository.FindByConditionStatus(maCachTinhLuong, maDonVi, thang, nam, status);
        }

        public int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl)
        {
            return _tlDsCapNhapBangLuongRepository.DeleteBangLuong(thang, nam, maDonVi, maCachTl);
        }

        public void SaveBangLuong(List<TlDsCapNhapBangLuongNq104> tlDsCapNhapBangLuongs, List<TlBangLuongThangNq104> tlBangLuongThangs)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _tlDsCapNhapBangLuongRepository.BulkInsert(tlDsCapNhapBangLuongs);
                _tlBangLuongThangRepository.BulkInsert(tlBangLuongThangs);

                transactionScope.Complete();
            }
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindBangLuongThangByNam(int nam)
        {
            return _tlDsCapNhapBangLuongRepository.FindBangLuongThangByNam(nam);
        }

        public int DeleteCapNhatBangLuong(string idBangLuong)
        {
            return _tlDsCapNhapBangLuongRepository.DeleteCapNhatBangLuong(idBangLuong);
        }

        public void CapNhatBangLuong(string idXoa, List<TlDsCapNhapBangLuongNq104> tlDsCapNhapBangLuongs, List<TlBangLuongThangNq104> tlBangLuongThangs)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                //_tlDsCapNhapBangLuongRepository.DeleteBangLuong(idXoa);
                _tlDsCapNhapBangLuongRepository.BulkInsert(tlDsCapNhapBangLuongs);
                _tlBangLuongThangRepository.BulkInsert(tlBangLuongThangs);

                transactionScope.Complete();
            }
        }

        public void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi)
        {
            _tlDsCapNhapBangLuongRepository.UpdateBangLuongBhxhTheoCapBac(iThang, iNam, lstMaDonVi);
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindHaveDataByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            return _tlDsCapNhapBangLuongRepository.FindHaveDataByCondition(maCachTinhLuong, maDonVi, thang, nam);
        }
    }
}
