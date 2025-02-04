using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietNq104Service
    {
        IEnumerable<TlQtChungTuChiTietNq104> FindByChungTuId(Guid id);
        IEnumerable<TlQtChungTuChiTietNq104Query> FindByCondition(string maDonVi, int thang, int nam, string maCachTl);
        int AddRange(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets);
        int DeleteByChungTuId(Guid id);
        int UpdateRange(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets);
        void Delete(TlQtChungTuChiTietNq104 chungtu);
        List<TlQtChungTuChiTietNq104Query> BaoCaoChiTietNamKeHoach(string maDonVi, int fromYear, int toYear);
        IEnumerable<TlQtChungTuChiTietNq104> FindAll(Expression<Func<TlQtChungTuChiTietNq104, bool>> predicate);
        void AddAggregate(TlQuyetToanChiTietTongHopNq104Criteria creation);
        void BulkInsert(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets);
        IEnumerable<ReportQttxTheoCachTinhLuongNq104Query> FindReportQttxTheoCachTinhLuongNq104(string maDonVi, int thang, int nam, string cachTinhLuong);
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104(string idChungTu, int nam, string cachTinhLuong);
        IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa);
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104Export(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL = "");
        IEnumerable<TlQtChungTuChiTietNq104Query> GetDataGiaiThichBangSoNq104(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL = "");
        IEnumerable<ReportQttxTheoCotNq104Query> GetDataChungTuChiTietTheoCotNq104(string idChungTu, int nam, string cachTinhLuong);
        int Update(TlQtChungTuChiTietNq104 entity);
    }
}
