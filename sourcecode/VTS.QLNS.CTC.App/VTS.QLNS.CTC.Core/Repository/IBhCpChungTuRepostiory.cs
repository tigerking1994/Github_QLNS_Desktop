using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhCpChungTuRepostiory : IRepository<BhCpChungTu>
    {
        IEnumerable<BhCpChungTuQuery> FindIndex(int iNamChungTu);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoach(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap, string sMaLoaiChi);
        IEnumerable<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int yearOfWork, Guid idLoaiChi, string maDonVi, string sLNS, string sNguoiTao, int donViTinh, int iQuy);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoachCsskHssvNld(int yearOfWork, Guid iDLoaiCap, string lstDonVi, string sLNS, string sNguoiTao, int donViTinh, int IQuy, string sMaLoaiChi);
        int GetSoChungTuIndexByCondition(int namLamViec);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, Guid id);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonVi(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap);
        IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonViCsskHssvNld(int yearOfWork, Guid idLoaiCap, string lstMaDonVi, string sLNS, string principal, int donViTinh, int iQuy);
    }
}
