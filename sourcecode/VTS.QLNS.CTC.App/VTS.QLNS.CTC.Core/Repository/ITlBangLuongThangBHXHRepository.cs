using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBangLuongThangBHXHRepository : IRepository<TlBangLuongThangBHXH>
    {
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint, string maDonVi);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapXuatNgu(string maDonVi, int year, int month, int dvt);
        int DeleteByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXH> FindByParentId(Guid parentId);
        IEnumerable<TlBangLuongThangBHXH> FindByMonthYear(int month, int year);
        DataTable GetDataLuongThangBHXH(Guid id);
        TlBangLuongThangBHXH GetLatestSalaryBHXH(string maCanBo, int thang, int nam);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportBangLuongBHXH(int year, string months);
        IEnumerable<TlBangLuongThangBHXHQuery> ExportDataQTCBHXH(int year, string months);
        TlDmPhuCap GetCongChuan(string maCongChuan);
        IEnumerable<TlBangLuongThangBHXHQuery> GetBangLuongTheoPhanHo(int year, int month);
    }
}
