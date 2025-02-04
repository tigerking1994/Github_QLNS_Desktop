using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuToanChiPhiService : INhDaDuToanChiPhiService
    {
        private readonly INhDaDuToanChiPhiRepository _repository;

        public NhDaDuToanChiPhiService(INhDaDuToanChiPhiRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDaDuToanChiPhi> FindByDuToanId(Guid duToanId)
        {
            return _repository.FindAll(x => x.IIdDuToanId == duToanId).OrderBy(x => x.SMaOrder);
        }

        public IEnumerable<NhDaDuToanChiPhi> FindByNguonVonId(Guid NguonVonId)
        {
            return _repository.FindAll(x => x.IIdDuToanNguonVonId == NguonVonId).OrderBy(x => x.SMaOrder);
        }
    }
}
