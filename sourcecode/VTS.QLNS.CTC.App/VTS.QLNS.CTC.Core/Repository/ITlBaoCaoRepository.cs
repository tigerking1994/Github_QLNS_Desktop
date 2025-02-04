using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBaoCaoRepository : IRepository<TlBaoCao>
    {       
        IEnumerable<ExportLuongNgachCanBoQuery> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongChiTietNgachCanBoQuery> FindLuongChiTietNgach(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<TlCanBoPhuCapInKiemQuery> FindCanBoPhuCapInKiem(string thangNam, string maDonVi);
        IEnumerable<BaoCaoPhanTichTienAnQuery> ReportBaoCaoTienAn(string MaCanBo, string MaPhuCap);
    }
}
