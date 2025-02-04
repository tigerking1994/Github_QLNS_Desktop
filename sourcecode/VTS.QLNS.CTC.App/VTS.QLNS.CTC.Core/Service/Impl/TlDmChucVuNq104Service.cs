using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmChucVuNq104Service : ITlDmChucVuNq104Service
    {
        private ITlDmChucVuNq104Repository _tlDmChucVuRepository;
        public TlDmChucVuNq104Service(ITlDmChucVuNq104Repository tlDmChucVuRepository)
        {
            _tlDmChucVuRepository = tlDmChucVuRepository;
        }
        public IEnumerable<TlDmChucVuNq104> FindAll()
        {
            return _tlDmChucVuRepository.FindAll();
        }

        public IEnumerable<TlDmChucVuNq104> FindAll(Expression<Func<TlDmChucVuNq104, bool>> predicate)
        {
            return _tlDmChucVuRepository.FindAll(predicate);
        }

        public TlDmChucVuNq104 FindByHeSoChucVu(decimal? heSoCv)
        {
            return _tlDmChucVuRepository.FindByHeSoChucVu(heSoCv);
        }

        public TlDmChucVuNq104 FindByMaChucVu(string maChucVu)
        {
            return _tlDmChucVuRepository.FindByMaChucVu(maChucVu);
        }
    }
}
