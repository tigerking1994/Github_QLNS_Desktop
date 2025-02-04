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
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcQuyKinhPhiQuanLyChiTietService : IBhQtcQuyKinhPhiQuanLyChiTietService
    {
        private readonly IBhQtcQuyKinhPhiQuanLyChiTietRepostiory _repostiory;
        public BhQtcQuyKinhPhiQuanLyChiTietService(IBhQtcQuyKinhPhiQuanLyChiTietRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(QtcQuyKinhPhiQuanLyCriteria criteria)
        {
            _repostiory.AddAggregate(criteria);
        }

        public int AddRange(List<BhQtcQuyKinhPhiQuanLyChiTiet> addItems)
        {
            return _repostiory.AddRange(addItems);
        }

        public void CreateVoudcherForQuaterBefore(BhQtcQuyKinhPhiQuanLy entity)
        {
            _repostiory.CreateVoudcherForQuaterBefore(entity);
        }

        public bool ExitChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.ExitChungTuChiTiet(searchCondition);
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindAllChungTuDuToan()
        {
            return _repostiory.FindAll();
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByCondition(Expression<Func<BhQtcQuyKinhPhiQuanLyChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhQtcQuyKinhPhiQuanLyChiTiet FindById(Guid id)
        {
            return _repostiory.FindById(id);
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.FindChungTuChiTiet(searchCondition);
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.FindSoTienQuyetToanDaDuocDuyetTheoQuy(searchCondition);
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportKeHoach(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.GetDataReportKeHoach(searchCondition);
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri1(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
        {
            return _repostiory.GetDataReportQtcQuyKPQlThongTri1(yearOfWork, quy, donVi, principal, iLoaiChungTu, donViTinh);
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, int iLoaiChungTu, int donViTinh)
        {
            return _repostiory.GetDataReportQtcQuyKPQlThongTri2(yearOfWork, quy, donVi, sLNS, principal, iLoaiChungTu, donViTinh);
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.GetDataTienDuToanPhanBoChi(searchCondition);
        }

        public int RemoveRange(IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> bhQtcKinhphiQuanlyChiTiets)
        {
            return _repostiory.RemoveRange(bhQtcKinhphiQuanlyChiTiets);
        }

        public int Update(BhQtcQuyKinhPhiQuanLyChiTiet chungTuChiTiet)
        {
            return _repostiory.Update(chungTuChiTiet);
        }
    }
}
