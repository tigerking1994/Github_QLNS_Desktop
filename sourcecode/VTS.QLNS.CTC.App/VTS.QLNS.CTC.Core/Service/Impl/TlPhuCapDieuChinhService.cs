using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlPhuCapDieuChinhService : ITlPhuCapDieuChinhService
    {
        private readonly ITlPhuCapDieuChinhRepository _tlPhuCapDieuChinhRepository;

        public TlPhuCapDieuChinhService(ITlPhuCapDieuChinhRepository tlPhuCapDieuChinhRepository)
        {
            _tlPhuCapDieuChinhRepository = tlPhuCapDieuChinhRepository;
        }

        public int Add(TlPhuCapDieuChinh tlPhuCapDieuChinh)
        {
            return _tlPhuCapDieuChinhRepository.Add(tlPhuCapDieuChinh);
        }

        public int AddRange(IEnumerable<TlPhuCapDieuChinh> tlPhuCapDieuChinhs)
        {
            return _tlPhuCapDieuChinhRepository.AddRange(tlPhuCapDieuChinhs);
        }

        public int Delete(Guid id)
        {
            return _tlPhuCapDieuChinhRepository.Delete(id);
        }

        public IEnumerable<TlPhuCapDieuChinh> FindAll()
        {
            return _tlPhuCapDieuChinhRepository.FindAll();
        }

        public IEnumerable<TlPhuCapDieuChinhQuery> FindAllPhuCapDieuChinh()
        {
            return _tlPhuCapDieuChinhRepository.FindAllPhuCapDieuChinh();
        }

        public int Update(TlPhuCapDieuChinh tlPhuCapDieuChinh)
        {
            return _tlPhuCapDieuChinhRepository.Update(tlPhuCapDieuChinh);
        }
    }
}
