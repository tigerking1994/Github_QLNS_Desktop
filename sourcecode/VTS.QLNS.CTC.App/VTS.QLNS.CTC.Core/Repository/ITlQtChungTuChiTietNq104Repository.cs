using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietNq104Repository : IRepository<TlQtChungTuChiTietNq104>
    {
        IEnumerable<TlQtChungTuChiTietNq104Query> FindByCondition(string maDonVi, int thang, int nam, string maCachTl);
        IEnumerable<TlQtChungTuChiTietNq104> FindByChungTuId(Guid id);
        int DeleteByChungTuId(Guid id);
        IEnumerable<TlQtChungTuChiTietNq104Query> FindByNamKeHoach(string maDonVi, int thang, int nam);

        IEnumerable<ReportQttxTheoCachTinhLuongNq104Query> FindReportQttxTheoCachTinhLuongNq104(string maDonVi, int thang,
            int nam, string cachTinhLuong);
        void AddAggregate(TlQuyetToanChiTietTongHopNq104Criteria creation);
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104(string idChungTu, int nam, string cachTinhLuong);
        IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa);
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104Export(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL);
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataGiaiThichBangSoNq104(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL);
        IEnumerable<ReportQttxTheoCotNq104Query> GetDataChungTuChiTietTheoCotNq104(string idChungTu, int nam, string cachTinhLuong);
    }
}
