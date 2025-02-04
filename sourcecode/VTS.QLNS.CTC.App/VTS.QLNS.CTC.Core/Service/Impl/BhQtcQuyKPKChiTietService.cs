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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcQuyKPKChiTietService : IBhQtcQuyKPKChiTietService
    {
        private readonly IBhQtcQuyKPKChiTietRepository _repostiory;
        public BhQtcQuyKPKChiTietService(IBhQtcQuyKPKChiTietRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(QtcQuyKCBCriteria criteria)
        {
            _repostiory.AddAggregate(criteria);
        }

        public int AddRange(List<BhQtcQuyKPKChiTiet> addItems)
        {
            return _repostiory.AddRange(addItems);
        }

        public void CreateVoudcherForQuaterBefore(BhQtcQuyKPK entity)
        {
            _repostiory.CreateVoudcherForQuaterBefore(entity);
        }

        public bool ExitChungTuChiTiet(QtcQuyKCBCriteria searchCondition)
        {
            return _repostiory.ExitChungTuChiTiet(searchCondition);
        }

        public IEnumerable<BhQtcQuyKPKChiTiet> FindAllChungTu()
        {
            return _repostiory.FindAll();
        }

        public IEnumerable<BhQtcQuyKPKChiTiet> FindByCondition(Expression<Func<BhQtcQuyKPKChiTiet, bool>> predicateSummaryDetail)
        {
            return _repostiory.FindAll(predicateSummaryDetail);
        }

        public BhQtcQuyKPKChiTiet FindById(Guid id)
        {
            return _repostiory.FindById(id);
        }

        public IEnumerable<BhQtcQuyKPKChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public List<BhQtcQuyKPKChiTietQuery> FindChungTuChiTiet(QtcQuyKCBCriteria searchCondition)
        {
            return _repostiory.FindChungTuChiTiet(searchCondition);
        }

        public List<BhQtcQuyKPKChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKCBCriteria searchCondition)
        {
            return _repostiory.FindSoTienQuyetToanDaDuocDuyetTheoQuy(searchCondition);
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportKeHoach(QtcQuyKCriteria searchCondition)
        {
            return _repostiory.GetDataReportKeHoach(searchCondition);
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri1(int yearOfWork, string valueItem, string lstDonViChecked, string principal, int iLoaiChungTu,Guid iDLoaichi, int donViTinh)
        {
            return _repostiory.GetDataReportQtcQuyKPKThongTri1(yearOfWork, valueItem, lstDonViChecked, principal, iLoaiChungTu, iDLoaichi, donViTinh);
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, Guid IdLoaiChi, int iLoaiChungTu, int donViTinh)
        {
            return _repostiory.GetDataReportQtcQuyKPKThongTri2(yearOfWork, quy, donVi, sLNS, principal, IdLoaiChi, iLoaiChungTu, donViTinh);
        }

        public List<BhQtcQuyKPKChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKCBCriteria searchCondition)
        {
            return _repostiory.GetDataTienDuToanPhanBoChi(searchCondition);
        }

        public int RemoveRange(IEnumerable<BhQtcQuyKPKChiTiet> bhQtcKinhphiKhacChiTiets)
        {
            return _repostiory.RemoveRange(bhQtcKinhphiKhacChiTiets);
        }

        public int Update(BhQtcQuyKPKChiTiet chungTuChiTiet)
        {
            return _repostiory.Update(chungTuChiTiet);
        }
    }
}
