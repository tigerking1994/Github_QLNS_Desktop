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
    public interface IBhCpChungTuChiTietService
    {
        bool ExistCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        int RemoveRange(IEnumerable<BhCpChungTuChiTiet> cpChungTuChiTiets);
        int AddRange(IEnumerable<BhCpChungTuChiTiet> cpChungTuChiTiets);
        BhCpChungTuChiTiet FindById(Guid id);
        int Update(BhCpChungTuChiTiet cpChungTuChiTiets);
        void AddAggregate(BhCpChungTuChiChiTietCriteria criteria);
        IEnumerable<BhCpChungTuChiTiet> FindByCondition(Expression<Func<BhCpChungTuChiTiet, bool>> predicate);
        IEnumerable<BhCpChungTuChiTiet> FindByIdChiTiet(Guid id);
        void Delete(Guid id);
        IEnumerable<BhCpChungTuChiTiet> FindAllChungTuDuToan();
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuCheDoBHXHChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuCSSKHSSVandNLDChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        IEnumerable<DonVi> FindByDonViOfAllocationThongTri(int yearOfWork, int quy, Guid idLoaiChi);
        List<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTriNhieuLoaiChi(int yearOfWork, string iIDMaDonVi, string principal, int donViTinh, int iQuy, string lstsIDLoaiChi);
        IEnumerable<BhDanhMucLoaiChi> GetDanhMucLoaiChi(int yearOfWork, int Quy, string maDonVi);
    }
}
