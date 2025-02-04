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
    public interface IBhQtcNamKinhPhiQuanLyChiTietRepostiory : IRepository<BhQtcNamKinhPhiQuanLyChiTiet>
    {
        void AddAggregate(QtcNamKinhPhiQuanLyCriteria criteria);
        void CreateChungTuChiTietTheoQuy(Guid id, string idMaDonVi, int? namChungTu, string sNguoiTao);
        bool ExitChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition);
        BhQtcNamKinhPhiQuanLyChiTiet FindById(Guid id);
        IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, int dvt);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> GetTienPhanBoChiTietDuToanChi(QtcNamKinhPhiQuanLyCriteria searchCondition);
        List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportQTKPQL_KPKTSDK(int iNamLamViec, string listTenDonVi, int typeValue);
    }
}
