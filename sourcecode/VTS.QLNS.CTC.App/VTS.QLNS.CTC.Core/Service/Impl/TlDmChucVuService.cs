using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmChucVuService : ITlDmChucVuService
    {
        private ITlDmChucVuRepository _tlDmChucVuRepository;
        public TlDmChucVuService(ITlDmChucVuRepository tlDmChucVuRepository)
        {
            _tlDmChucVuRepository = tlDmChucVuRepository;
        }
        public IEnumerable<TlDmChucVu> FindAll()
        {
            return _tlDmChucVuRepository.FindAll();
        }

        public TlDmChucVu FindByHeSoChucVu(decimal? heSoCv)
        {
            return _tlDmChucVuRepository.FindByHeSoChucVu(heSoCv);
        }

        public TlDmChucVu FindByMaChucVu(string maChucVu)
        {
            return _tlDmChucVuRepository.FindByMaChucVu(maChucVu);
        }
    }
}
