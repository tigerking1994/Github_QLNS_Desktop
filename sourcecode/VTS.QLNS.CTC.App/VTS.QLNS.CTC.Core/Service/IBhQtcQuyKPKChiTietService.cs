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
    public interface IBhQtcQuyKPKChiTietService
    {
        int AddRange(List<BhQtcQuyKPKChiTiet> addItems);
        BhQtcQuyKPKChiTiet FindById(Guid id);
        int RemoveRange(IEnumerable<BhQtcQuyKPKChiTiet> bhQtcKinhphiQuanlyChiTiets);
        int Update(BhQtcQuyKPKChiTiet chungTuChiTiet);
        IEnumerable<BhQtcQuyKPKChiTiet> FindByIdChiTiet(Guid id);
        bool ExitChungTuChiTiet(QtcQuyKCBCriteria searchCondition);
        List<BhQtcQuyKPKChiTietQuery> FindChungTuChiTiet(QtcQuyKCBCriteria searchCondition);
        List<BhQtcQuyKPKChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKCBCriteria searchCondition);
        IEnumerable<BhQtcQuyKPKChiTiet> FindByCondition(Expression<Func<BhQtcQuyKPKChiTiet, bool>> predicateSummaryDetail);
        void AddAggregate(QtcQuyKCBCriteria criteria);
        IEnumerable<BhQtcQuyKPKChiTiet> FindAllChungTu();
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri1(int yearOfWork, string valueItem, string lstDonViChecked, string principal, int iLoaiChungTu,Guid iDLoaichi, int donViTinh);
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, Guid IdLoaiChi, int iLoaiChungTu, int donViTinh);
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportKeHoach(QtcQuyKCriteria searchCondition);
        void CreateVoudcherForQuaterBefore(BhQtcQuyKPK entity);
        List<BhQtcQuyKPKChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKCBCriteria searchCondition);
    }
}
