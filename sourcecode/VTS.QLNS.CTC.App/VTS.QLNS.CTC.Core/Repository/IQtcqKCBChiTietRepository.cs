using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcqKCBChiTietRepository : IRepository<BhQtcqKCBChiTiet>
    {
        IEnumerable<BhQtcqKCBChiTiet> FindByCondition(Expression<Func<BhQtcqKCBChiTiet, bool>> predicate);
        void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary,string sMaDonVi);
        IEnumerable<BhQtcqKCBChiTietQuery> GetChiTietQuyetToanChiQuyKCB(Guid idChungTu,Guid idLoaiChi,string sLNS, string sMaLoaiChi,string sMaDonVi, DateTime dNgayChungTu,int iQuy, int iNamLamViec, int loai);
        IEnumerable<BhQtcqKCBChiTietQuery> BaoCaoKCBQuanYDonVi(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh);
        void CreateVoudcherForQuaterBefore(BhQtcqKCB entity);
        List<ReportBHQTCQKCBThongTriQuery> GetDataThongTriDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh);
        List<BhQtcqKCBChiTietQuery> GetDataTienDuToanPhanBoChi(int namChungTu, string sDSLNS, string idMaDonVi, DateTime dNgayChungTu, Guid idLoaiChi, int quyChungTu);
        bool ExitChungTuChiTiet(Guid id);
        List<ReportBhQtcQKCBTongHopChi> BaoCaoKCBQuanYDonViTongHopChi(int yearOfWork, int donViTinh, string lstIdDonVi, int iQuy);
    }
}
