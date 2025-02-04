using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhCpChungTuService : IBhCpChungTuService
    {
        private readonly IBhCpChungTuRepostiory _repostiory;
        public BhCpChungTuService(IBhCpChungTuRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void Add(BhCpChungTu entity)
        {
            using (var transactionScope = new TransactionScope(
              TransactionScopeOption.Required,
              new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (entity != null)
                {
                    _repostiory.Add(entity);
                }

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhCpChungTu entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhCpChungTu> FindByCondition(Expression<Func<BhCpChungTu, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhCpChungTu FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhCpChungTuQuery> FindIndex(int iNamChungTu)
        {
            return _repostiory.FindIndex(iNamChungTu);
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoach(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap, string sMaLoaiChi)
        {
            return _repostiory.GetDataReportCapPhatKeHoach(lstMaDonVi, iQuy, yearOfWork, principal, donViTinh, idLoaiCap, sMaLoaiChi);
        }

        public IEnumerable<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int yearOfWork, Guid idLoaiChi, string maDonVi, string sLNS, string sNguoiTao, int donViTinh, int iQuy)
        {
            return _repostiory.GetDataReportCapPhatThongTri(yearOfWork, idLoaiChi, maDonVi, sLNS, sNguoiTao, donViTinh, iQuy);
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoachCsskHssvNld(int yearOfWork, Guid idLoaiCap, string lstDonVi, string sLNS, string sNguoiTao, int donViTinh, int iQuy, string sMaLoaiChi)
        {
            return _repostiory.GetDataReportCapPhatKeHoachCsskHssvNld(yearOfWork, idLoaiCap, lstDonVi, sLNS, sNguoiTao, donViTinh, iQuy, sMaLoaiChi);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhCpChungTu entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }

        public void Update(BhCpChungTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, Guid id)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork, iQuy, id);
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonVi(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap)
        {
            return _repostiory.GetDataReportCapPhatThongTriForDonVi(lstMaDonVi, iQuy, yearOfWork, principal, donViTinh, idLoaiCap);
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonViCsskHssvNld(int yearOfWork, Guid idLoaiCap, string lstMaDonVi, string sLNS, string principal, int donViTinh, int iQuy)
        {
            return _repostiory.GetDataReportCapPhatThongTriForDonViCsskHssvNld(yearOfWork, idLoaiCap, lstMaDonVi, sLNS, principal, donViTinh, iQuy);
        }
    }
}
