using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktSoLieuChiTietCanCuDataService : ISktSoLieuChiTietCanCuDataService
    {
        private readonly ISktSoLieuChiTietDataRepository _sktSoLieuRepository;

        public SktSoLieuChiTietCanCuDataService(ISktSoLieuChiTietDataRepository sktSoLieuRepository)
        {
            _sktSoLieuRepository = sktSoLieuRepository;
        }

        public int AddRange(IEnumerable<NsDtdauNamChungTuChiTietCanCu> entities)
        {
            return _sktSoLieuRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            return _sktSoLieuRepository.Delete(id);
        }

        public NsDtdauNamChungTuChiTietCanCu Find(params object[] keyValues)
        {
            return _sktSoLieuRepository.Find(keyValues);
        }

        public IEnumerable<NsDtdauNamChungTuChiTietCanCu> FindByCondition(Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicate)
        {
            return _sktSoLieuRepository.FindAll(predicate);
        }

        public IEnumerable<DuToanDauNamCanCuQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, int loaiCanCu, int namLamViec)
        {
            return _sktSoLieuRepository.FindCanCuSoNhuCau(lstChungTu, lstIdMucLuc, idDonVi, loaiCanCu, namLamViec);
        }

        public int RemoveRange(IEnumerable<NsDtdauNamChungTuChiTietCanCu> sktChungTuChiTiets)
        {
            return _sktSoLieuRepository.RemoveRange(sktChungTuChiTiets);
        }

        public int Update(NsDtdauNamChungTuChiTietCanCu entity)
        {
            return _sktSoLieuRepository.Update(entity);
        }

        public void BulkInsertNsDtdauNamChungTuChiTietCanCu(List<NsDtdauNamChungTuChiTietCanCu> lstData)
        {
            _sktSoLieuRepository.BulkInsert(lstData);
        }
    }
}

