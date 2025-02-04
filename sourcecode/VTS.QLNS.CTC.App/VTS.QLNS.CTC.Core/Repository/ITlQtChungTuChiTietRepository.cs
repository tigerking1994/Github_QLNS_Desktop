using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietRepository : IRepository<TlQtChungTuChiTiet>
    {
        IEnumerable<TlQtChungTuChiTietQuery> FindByCondition(string maDonVi, int thang, int nam, string maCachTl);
        IEnumerable<TlQtChungTuChiTiet> FindByChungTuId(Guid id);
        int DeleteByChungTuId(Guid id);
        IEnumerable<TlQtChungTuChiTietQuery> FindByNamKeHoach(string maDonVi, int thang, int nam);

        IEnumerable<ReportQttxTheoCachTinhLuongQuery> FindReportQttxTheoCachTinhLuong(string maDonVi, int thang,
            int nam, string cachTinhLuong);
        void AddAggregate(TlQuyetToanChiTietTongHopCriteria creation);
        IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTiet(string idChungTu, int nam, string cachTinhLuong);
        IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa);
        IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTietExport(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL);
        IEnumerable<TlQtChungTuChiTietQuery> GetDataGiaiThichBangSo(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL);
        IEnumerable<ReportQttxTheoCotQuery> GetDataChungTuChiTietTheoCot(string idChungTu, int nam, string cachTinhLuong);
        IEnumerable<TlQtChungTuChiTietQuery> GetQuyetToanChiTietBHXH(string maDonVi, int thang, int nam);
    }
}
