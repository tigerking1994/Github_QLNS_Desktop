using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDsCapNhapBangLuongNq104Repository : IRepository<TlDsCapNhapBangLuongNq104>
    {
        IEnumerable<TlDsCapNhapBangLuongNq104> FindByMaCachTinhluong(string maCachTinhLuong);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindByMonth(int month);
        TlDsCapNhapBangLuongNq104 FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
        TlDsCapNhapBangLuongNq104 FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status);
        int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindBangLuongThangByNam(int nam);
        int DeleteCapNhatBangLuong(string idBangLuong);
        void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);
        IEnumerable<TlDsCapNhapBangLuongNq104> FindHaveDataByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
    }
}
