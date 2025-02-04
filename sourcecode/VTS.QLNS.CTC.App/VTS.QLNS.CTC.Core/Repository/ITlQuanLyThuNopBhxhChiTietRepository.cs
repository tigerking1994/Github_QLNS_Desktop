using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQuanLyThuNopBhxhChiTietRepository : IRepository<TlQuanLyThuNopBhxhChiTiet>
    {
        DataTable GetDataQlThuNopBhxhDetails(Guid id);
        void CreateSummaryDetails(Guid iIdParent, string lstidChungTus, string idMaDonVi, int iNamLamViec, int iThang);
        DataTable ReportThuNopBhxhTongHopTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong);
        DataTable GetDataReportThuNopBhxh(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, bool isCheckedMaHuongLuong, bool isInCanBoMoi);

    }
}
