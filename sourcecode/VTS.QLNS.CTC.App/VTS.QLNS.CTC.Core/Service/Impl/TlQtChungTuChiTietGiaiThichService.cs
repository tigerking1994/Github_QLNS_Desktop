using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuChiTietGiaiThichService : ITlQtChungTuChiTietGiaiThichService
    {
        private ITlQtChungTuChiTietGiaiThichRepository _tlQtChungTuChiTietGiaiThichRepository;

        public TlQtChungTuChiTietGiaiThichService(ITlQtChungTuChiTietGiaiThichRepository lQtChungTuChiTietGiaiThichRepository)
        {
            _tlQtChungTuChiTietGiaiThichRepository = lQtChungTuChiTietGiaiThichRepository;
        }

        public TlQtChungTuChiTietGiaiThich FindByChungTuId(Guid chungTuId)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.FindByChungTuId(chungTuId);
        }

        public TlQtChungTuChiTietGiaiThich FindByCondition(string thang, int nam, string maDonVi)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.FindByCondition(thang, nam, maDonVi);
        }

        public int Add(TlQtChungTuChiTietGiaiThich entity)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.Add(entity);
        }

        public int Update(TlQtChungTuChiTietGiaiThich entity)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.Update(entity);
        }

        public int RemoveRange(IEnumerable<TlQtChungTuChiTietGiaiThich> explanations)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.RemoveRange(explanations);
        }

        public IEnumerable<TlQtChungTuChiTietGiaiThich> FindListByChungTuId(Guid chungTuId)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.FindListByChungTuId(chungTuId);
        }
    }
}
