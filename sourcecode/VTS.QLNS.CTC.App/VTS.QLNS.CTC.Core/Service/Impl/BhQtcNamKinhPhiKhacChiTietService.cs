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

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcNamKinhPhiKhacChiTietService : IBhQtcNamKinhPhiKhacChiTietService
    {
        private readonly IBhQtcNamKinhPhiKhacChiTietRepostiory _repostiory;
        public BhQtcNamKinhPhiKhacChiTietService(IBhQtcNamKinhPhiKhacChiTietRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(QtcNamKinhPhiKhacCriteria criteria)
        {
            _repostiory.AddAggregate(criteria);
        }

        public int AddRange(List<BhQtcNamKinhPhiKhacChiTiet> items)
        {
            return _repostiory.AddRange(items);
        }

        public bool ExitChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            return _repostiory.ExitChungTuChiTiet(searchCondition);
        }

        public IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindAllChungTu()
        {
            return _repostiory.FindAll();
        }

        public IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByCondition(Expression<Func<BhQtcNamKinhPhiKhacChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhQtcNamKinhPhiKhacChiTiet FindById(Guid id)
        {
            return _repostiory.FindById(id);
        }

        public IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            return _repostiory.FindChungTuChiTiet(searchCondition);
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            return _repostiory.FindGetReportKeHoach(searchCondition);
        }

        public List<ReportBHQTCNKPKhacPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, string sLNS)
        {
            return _repostiory.FindGetReportPhuLuc(iNamLamViec, listTenDonVi, sLNS);
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            return _repostiory.FindTienThuChiForChungTu(searchCondition);
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> GetExcelData(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            return _repostiory.GetExcelData(searchCondition);
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, Guid iID_LoaiChi, string iID_MaDonVi, string sDSLNS, DateTime? dNgayChungTu)
        {
            return _repostiory.GetTienPhanBoChiTietDuToanChi(iNamLamViec, iID_LoaiChi, iID_MaDonVi, sDSLNS, dNgayChungTu);
        }

        public int RemoveRange(IEnumerable<BhQtcNamKinhPhiKhacChiTiet> bhQtcKinhphiQuanlyChiTiets)
        {
            return _repostiory.RemoveRange(bhQtcKinhphiQuanlyChiTiets);
        }

        public int Update(BhQtcNamKinhPhiKhacChiTiet item)
        {
            return _repostiory.Update(item);
        }
    }
}
