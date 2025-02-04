using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietService
    {
        IEnumerable<TlQtChungTuChiTiet> FindByChungTuId(Guid id);
        IEnumerable<TlQtChungTuChiTietQuery> FindByCondition(string maDonVi, int thang, int nam, string maCachTl);
        int AddRange(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets);
        int DeleteByChungTuId(Guid id);
        int UpdateRange(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets);
        void Delete(TlQtChungTuChiTiet chungtu);
        List<TlQtChungTuChiTietQuery> BaoCaoChiTietNamKeHoach(string maDonVi, int fromYear, int toYear);
        IEnumerable<TlQtChungTuChiTiet> FindAll(Expression<Func<TlQtChungTuChiTiet, bool>> predicate);
        void AddAggregate(TlQuyetToanChiTietTongHopCriteria creation);
        void BulkInsert(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets);
        IEnumerable<ReportQttxTheoCachTinhLuongQuery> FindReportQttxTheoCachTinhLuong(string maDonVi, int thang, int nam, string cachTinhLuong);
        IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTiet(string idChungTu, int nam, string cachTinhLuong);
        IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa);
        IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTietExport(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL = "");
        IEnumerable<TlQtChungTuChiTietQuery> GetDataGiaiThichBangSo(string lstId, int nam, string maDonViTongHop, bool isSummary, string sCachTL = "");
        IEnumerable<ReportQttxTheoCotQuery> GetDataChungTuChiTietTheoCot(string idChungTu, int nam, string cachTinhLuong);
        int Update(TlQtChungTuChiTiet entity);
        IEnumerable<TlQtChungTuChiTietQuery> GetQuyetToanChiTietBHXH(string maDonVi, int thang, int nam);
    }
}
