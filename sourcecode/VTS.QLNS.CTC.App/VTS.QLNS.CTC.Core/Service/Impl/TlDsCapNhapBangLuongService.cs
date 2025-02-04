using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Transactions;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsCapNhapbangLuongService : ITlDsCapNhapBangLuongService
    {
        private ITlDsCapNhapBangLuongRepository _tlDsCapNhapBangLuongRepository;
        private ITlBangLuongThangRepository _tlBangLuongThangRepository;

        public TlDsCapNhapbangLuongService(ITlDsCapNhapBangLuongRepository tlDsCapNhapBangLuongRepository,
            ITlBangLuongThangRepository tlBangLuongThangRepository)
        {
            _tlDsCapNhapBangLuongRepository = tlDsCapNhapBangLuongRepository;
            _tlBangLuongThangRepository = tlBangLuongThangRepository;
        }

        public int Add(TlDsCapNhapBangLuong entity)
        {
            return _tlDsCapNhapBangLuongRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            TlDsCapNhapBangLuong entity = _tlDsCapNhapBangLuongRepository.Find(id);
            return _tlDsCapNhapBangLuongRepository.Delete(entity);
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindByMonth(int month)
        {
            return _tlDsCapNhapBangLuongRepository.FindByMonth(month);
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindAll()
        {
            return _tlDsCapNhapBangLuongRepository.FindAll();
        }

        public TlDsCapNhapBangLuong Find(Guid id)
        {
            return _tlDsCapNhapBangLuongRepository.Find(id);
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindByMaCachTinhLuong(string maCachTinhLuong)
        {
            return _tlDsCapNhapBangLuongRepository.FindByMaCachTinhluong(maCachTinhLuong);
        }

        public TlDsCapNhapBangLuong FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            return _tlDsCapNhapBangLuongRepository.FindByCondition(maCachTinhLuong, maDonVi, thang, nam);
        }


        public IEnumerable<TlDsCapNhapBangLuong> FindByCondition(Expression<Func<TlDsCapNhapBangLuong, bool>> predicate)
        {
            return _tlDsCapNhapBangLuongRepository.FindAll(predicate);
        }

        public int Update(TlDsCapNhapBangLuong entity)
        {
            return _tlDsCapNhapBangLuongRepository.Update(entity);
        }

        int ITlDsCapNhapBangLuongService.UpdateRange(List<TlDsCapNhapBangLuong> entites)
        {
            return _tlDsCapNhapBangLuongRepository.UpdateRange(entites);
        }

        public TlDsCapNhapBangLuong FindByMaCanBo(string MaCanBo)
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

        public TlDsCapNhapBangLuong FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status)
        {
            return _tlDsCapNhapBangLuongRepository.FindByConditionStatus(maCachTinhLuong, maDonVi, thang, nam, status);
        }

        public int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl)
        {
            return _tlDsCapNhapBangLuongRepository.DeleteBangLuong(thang, nam, maDonVi, maCachTl);
        }

        public int DeleteBangLuongTruyThu(int thang, int nam, string maDonVi, string maCachTl)
        {
            return _tlDsCapNhapBangLuongRepository.DeleteBangLuongTruyThu(thang, nam, maDonVi, maCachTl);
        }

        public void SaveBangLuong(List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThang> tlBangLuongThangs)
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

        public IEnumerable<TlDsCapNhapBangLuong> FindBangLuongThangByNam(int nam)
        {
            return _tlDsCapNhapBangLuongRepository.FindBangLuongThangByNam(nam);
        }

        public int DeleteCapNhatBangLuong(string idBangLuong)
        {
            return _tlDsCapNhapBangLuongRepository.DeleteCapNhatBangLuong(idBangLuong);
        }

        public void CapNhatBangLuong(string idXoa, List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThang> tlBangLuongThangs)
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

        public void LockOrUnlock(Guid id, bool? isLock)
        {
            var bangLuong = _tlDsCapNhapBangLuongRepository.Find(id);
            bangLuong.KhoaBangLuong = isLock;
            _tlDsCapNhapBangLuongRepository.Update(bangLuong);
        }

        public void CreateSummaryVoucher(Guid idParent, string lstidChungTus, string idMaDonVi, string donViTongHop, decimal NamLamViec, decimal Thang)
        {
            _tlDsCapNhapBangLuongRepository.CreateSummaryVoucher(idParent, lstidChungTus, idMaDonVi, donViTongHop, NamLamViec, Thang);
        }
    }
}
