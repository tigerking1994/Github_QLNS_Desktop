using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuToanNguonVonService : INhDaDuToanNguonVonService
    {
        private readonly INhDaDuToanNguonVonRepository _repository;

        public NhDaDuToanNguonVonService(INhDaDuToanNguonVonRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDaDuToanNguonVon> FindByDuToanId(Guid duToanId)
        {
            return _repository.FindAll(x => x.IIdDuToanId == duToanId);
        }
    }
}
