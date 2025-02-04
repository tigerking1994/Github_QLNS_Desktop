using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuChiTietGiaiThichNq104Service : ITlQtChungTuChiTietGiaiThichNq104Service
    {
        private ITlQtChungTuChiTietGiaiThichNq104Repository _tlQtChungTuChiTietGiaiThichRepository;

        public TlQtChungTuChiTietGiaiThichNq104Service(ITlQtChungTuChiTietGiaiThichNq104Repository lQtChungTuChiTietGiaiThichRepository)
        {
            _tlQtChungTuChiTietGiaiThichRepository = lQtChungTuChiTietGiaiThichRepository;
        }

        public TlQtChungTuChiTietGiaiThichNq104 FindByChungTuId(Guid chungTuId)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.FindByChungTuId(chungTuId);
        }

        public TlQtChungTuChiTietGiaiThichNq104 FindByCondition(string thang, int nam, string maDonVi)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.FindByCondition(thang, nam, maDonVi);
        }

        public int Add(TlQtChungTuChiTietGiaiThichNq104 entity)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.Add(entity);
        }

        public int Update(TlQtChungTuChiTietGiaiThichNq104 entity)
        {
            return _tlQtChungTuChiTietGiaiThichRepository.Update(entity);
        }
    }
}
