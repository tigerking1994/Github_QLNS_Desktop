using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ICpChungTuRepository : IRepository<NsCpChungTu>
    {
        IEnumerable<CpChungTuQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, string userName, bool isCapPhatToanDonVi, int iLoai);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        IEnumerable<ReportCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int namLamViec, Guid capPhatId, string idDonvi, string phanCap, string lns, string userName, int dvt);
        IEnumerable<ReportCapPhatThongTriDonViQuery> GetDataReportCapPhatThongTriDonVi(int namLamViec, Guid capPhatId, string idDonvi, int dvt, int loaiNganSach);
        public void UpdateAggregateStatus(string voucherIds);
        IEnumerable<NsCpChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<T> GetDataReportLoaiCap<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, int dvt, string loaiBaoCao) where T : class;
        IEnumerable<T> GetDataReportSoSanh<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, DateTime ngayChungTu, string phanCap, string lns, string userName, int dvt, string loaiBaoCao) where T : class;
        IEnumerable<ReportCapPhatDonViQuery> GetDataReportCapPhatDonVi(int NamLamViec, int NamNganSach, int NguonNganSach, string IdDonvi, string ctID, DateTime NgayChungTu, string UserName, int Dvt);
    }
}