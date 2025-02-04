using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlGtTaiChinhService : ITlGtTaiChinhService
    {
        private readonly ITlGtTaiChinhRepository _tlGtTaiChinhRepository;

        public TlGtTaiChinhService (ITlGtTaiChinhRepository tlGtTaiChinhRepository)
        {
            _tlGtTaiChinhRepository = tlGtTaiChinhRepository;
        }

        public int Add(TlGtTaiChinh entity)
        {
            return _tlGtTaiChinhRepository.Add(entity);
        }

        public IEnumerable<TlGtTaiChinh> FindAll()
        {
            return _tlGtTaiChinhRepository.FindAll();
        }
    }
}
