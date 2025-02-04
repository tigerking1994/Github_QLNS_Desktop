using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsCapNhapBangLuongNq104Service
    {
        int Add(TlDsCapNhapBangLuongNq104 entity);
        int Delete(Guid id);
        TlDsCapNhapBangLuongNq104 Find(Guid id);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindByMaCachTinhLuong(string maCachTinhLuong);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindByMonth(int month);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindAll();
        TlDsCapNhapBangLuongNq104 FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindByCondition(Expression<Func<TlDsCapNhapBangLuongNq104, bool>> predicate);
        int Update(TlDsCapNhapBangLuongNq104 entity);
        int UpdateRange(List<TlDsCapNhapBangLuongNq104> entites);
        TlDsCapNhapBangLuongNq104 FindByMaCanBo(string MaCanBo);
        TlDsCapNhapBangLuongNq104 FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status);
        int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTln);
        void SaveBangLuong(List<TlDsCapNhapBangLuongNq104> tlDsCapNhapBangLuongs, List<TlBangLuongThangNq104> tlBangLuongThangs);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindBangLuongThangByNam(int nam);
        int DeleteCapNhatBangLuong(string idBangLuong);
        void CapNhatBangLuong(string idXoa, List<TlDsCapNhapBangLuongNq104> tlDsCapNhapBangLuongs, List<TlBangLuongThangNq104> tlBangLuongThangs);
        void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindHaveDataByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);

    }
}
