using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlPhuCapDieuChinhNq104Service : ITlPhuCapDieuChinhNq104Service
    {
        private readonly ITlPhuCapDieuChinhNq104Repository _tlPhuCapDieuChinhRepository;

        public TlPhuCapDieuChinhNq104Service(ITlPhuCapDieuChinhNq104Repository tlPhuCapDieuChinhRepository)
        {
            _tlPhuCapDieuChinhRepository = tlPhuCapDieuChinhRepository;
        }

        public int Add(TlPhuCapDieuChinhNq104 tlPhuCapDieuChinh)
        {
            return _tlPhuCapDieuChinhRepository.Add(tlPhuCapDieuChinh);
        }

        public int AddRange(IEnumerable<TlPhuCapDieuChinhNq104> tlPhuCapDieuChinhs)
        {
            return _tlPhuCapDieuChinhRepository.AddRange(tlPhuCapDieuChinhs);
        }

        public int Delete(Guid id)
        {
            return _tlPhuCapDieuChinhRepository.Delete(id);
        }

        public IEnumerable<TlPhuCapDieuChinhNq104> FindAll()
        {
            return _tlPhuCapDieuChinhRepository.FindAll();
        }

        public IEnumerable<TlPhuCapDieuChinhNq104Query> FindAllPhuCapDieuChinh()
        {
            return _tlPhuCapDieuChinhRepository.FindAllPhuCapDieuChinh();
        }

        public int Update(TlPhuCapDieuChinhNq104 tlPhuCapDieuChinh)
        {
            return _tlPhuCapDieuChinhRepository.Update(tlPhuCapDieuChinh);
        }
    }
}
