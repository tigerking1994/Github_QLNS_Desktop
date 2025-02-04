using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuChiTietNq104Service : ITlQtChungTuChiTietNq104Service
    {
        private readonly ITlQtChungTuChiTietNq104Repository _tlQtChungTuChiTietRepository;

        public TlQtChungTuChiTietNq104Service(ITlQtChungTuChiTietNq104Repository tlQtChungTuChiTietRepository)
        {
            _tlQtChungTuChiTietRepository = tlQtChungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets)
        {
            return _tlQtChungTuChiTietRepository.AddRange(tlQtChungTuChiTiets);
        }

        public List<TlQtChungTuChiTietNq104Query> BaoCaoChiTietNamKeHoach(string maDonVi, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public int DeleteByChungTuId(Guid id)
        {
            return _tlQtChungTuChiTietRepository.DeleteByChungTuId(id);
        }

        public IEnumerable<TlQtChungTuChiTietNq104> FindByChungTuId(Guid id)
        {
            return _tlQtChungTuChiTietRepository.FindByChungTuId(id);
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> FindByCondition(string maDonVi, int thang, int nam, string maCachTl)
        {
            return _tlQtChungTuChiTietRepository.FindByCondition(maDonVi, thang, nam, maCachTl);
        }

        public int UpdateRange(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets)
        {
            return _tlQtChungTuChiTietRepository.UpdateRange(tlQtChungTuChiTiets);
        }

        public void Delete (TlQtChungTuChiTietNq104 chungtu)
        {
            _tlQtChungTuChiTietRepository.Delete(chungtu);
        }

        public IEnumerable<TlQtChungTuChiTietNq104> FindAll(Expression<Func<TlQtChungTuChiTietNq104, bool>> predicate)
        {
            return _tlQtChungTuChiTietRepository.FindAll(predicate);
        }

        public void AddAggregate(TlQuyetToanChiTietTongHopNq104Criteria creation)
        {
            _tlQtChungTuChiTietRepository.AddAggregate(creation);
        }

        public void BulkInsert(IEnumerable<TlQtChungTuChiTietNq104> tlQtChungTuChiTiets)
        {
            _tlQtChungTuChiTietRepository.BulkInsert(tlQtChungTuChiTiets);
        }

        public IEnumerable<ReportQttxTheoCachTinhLuongNq104Query> FindReportQttxTheoCachTinhLuongNq104(string maDonVi, int thang,
            int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.FindReportQttxTheoCachTinhLuongNq104(maDonVi, thang, nam, cachTinhLuong);
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104(string idChungTu, int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTietNq104(idChungTu, nam, cachTinhLuong);
        }

        public IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa)
        {
            return _tlQtChungTuChiTietRepository.GetDataMucLucNG(sXauNoiMa);
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104Export(string lstId, int nam, string maDonViTongHop, bool IsSummary, string sCachTl = "")
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTietNq104Export(lstId, nam, maDonViTongHop, IsSummary, sCachTl);
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataGiaiThichBangSoNq104(string lstId, int nam, string maDonViTongHop, bool IsSummary, string sCachTl = "")
        {
            return _tlQtChungTuChiTietRepository.GetDataGiaiThichBangSoNq104(lstId, nam, maDonViTongHop, IsSummary, sCachTl);
        }

        public IEnumerable<ReportQttxTheoCotNq104Query> GetDataChungTuChiTietTheoCotNq104(string idChungTu, int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTietTheoCotNq104(idChungTu, nam, cachTinhLuong);
        }

        public int Update(TlQtChungTuChiTietNq104 entity)
        {
            return _tlQtChungTuChiTietRepository.Update(entity);
        }
    }
}
