using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhCpChungTuService
    {
        void Add(BhCpChungTu entity);
        void Delete(Guid id);
        void Update(BhCpChungTu entity);
        IEnumerable<BhCpChungTuQuery> FindIndex(int iNamChungTu);
        BhCpChungTu FindById(Guid id);
        IEnumerable<BhCpChungTu> FindByCondition(Expression<Func<BhCpChungTu, bool>> predicate);

        void LockOrUnlock(Guid id, bool status);

        int GetSoChungTuIndexByCondition(int namLamViec);
        IEnumerable<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int yearOfWork, Guid idLoaiChi, string sMaDonVi, string sLNS, string principal, int donViTinh, int iQuy);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoach(string lsyMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap,string sMaLoaiChi);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoachCsskHssvNld(int yearOfWork, Guid idLoaiCap, string lstDonVi, string sLNS, string principal, int donViTinh, int iQuy, string sMaLoaiChi);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, Guid id);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonVi(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonViCsskHssvNld(int yearOfWork, Guid idLoaiCap, string lstMaDonVi, string sLNS, string principal, int donViTinh, int iQuy);
    }
}
