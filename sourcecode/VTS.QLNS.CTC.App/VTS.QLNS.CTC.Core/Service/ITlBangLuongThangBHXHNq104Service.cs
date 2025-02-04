using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongThangBHXHNq104Service
    {
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroXuatNgu(string maDonVi, int year, int month, int dvt);
        int AddRange(IEnumerable<TlBangLuongThangBHXHNq104> entities);
        int DeleteByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXHNq104> FindByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXHNq104> FindByCondition(Expression<Func<TlBangLuongThangBHXHNq104, bool>> predicate);
        IEnumerable<TlBangLuongThangBHXHNq104> FindByMonthYear(int month, int year);
        DataTable GetDataLuongThangBHXH(Guid id);
        TlBangLuongThangBHXHNq104 GetLatestSalaryBHXH(string maCanBo, int thang, int nam);
        IEnumerable<TlBangLuongThangBHXHNq104ReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangLuongBHXH(int year, string months);
        IEnumerable<TlBangLuongThangBHXHNq104Query> ExportDataQTCBHXH(int year, string months);
        int RemoveRange(IEnumerable<TlBangLuongThangBHXHNq104> items);
        TlDmPhuCapNq104 GetCongChuan(string maCongChuan);
    }
}
