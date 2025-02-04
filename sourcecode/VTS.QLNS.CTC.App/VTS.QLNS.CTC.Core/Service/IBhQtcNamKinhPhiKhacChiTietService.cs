using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcNamKinhPhiKhacChiTietService
    {
        IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByCondition(Expression<Func<BhQtcNamKinhPhiKhacChiTiet, bool>> predicate);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition);
        int AddRange(List<BhQtcNamKinhPhiKhacChiTiet> items);
        int Update(BhQtcNamKinhPhiKhacChiTiet item);
        int RemoveRange(IEnumerable<BhQtcNamKinhPhiKhacChiTiet> bhQtcKinhphiQuanlyChiTiets);
        BhQtcNamKinhPhiKhacChiTiet FindById(Guid id);
        IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByIdChiTiet(Guid id);
        void AddAggregate(QtcNamKinhPhiKhacCriteria criteria);
        bool ExitChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiKhacCriteria searchCondition);
        IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindAllChungTu();
        List<ReportBHQTCNKPKhacPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, string sLNS);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> GetExcelData(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, Guid iID_LoaiChi, string iID_MaDonVi, string sDSLNS, DateTime? dNgayChungTu);
    }
}
