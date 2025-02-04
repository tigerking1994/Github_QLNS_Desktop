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
    public interface ITlBangLuongThangBHXHService
    {
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroXuatNgu(string maDonVi, int year, int month, int dvt);
        int AddRange(IEnumerable<TlBangLuongThangBHXH> entities);
        int DeleteByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXH> FindByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXH> FindByCondition(Expression<Func<TlBangLuongThangBHXH, bool>> predicate);
        IEnumerable<TlBangLuongThangBHXH> FindByMonthYear(int month, int year);
        DataTable GetDataLuongThangBHXH(Guid id);
        TlBangLuongThangBHXH GetLatestSalaryBHXH(string maCanBo, int thang, int nam);
        IEnumerable<TlBangLuongThangBHXHReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint, string maDonVi);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangLuongBHXH(int year, string months);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportDataQTCBHXH(int year, string months);
        int RemoveRange(IEnumerable<TlBangLuongThangBHXH> items);
        TlDmPhuCap GetCongChuan(string maCongChuan);
        IEnumerable<TlBangLuongThangBHXHQuery> GetBangLuongTheoPhanHo(int year, int month);
    }
}
