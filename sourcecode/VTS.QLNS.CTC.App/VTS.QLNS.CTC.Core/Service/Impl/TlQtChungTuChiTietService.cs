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
    public class TlQtChungTuChiTietService : ITlQtChungTuChiTietService
    {
        private readonly ITlQtChungTuChiTietRepository _tlQtChungTuChiTietRepository;

        public TlQtChungTuChiTietService(ITlQtChungTuChiTietRepository tlQtChungTuChiTietRepository)
        {
            _tlQtChungTuChiTietRepository = tlQtChungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets)
        {
            return _tlQtChungTuChiTietRepository.AddRange(tlQtChungTuChiTiets);
        }

        public List<TlQtChungTuChiTietQuery> BaoCaoChiTietNamKeHoach(string maDonVi, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public int DeleteByChungTuId(Guid id)
        {
            return _tlQtChungTuChiTietRepository.DeleteByChungTuId(id);
        }

        public IEnumerable<TlQtChungTuChiTiet> FindByChungTuId(Guid id)
        {
            return _tlQtChungTuChiTietRepository.FindByChungTuId(id);
        }

        public IEnumerable<TlQtChungTuChiTietQuery> FindByCondition(string maDonVi, int thang, int nam, string maCachTl)
        {
            return _tlQtChungTuChiTietRepository.FindByCondition(maDonVi, thang, nam, maCachTl);
        }

        public int UpdateRange(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets)
        {
            return _tlQtChungTuChiTietRepository.UpdateRange(tlQtChungTuChiTiets);
        }

        public void Delete (TlQtChungTuChiTiet chungtu)
        {
            _tlQtChungTuChiTietRepository.Delete(chungtu);
        }

        public IEnumerable<TlQtChungTuChiTiet> FindAll(Expression<Func<TlQtChungTuChiTiet, bool>> predicate)
        {
            return _tlQtChungTuChiTietRepository.FindAll(predicate);
        }

        public void AddAggregate(TlQuyetToanChiTietTongHopCriteria creation)
        {
            _tlQtChungTuChiTietRepository.AddAggregate(creation);
        }

        public void BulkInsert(IEnumerable<TlQtChungTuChiTiet> tlQtChungTuChiTiets)
        {
            _tlQtChungTuChiTietRepository.BulkInsert(tlQtChungTuChiTiets);
        }

        public IEnumerable<ReportQttxTheoCachTinhLuongQuery> FindReportQttxTheoCachTinhLuong(string maDonVi, int thang,
            int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.FindReportQttxTheoCachTinhLuong(maDonVi, thang, nam, cachTinhLuong);
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTiet(string idChungTu, int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTiet(idChungTu, nam, cachTinhLuong);
        }

        public IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa)
        {
            return _tlQtChungTuChiTietRepository.GetDataMucLucNG(sXauNoiMa);
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTietExport(string lstId, int nam, string maDonViTongHop, bool IsSummary, string sCachTl = "")
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTietExport(lstId, nam, maDonViTongHop, IsSummary, sCachTl);
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataGiaiThichBangSo(string lstId, int nam, string maDonViTongHop, bool IsSummary, string sCachTl = "")
        {
            return _tlQtChungTuChiTietRepository.GetDataGiaiThichBangSo(lstId, nam, maDonViTongHop, IsSummary, sCachTl);
        }

        public IEnumerable<ReportQttxTheoCotQuery> GetDataChungTuChiTietTheoCot(string idChungTu, int nam, string cachTinhLuong)
        {
            return _tlQtChungTuChiTietRepository.GetDataChungTuChiTietTheoCot(idChungTu, nam, cachTinhLuong);
        }

        public int Update(TlQtChungTuChiTiet entity)
        {
            return _tlQtChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetQuyetToanChiTietBHXH(string maDonVi, int thang, int nam)
        {
            return _tlQtChungTuChiTietRepository.GetQuyetToanChiTietBHXH(maDonVi, thang, nam);
        }
    }
}
