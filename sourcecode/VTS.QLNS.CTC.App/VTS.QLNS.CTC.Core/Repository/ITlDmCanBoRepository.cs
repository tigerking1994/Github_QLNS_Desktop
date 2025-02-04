using System;
using System.Collections.Generic;
using System.Data;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCanBoRepository : IRepository<TlDmCanBo>
    {
        TlDmCanBo FindByMaCanbo(string maCanBo);
        IEnumerable<TlDmCanBo> FindByMonthAndDonVi(int month, string parent);
        IEnumerable<TlDmCanBo> FindByMonth(int month);
        IEnumerable<TlDmCanBo> FindByConditionInsurance(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBo> FindByMaHieuCanBo(string maHieuCanBo);
        IEnumerable<TlDmCanBo> FindLoadIndex();
        IEnumerable<TlDmCanBo> FindAllCanBo();
        IEnumerable<TlDmCanBo> FindCanBoXoa();
        IEnumerable<TlDmCanBo> FindByMaCanboIn(List<string> MaCanBo);
        IEnumerable<TlDmCanBo> FindUpdateMultiCanBo();
        IEnumerable<TlDmCanBo> FindUpdateMultiCanBoNq104();
        IEnumerable<TlDmCanBoQuery> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq);
        IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false);
        IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false);
        DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM, int thangTruoc, int namTruoc);
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi);
        TlDmCanBo FindByMaHieuCanbo(string maHieuCanBo,string maCb);
    }
}
