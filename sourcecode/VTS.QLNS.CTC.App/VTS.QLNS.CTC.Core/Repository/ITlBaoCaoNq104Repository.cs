using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBaoCaoNq104Repository : IRepository<TlBaoCao>
    {       
        IEnumerable<ExportLuongNgachCanBoNq104Query> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongChiTietNgachCanBoNq104Query> FindLuongChiTietNgach(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<TlCanBoPhuCapInKiemNq104Query> FindCanBoPhuCapInKiem(string thangNam, string maDonVi);
        IEnumerable<BaoCaoPhanTichTienAnNq104Query> ReportBaoCaoTienAn(string MaCanBo, string MaPhuCap);
    }
}
