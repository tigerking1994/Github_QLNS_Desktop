using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBaoCaoNq104Service : ITlBaoCaoNq104Service
    {
        private ITlBaoCaoNq104Repository _tlBaoCaoRepository;

        public TlBaoCaoNq104Service(ITlBaoCaoNq104Repository tlBaoCaoRepository)
        {
            _tlBaoCaoRepository = tlBaoCaoRepository;
        }

        public TlBaoCao Find(Guid id)
        {
            return _tlBaoCaoRepository.Find(id);
        }

        public IEnumerable<TlBaoCao> FindAll()
        {
            return _tlBaoCaoRepository.FindAll();
        }

        public IEnumerable<ExportLuongChiTietNgachCanBoNq104Query> FindLuongChiTiet(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietNgach(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietCapBac(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietNgachCapBac(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongNgachCanBoNq104Query> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongNgachCanBo(thang, nam, maDonVi, maCachTL);
        }

        public void UpdateBaoCao(List<TlBaoCao> entities)
        {
            _tlBaoCaoRepository.UpdateRange(entities);
        }

        public IEnumerable<TlBaoCao> FindByCondition(Expression<Func<TlBaoCao, bool>> predicate)
        {
            return _tlBaoCaoRepository.FindAll(predicate);
        }

        public int Add(TlBaoCao entity)
        {
            return _tlBaoCaoRepository.Add(entity);
        }

        public int Delete(TlBaoCao item)
        {
            return _tlBaoCaoRepository.Delete(item);
        }

        public int Update(TlBaoCao item)
        {
            return _tlBaoCaoRepository.Update(item);
        }
    }
}
