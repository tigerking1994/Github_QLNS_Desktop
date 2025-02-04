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
    public interface IBhQtcQuyKPKChiTietRepository : IRepository<BhQtcQuyKPKChiTiet>
    {
        void AddAggregate(QtcQuyKCBCriteria criteria);
        void CreateVoudcherForQuaterBefore(BhQtcQuyKPK entity);
        bool ExitChungTuChiTiet(QtcQuyKCBCriteria searchCondition);
        BhQtcQuyKPKChiTiet FindById(Guid id);
        IEnumerable<BhQtcQuyKPKChiTiet> FindByIdChiTiet(Guid id);
        List<BhQtcQuyKPKChiTietQuery> FindChungTuChiTiet(QtcQuyKCBCriteria searchCondition);
        List<BhQtcQuyKPKChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKCBCriteria searchCondition);
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportKeHoach(QtcQuyKCriteria searchCondition);
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri1(int yearOfWork, string valueItem, string lstDonViChecked, string principal, int iLoaiChungTu,Guid iDLoaichi, int donViTinh);
        List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, Guid IdLoaiChi, int iLoaiTongHop, int donViTinh);
        List<BhQtcQuyKPKChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKCBCriteria searchCondition);
        void DeleteByIdChungTu(Guid idChungTu);


    }
}
