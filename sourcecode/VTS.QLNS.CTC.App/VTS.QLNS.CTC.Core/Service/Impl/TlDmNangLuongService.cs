using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmNangLuongService : ITlDmNangLuongService
    {
        private readonly ITlDmNangLuongRepository _tlDmNangLuongRepository;

        public TlDmNangLuongService(ITlDmNangLuongRepository tlDmNangLuongRepository)
        {
            _tlDmNangLuongRepository = tlDmNangLuongRepository;
        }

        public IEnumerable<TlDmNangLuong> FindAll()
        {
            return _tlDmNangLuongRepository.FindAll();
        }
    }
}
