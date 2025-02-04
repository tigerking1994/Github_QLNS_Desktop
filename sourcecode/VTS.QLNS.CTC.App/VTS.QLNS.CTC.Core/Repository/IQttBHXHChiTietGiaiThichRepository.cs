using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQttBHXHChiTietGiaiThichRepository : IRepository<BhQttBHXHChiTietGiaiThich>
    {
        BhQttBHXHChiTietGiaiThich FindByCondition(BhQttBHXHChiTietGiaiThichCriteria condition);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetChiTietGiaiThichTruyThu(BhQttBHXHChiTietGiaiThichCriteria condition);
        IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanh(BhQttBHXHChiTietGiaiThichCriteria condition);
        IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanhTonTai(BhQttBHXHChiTietGiaiThichCriteria condition);
        IEnumerable<BhQttBHXHChiTietGiaiThich> FindByQttId(Guid iDQTT);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThu(int namLamViec, string maDonVi);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanh(int namLamViec, string maDonVi);
        BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoi(int namLamViec, int quy, int loaiQuy, string maDonVi, string loaiThu);
        BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoiTongHopDonVi(int namLamViec, int quy, int loaiQuy, string maDonVis, string maLoaiThu);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDong(int namLamViec, string maDonVi);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThuTongHopDonVi(int namLamViec, string maDonVis);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanhDonVi(int namLamViec, string maDonVis);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDongTongHopDonVi(int namLamViec, string maDonVis);
        IEnumerable<BhQttBHXHChiTietGiaiThich> FindByVouCherId(Guid voucherID);
        IEnumerable<BhQttBHXHChiTietGiaiThichBangLoiQuery> GetGiaiThichBangLoi(BhQttBHXHChiTietGiaiThichCriteria condition);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThangQuy(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThang(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe);
        bool HasMonthlyExplains(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThu(int namLamViec, string maDonVi, int quy, int loaiQuy);
        IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThuDonVi(int namLamViec, string maDonVi, int quy, int loaiQuy);
    }
}
