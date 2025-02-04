using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CpChungTuService : ICpChungTuService
    {
        private readonly ICpChungTuRepository _cpChungTuRepository;

        public CpChungTuService(ICpChungTuRepository chungTuRepository)
        {
            _cpChungTuRepository = chungTuRepository;
        }

        public NsCpChungTu Add(NsCpChungTu entity)
        {
            _cpChungTuRepository.Add(entity);
            return entity;
        }

        public IEnumerable<CpChungTuQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, string userName,  bool isCapPhatToanDonVi, int iLoai)
        {
            return _cpChungTuRepository.FindByCondition(namLamViec, namNganSach, nguonNganSach, userName, isCapPhatToanDonVi, iLoai);
        }

        public IEnumerable<NsCpChungTu> FindByCondition(Expression<Func<NsCpChungTu, bool>> predicate)
        {
            return _cpChungTuRepository.FindAll(predicate);
        }

        public int Delete(Guid id)
        {
            NsCpChungTu entity = _cpChungTuRepository.Find(id);
            return _cpChungTuRepository.Delete(entity);
        }

        public NsCpChungTu FindById(Guid id)
        {
            return _cpChungTuRepository.Find(id);
        }

        public int Update(NsCpChungTu item)
        {
            return _cpChungTuRepository.Update(item);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach)
        {
            return _cpChungTuRepository.GetSoChungTuIndexByCondition(namLamViec, nguonNganSach, namNganSach);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _cpChungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _cpChungTuRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }

        public IEnumerable<ReportCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int namLamViec, Guid capPhatId, string idDonvi, string phanCap, string lns, string userName, int dvt)
        {
            return _cpChungTuRepository.GetDataReportCapPhatThongTri(namLamViec, capPhatId, idDonvi, phanCap, lns, userName, dvt);
        }

        public IEnumerable<ReportCapPhatThongTriDonViQuery> GetDataReportCapPhatThongTriDonVi(int namLamViec, Guid capPhatId, string idDonvi, int dvt, int loaiNganSach)
        {
            return _cpChungTuRepository.GetDataReportCapPhatThongTriDonVi(namLamViec, capPhatId, idDonvi, dvt, loaiNganSach);
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            _cpChungTuRepository.UpdateAggregateStatus(voucherIds);
        }

        public IEnumerable<NsCpChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            return _cpChungTuRepository.FindByAggregateVoucher(voucherNoes, yearOfWork, yearOfBudget, budgetSource);
        }

        public IEnumerable<T> GetDataReportLoaiCap<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, int dvt, string loaiBaoCao) where T : class
        {
            return _cpChungTuRepository.GetDataReportLoaiCap<T>(yearOfWork, yearOfBudget, budget, donViIds, capPhatIds, dvt, loaiBaoCao);
        }

        public IEnumerable<T> GetDataReportSoSanh<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, DateTime ngayChungTu, string phanCap, string lns, string userName, int dvt, string loaiBaoCao) where T : class
        {
            return _cpChungTuRepository.GetDataReportSoSanh<T>(yearOfWork, yearOfBudget, budget, donViIds, capPhatIds, ngayChungTu, phanCap, lns, userName, dvt, loaiBaoCao);
        }

        public IEnumerable<ReportCapPhatDonViQuery> GetDataReportCapPhatDonVi(int NamLamViec, int NamNganSach, int NguonNganSach, string IdDonvi, string ctID,  DateTime NgayChungTu, string UserName, int Dvt)
        {
            return _cpChungTuRepository.GetDataReportCapPhatDonVi(NamLamViec, NamNganSach, NguonNganSach, IdDonvi, ctID, NgayChungTu, UserName, Dvt);
        }
    }
}
