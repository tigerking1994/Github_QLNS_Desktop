using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcNamKinhPhiQuanLyChiTietService
    {
        void CreateChungTuChiTietTheoQuy(Guid id, string idMaDonVi, int? namChungTu, string sNguoiTao);
        IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByCondition(Expression<Func<BhQtcNamKinhPhiQuanLyChiTiet, bool>> predicate);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition);
        int AddRange(List<BhQtcNamKinhPhiQuanLyChiTiet> items);
        int Update(BhQtcNamKinhPhiQuanLyChiTiet item);
        int RemoveRange(IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> bhQtcKinhphiQuanlyChiTiets);
        BhQtcNamKinhPhiQuanLyChiTiet FindById(Guid id);
        IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id);
        void AddAggregate(QtcNamKinhPhiQuanLyCriteria criteria);
        bool ExitChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiQuanLyCriteria searchCondition);
        IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindAllChungTuDuToan();
        List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, int dvt);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> GetTienPhanBoChiTietDuToanChi(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportQTKPQL_KPKTSDK(int iNamLamViec, string listTenDonVi, int typeValue);
    }
}
