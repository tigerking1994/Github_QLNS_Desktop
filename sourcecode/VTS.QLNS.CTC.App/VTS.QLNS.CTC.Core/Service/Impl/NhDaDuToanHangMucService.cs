using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuToanHangMucService : INhDaDuToanHangMucService
    {
        private readonly INhDaDuToanHangMucRepository _repository;

        public NhDaDuToanHangMucService(INhDaDuToanHangMucRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDaDuToanHangMuc> FindByDuToanChiPhiId(Guid duToanChiPhiId)
        {
            return _repository.FindAll(x => x.IIdDuToanChiPhiId == duToanChiPhiId).OrderBy(x => x.SMaOrder);
        }
    }
}
