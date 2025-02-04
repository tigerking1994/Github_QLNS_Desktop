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
    public interface IBhQtcQuyKinhPhiQuanLyChiTietService
    {
        void AddAggregate(QtcQuyKinhPhiQuanLyCriteria criteria);
        int AddRange(List<BhQtcQuyKinhPhiQuanLyChiTiet> addItems);
        void CreateVoudcherForQuaterBefore(BhQtcQuyKinhPhiQuanLy entity);
        bool ExitChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindAllChungTuDuToan();
        IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByCondition(Expression<Func<BhQtcQuyKinhPhiQuanLyChiTiet, bool>> predicate);
        BhQtcQuyKinhPhiQuanLyChiTiet FindById(Guid id);
        IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportKeHoach(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri1(int yearOfWork, string valueItem1, string valueItem2, string principal, int iLoaiChungTu, int donViTinh);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri2(int yearOfWork, string valueItem1, string valueItem2, string sLNS, string principal, int iLoaiChungTu, int donViTinh);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        int RemoveRange(IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> bhQtcKinhphiQuanlyChiTiets);
        int Update(BhQtcQuyKinhPhiQuanLyChiTiet chungTuChiTiet);
    }
}
