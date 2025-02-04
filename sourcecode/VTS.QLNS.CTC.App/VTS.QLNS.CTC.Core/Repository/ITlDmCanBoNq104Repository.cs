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
    public interface ITlDmCanBoNq104Repository : IRepository<TlDmCanBoNq104>
    {
        TlDmCanBoNq104 FindByMaCanbo(string maCanBo);
        IEnumerable<TlDmCanBoNq104> FindByMonthAndDonVi(int month, string parent);
        IEnumerable<TlDmCanBoNq104> FindByMonth(int month);
        IEnumerable<TlDmCanBoNq104> FindByConditionInsurance(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoNq104> FindByMaHieuCanBo(string maHieuCanBo);
        IEnumerable<TlDmCanBoNq104> FindLoadIndex();
        IEnumerable<TlDmCanBoNq104> FindAllCanBo();
        IEnumerable<TlDmCanBoNq104> FindCanBoXoa();
        IEnumerable<TlDmCanBoNq104> FindByMaCanboIn(List<string> MaCanBo);
        IEnumerable<TlDmCanBoNq104> FindUpdateMultiCanBo();
        IEnumerable<TlDmCanBoNq104> FindUpdateMultiCanBoNq104();
        IEnumerable<TlDmCanBoNq104Query> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq);
        IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false);
        IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false);
        DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM, int thangTruoc, int namTruoc);
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi);
        TlDmCanBoNq104 FindByMaHieuCanbo(string maHieuCanBo);
        IEnumerable<TlDmCanBoNq104> FindByMonthYear(int thang, int nam);
    }
}
