using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDsCapNhapBangLuongRepository : IRepository<TlDsCapNhapBangLuong>
    {
        IEnumerable<TlDsCapNhapBangLuong> FindByMaCachTinhluong(string maCachTinhLuong);
        IEnumerable<TlDsCapNhapBangLuong> FindByMonth(int month);
        TlDsCapNhapBangLuong FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
        TlDsCapNhapBangLuong FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status);
        int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl);
        int DeleteBangLuongTruyThu(int thang, int nam, string maDonVi, string maCachTl);
        IEnumerable<TlDsCapNhapBangLuong> FindBangLuongThangByNam(int nam);
        int DeleteCapNhatBangLuong(string idBangLuong);
        void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);
        void CreateSummaryVoucher(Guid idParent, string lstidChungTus, string idMaDonVi, string donViTongHop, decimal NamLamViec, decimal Thang);
    }
}
