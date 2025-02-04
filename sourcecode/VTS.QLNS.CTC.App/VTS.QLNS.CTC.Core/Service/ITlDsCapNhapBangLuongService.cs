using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsCapNhapBangLuongService
    {
        int Add(TlDsCapNhapBangLuong entity);
        int Delete(Guid id);
        TlDsCapNhapBangLuong Find(Guid id);
        IEnumerable<TlDsCapNhapBangLuong> FindByMaCachTinhLuong(string maCachTinhLuong);
        IEnumerable<TlDsCapNhapBangLuong> FindByMonth(int month);
        IEnumerable<TlDsCapNhapBangLuong> FindAll();
        TlDsCapNhapBangLuong FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
        IEnumerable<TlDsCapNhapBangLuong> FindByCondition(Expression<Func<TlDsCapNhapBangLuong, bool>> predicate);
        int Update(TlDsCapNhapBangLuong entity);
        int UpdateRange(List<TlDsCapNhapBangLuong> entites);
        TlDsCapNhapBangLuong FindByMaCanBo(string MaCanBo);
        TlDsCapNhapBangLuong FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status);
        int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTln);
        int DeleteBangLuongTruyThu(int thang, int nam, string maDonVi, string maCachTln);
        void SaveBangLuong(List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThang> tlBangLuongThangs);
        IEnumerable<TlDsCapNhapBangLuong> FindBangLuongThangByNam(int nam);
        int DeleteCapNhatBangLuong(string idBangLuong);
        void CapNhatBangLuong(string idXoa, List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThang> tlBangLuongThangs);
        void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);
        void LockOrUnlock(Guid id, bool? isLock);
        void CreateSummaryVoucher(Guid idParent, string lstidChungTus, string idMaDonVi,string donViTongHop, decimal NamLamViec, decimal Thang);
    }
}
