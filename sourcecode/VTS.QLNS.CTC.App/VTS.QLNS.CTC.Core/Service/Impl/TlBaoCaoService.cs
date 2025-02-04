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
    public class TlBaoCaoService : ITlBaoCaoService
    {
        private ITlBaoCaoRepository _tlBaoCaoRepository;

        public TlBaoCaoService(ITlBaoCaoRepository tlBaoCaoRepository)
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

        public IEnumerable<ExportLuongChiTietNgachCanBoQuery> FindLuongChiTiet(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietNgach(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietCapBac(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            return _tlBaoCaoRepository.FindLuongChiTietNgachCapBac(thang, nam, maDonVi, maCachTL);
        }

        public IEnumerable<ExportLuongNgachCanBoQuery> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL)
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
