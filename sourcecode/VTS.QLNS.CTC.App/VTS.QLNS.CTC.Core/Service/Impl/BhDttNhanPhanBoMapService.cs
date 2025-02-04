using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDttNhanPhanBoMapService : IBhDttNhanPhanBoMapService
    {
        private readonly IBhDttNhanPhanBoMapRepository _dtChungTuMapRepository;

        public BhDttNhanPhanBoMapService(IBhDttNhanPhanBoMapRepository dtChungTuMapRepository)
        {
            _dtChungTuMapRepository = dtChungTuMapRepository;
        }

        public void DeleteByIdPhanBoDuToan(Guid id)
        {
            _dtChungTuMapRepository.DeleteByIdPhanBoDuToan(id);
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            return _dtChungTuMapRepository.FindByIdNhanDuToan(idNhanDuToan);
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan)
        {
            return _dtChungTuMapRepository.FindByIdPhanBoDuToan(idPhanBoDuToan);
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            return _dtChungTuMapRepository.FindByListIdNhanDuToan(listIdNhanDuToan);
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo)
        {
            return _dtChungTuMapRepository.FindByListIdPhanBo(listIdPhanBo);
        }

        public IEnumerable<BhDttNhanPhanBoMap> Save(IEnumerable<BhDttNhanPhanBoMap> dtChungTuMaps)
        {
            if (dtChungTuMaps == null || !dtChungTuMaps.Any())
            {
                return Enumerable.Empty<BhDttNhanPhanBoMap>();
            }
            _dtChungTuMapRepository.AddRange(dtChungTuMaps);
            return dtChungTuMaps;
        }
    }
}
