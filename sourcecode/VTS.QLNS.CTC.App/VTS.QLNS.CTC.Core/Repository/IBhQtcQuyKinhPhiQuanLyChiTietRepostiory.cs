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
    public interface IBhQtcQuyKinhPhiQuanLyChiTietRepostiory : IRepository<BhQtcQuyKinhPhiQuanLyChiTiet>
    {
        void AddAggregate(QtcQuyKinhPhiQuanLyCriteria criteria);
        void CreateVoudcherForQuaterBefore(BhQtcQuyKinhPhiQuanLy entity);
        bool ExitChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        BhQtcQuyKinhPhiQuanLyChiTiet FindById(Guid id);
        IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportKeHoach(QtcQuyKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri1(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh);
        List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, int iLoaiChungTu, int donViTinh);
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKinhPhiQuanLyCriteria searchCondition);
    }
}
