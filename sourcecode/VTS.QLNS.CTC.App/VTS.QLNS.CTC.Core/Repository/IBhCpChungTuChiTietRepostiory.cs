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
    public interface IBhCpChungTuChiTietRepostiory : IRepository<BhCpChungTuChiTiet>
    {
        void AddAggregate(BhCpChungTuChiChiTietCriteria criteria);
        bool ExistCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuCheDoBHXHChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        List<BhCpChungTuChiTietQuery> FindBhCpChungTuCSSKHSSVandNLDChiTiet(BhCpChungTuChiChiTietCriteria searchCondition);
        BhCpChungTuChiTiet FindById(Guid id);
        IEnumerable<BhCpChungTuChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<DonVi> FindByDonViOfAllocationThongTri(int yearOfWork, int quy, Guid idLoaiChi);
        List<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTriNhieuLoaiChi(int yearOfWork, string iIDMaDonVi, string principal, int donViTinh, int iQuy,string lstsIDLoaiChi);
        IEnumerable<BhDanhMucLoaiChi> GetDanhMucLoaiChi(int yearOfWork, int quy, string maDonVi);
    }
}
