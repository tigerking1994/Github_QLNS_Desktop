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
    public interface IBhQtcNamKinhPhiKhacChiTietRepostiory : IRepository<BhQtcNamKinhPhiKhacChiTiet>
    {
        void AddAggregate(QtcNamKinhPhiKhacCriteria criteria);
        bool ExitChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition);
        BhQtcNamKinhPhiKhacChiTiet FindById(Guid id);
        IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByIdChiTiet(Guid id);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiKhacCriteria searchCondition);
        List<ReportBHQTCNKPKhacPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, string sLNS);
        List<BhQtcNamKinhPhiKhacChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> GetExcelData(QtcNamKinhPhiKhacCriteria searchCondition);
        List<BhQtcNamKinhPhiKhacChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, Guid iID_LoaiChi, string iID_MaDonVi, string sDSLNS, DateTime? dNgayChungTu);
    }
}
