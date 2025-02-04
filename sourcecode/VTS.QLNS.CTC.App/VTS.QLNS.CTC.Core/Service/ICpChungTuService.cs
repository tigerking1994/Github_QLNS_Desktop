using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICpChungTuService
    {
        IEnumerable<CpChungTuQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, string userName, bool isCapPhatToanDonVi, int iLoai);
        IEnumerable<NsCpChungTu> FindByCondition(Expression<Func<NsCpChungTu, bool>> predicate);
        NsCpChungTu Add(NsCpChungTu entity); 
        int Delete(Guid id); 
        NsCpChungTu FindById(Guid id);
        int LockOrUnLock(Guid id, bool lockStatus);
        int Update(NsCpChungTu entity);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        IEnumerable<ReportCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int namLamViec, Guid capPhatId, string idDonvi, string phanCap, string lns, string userName, int dvt);
        IEnumerable<ReportCapPhatThongTriDonViQuery> GetDataReportCapPhatThongTriDonVi(int namLamViec, Guid capPhatId, string idDonvi, int dvt, int loaiNganSach);
        void UpdateAggregateStatus(string voucherIds);
        IEnumerable<NsCpChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<T> GetDataReportLoaiCap<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, int dvt, string loaiBaoCao) where T : class;
        IEnumerable<T> GetDataReportSoSanh<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, DateTime ngayChungTu, string phanCap, string lns, string userName, int dvt, string loaiBaoCao) where T : class;
        IEnumerable<ReportCapPhatDonViQuery> GetDataReportCapPhatDonVi(int NamLamViec, int NamNganSach, int NguonNganSach, string IdDonvi, string ctID,  DateTime NgayChungTu, string UserName, int Dvt);
    }
}
