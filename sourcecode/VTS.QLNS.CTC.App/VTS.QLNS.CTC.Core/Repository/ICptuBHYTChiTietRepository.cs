using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ICptuBHYTChiTietRepository : IRepository<BhCptuBHYTChiTiet>
    {
        IEnumerable<BhCptuBHYTChiTiet> FindByCondition(Expression<Func<BhCptuBHYTChiTiet, bool>> predicate);
        void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary);
        IEnumerable<BhCptuBHYTChiTietQuery> FinChungTuChiTiet(Guid idChungTu, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName);
        IEnumerable<BhCptuBHYTChiTietQuery> FindChungTuImport(int iQuy, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName);
        IEnumerable<BhCptuBHYTChiTietQuery> ExportKeHoachCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion);
        IEnumerable<BhCptuBHYTChiTietQuery> ExportTongHopCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion);
        IEnumerable<BhCptuBHYTChiTietQuery> ExportThongTriCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, string sLns, int iQuy, int iNamLamViec, string userName, int donViTinh, bool isRoundMillion);
        IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTChiTiet(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe);
        IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTTongHop(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe);
        IEnumerable<BhCptuBHYTChiTiet> FindChungTuChiTietByChungTuId(BhCpTUChungTuChiTietCriteria searchModel);
    }
}
