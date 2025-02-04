using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDttBHXHPhanBoChiTietRepository : IRepository<BhDtPhanBoChungTuChiTiet>
    {
        void DeleteByIdChungTu(Guid id);
        IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindByCondition(DuToanThuChungTuChiTietCriteria searchCondition);
        IEnumerable<BhDtPhanBoChungTuChiTiet> FindByIdChungTu(string idChungTu);
        bool IsExistEstimate(Guid id, Guid estimateId);
        void DeleteByIdChungTuDuToanNhan(Guid id, String idDuToanNhan);
        IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTiet(Guid chungTuPhanBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName);
        void DeleteByIds(IEnumerable<string> ids);
        IEnumerable<BhDtPhanBoChungTuChiTiet> FindByCondition(Expression<Func<BhDtPhanBoChungTuChiTiet, bool>> predicate);
        IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName);
        IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHXH(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi);
        IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi);
        IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopDuToanThuChi(int iNamLamViec, string sIdDonVis, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound);
        IEnumerable<ReportKhtDuToanBHXHBHYTBHTNQuery> ExportKhtDuToanBHXHBHYTBHTN(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound);
    }
}
