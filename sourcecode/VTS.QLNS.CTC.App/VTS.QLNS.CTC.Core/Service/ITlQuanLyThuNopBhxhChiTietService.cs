using System.Data;
using System;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQuanLyThuNopBhxhChiTietService
    {
        DataTable GetDataQlThuNopBhxhDetails(Guid id);
        void CreateSummaryDetails(Guid iIdParent, string lstidChungTus, string idMaDonVi, int iNamLamViec, int iThang);
        int Add(TlQuanLyThuNopBhxhChiTiet entity);
        int Update(TlQuanLyThuNopBhxhChiTiet entity);
        int Delete(TlQuanLyThuNopBhxhChiTiet entity);
        IEnumerable<TlQuanLyThuNopBhxhChiTiet> FindByCondition(Expression<Func<TlQuanLyThuNopBhxhChiTiet, bool>> predicate);
        void BulkInsert(List<TlQuanLyThuNopBhxhChiTiet> dataDetails);
        DataTable ReportThuNopBhxh(TlDmDonVi donVi, DataTable data, int donViTinh, bool isOrderChucVu, Dictionary<string, string> dsNgach);
        DataTable ReportThuNopBhxhCalculate(DataTable data, int donViTinh, Dictionary<string, string> dsNgach);
        DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonVi> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, string sMaDonViRoot, Dictionary<string, string> dsNgach, bool isCheckedMaHuongLuong);
        DataTable ReportThuNopBhxhTheoDonViCalculate(List<TlDmDonVi> listDonVi, DataTable data, int donViTinh, Dictionary<string, string> dsNgach);
        DataTable GetDataReportThuNopBhxh(List<TlDmDonVi> listDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, string sMaDonViRoot, bool isCheckedMaHuongLuong, bool isInCanBoMoi);

        DataTable ReportThuNopBhxh(TlDmDonViNq104 donVi, DataTable data, int donViTinh, bool isOrderChucVu, Dictionary<string, string> dsNgach);
        DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonViNq104> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, string sMaDonViRoot, Dictionary<string, string> dsNgach, bool isCheckedMaHuongLuong);
        DataTable ReportThuNopBhxhTheoDonViCalculate(List<TlDmDonViNq104> listDonVi, DataTable data, int donViTinh, Dictionary<string, string> dsNgach);
        DataTable GetDataReportThuNopBhxh(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, string sMaDonViRoot, bool isCheckedMaHuongLuong, bool isInCanBoMoi);
    }
}
