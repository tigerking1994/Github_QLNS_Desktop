using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQuanLyThuNopBhxhRepository : IRepository<TlQuanLyThuNopBhxh>
    {
        IEnumerable<TlQuanLyThuNopBhxh> FindByMaCachTinhluong(string maCachTinhLuong);
        IEnumerable<TlQuanLyThuNopBhxh> FindByMonth(int month);
        TlQuanLyThuNopBhxh FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam);
        TlQuanLyThuNopBhxh FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status);
        IEnumerable<TlQuanLyThuNopBhxhQuery> FindByThangByNam(int nam);
        int DeleteModelAndDetail(int thang, int nam, string maDonVi, string maCachTl, Guid? idTongHop = null, bool isTongHop = false);
        int DeleteDetail(string siIdDetail);
        void UpdateDetailBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi);

    }
}
