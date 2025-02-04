using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhCpChungTuChiTietService : IBhCpChungTuChiTietService
    {
        private readonly IBhCpChungTuChiTietRepostiory _repostiory;
        public BhCpChungTuChiTietService(IBhCpChungTuChiTietRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(BhCpChungTuChiChiTietCriteria criteria)
        {
            _repostiory.AddAggregate(criteria);
        }

        public int AddRange(IEnumerable<BhCpChungTuChiTiet> cpChungTuChiTiets)
        {
            return _repostiory.AddRange(cpChungTuChiTiets);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhCpChungTuChiTiet entity = _repostiory.Find(id);
                if (entity != null)
                {
                    // Xóa chi tiết
                    _repostiory.Delete(entity);
                }

                transactionScope.Complete();
            }
        }

        public bool ExistCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            return _repostiory.ExistCpChungTuChiTiet(searchCondition);
        }

        public IEnumerable<BhCpChungTuChiTiet> FindAllChungTuDuToan()
        {
            return _repostiory.FindAll();
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuCheDoBHXHChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            return _repostiory.FindBhCpChungTuCheDoBHXHChiTiet(searchCondition);
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            return _repostiory.FindBhCpChungTuChiTiet(searchCondition);
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuCSSKHSSVandNLDChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            return _repostiory.FindBhCpChungTuCSSKHSSVandNLDChiTiet(searchCondition);
        }

        public IEnumerable<BhCpChungTuChiTiet> FindByCondition(Expression<Func<BhCpChungTuChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhCpChungTuChiTiet FindById(Guid id)
        {
            return _repostiory.FindById(id);
        }

        public IEnumerable<BhCpChungTuChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public int RemoveRange(IEnumerable<BhCpChungTuChiTiet> cpChungTuChiTiets)
        {
            return _repostiory.RemoveRange(cpChungTuChiTiets);
        }

        public int Update(BhCpChungTuChiTiet cpChungTuChiTiets)
        {
            return _repostiory.Update(cpChungTuChiTiets);
        }
        public IEnumerable<DonVi> FindByDonViOfAllocationThongTri(int yearOfWork, int quy, Guid idLoaiChi)
        {
            return _repostiory.FindByDonViOfAllocationThongTri(yearOfWork, quy, idLoaiChi);
        }

        public List<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTriNhieuLoaiChi(int yearOfWork, string iIDMaDonVi, string principal, int donViTinh, int iQuy, string lstsIDLoaiChi)
        {
            return _repostiory.GetDataReportCapPhatThongTriNhieuLoaiChi(yearOfWork, iIDMaDonVi, principal, donViTinh, iQuy, lstsIDLoaiChi);
        }
        public IEnumerable<BhDanhMucLoaiChi> GetDanhMucLoaiChi(int yearOfWork, int Quy, string maDonVi)
        {
            return _repostiory.GetDanhMucLoaiChi(yearOfWork, Quy, maDonVi);
        }
    }
}
