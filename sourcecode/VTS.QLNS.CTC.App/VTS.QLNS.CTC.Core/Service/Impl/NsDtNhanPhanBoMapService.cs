using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDtNhanPhanBoMapService : INsDtNhanPhanBoMapService
    {
        private readonly INsDtNhanPhanBoMapRepository _dtChungTuMapRepository;

        public NsDtNhanPhanBoMapService(INsDtNhanPhanBoMapRepository dtChungTuMapRepository)
        {
            _dtChungTuMapRepository = dtChungTuMapRepository;
        }

        public IEnumerable<NsDtNhanPhanBoMap> Save(IEnumerable<NsDtNhanPhanBoMap> dtChungTuMaps)
        {
            if (dtChungTuMaps == null || !dtChungTuMaps.Any())
            {
                return Enumerable.Empty<NsDtNhanPhanBoMap>();
            }

            _dtChungTuMapRepository.AddRange(dtChungTuMaps);
            return dtChungTuMaps;
        }

        public void DeleteByIdPhanBoDuToan(Guid id)
        {
            _dtChungTuMapRepository.DeleteByIdPhanBoDuToan(id);
        }

        public void RemoveDuplicate()
        {
            _dtChungTuMapRepository.RemoveDuplicate();
        }

        public void DeleteByIdNhanPhanBoDuToan(Guid id)
        {
            _dtChungTuMapRepository.DeleteByIdNhanPhanBoDuToan(id);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            return _dtChungTuMapRepository.FindByListIdNhanDuToan(listIdNhanDuToan);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo)
        {
            return _dtChungTuMapRepository.FindByListIdPhanBo(listIdPhanBo);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan)
        {
            return _dtChungTuMapRepository.FindByIdPhanBoDuToan(idPhanBoDuToan);
        }
        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToanDieuChinh(string idPhanBoDuToan)
        {
            return _dtChungTuMapRepository.FindByIdPhanBoDuToanDieuChinh(idPhanBoDuToan);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindListIdByListIdPhanBo(IEnumerable<string> listIdPhanBo, int yearOfWork)
        {
            return _dtChungTuMapRepository.FindListIdByListIdPhanBo(listIdPhanBo, yearOfWork);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            return _dtChungTuMapRepository.FindByIdNhanDuToan(idNhanDuToan);
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByCondition(Expression<Func<NsDtNhanPhanBoMap, bool>> predicate)
        {
            return _dtChungTuMapRepository.FindAll(predicate);
        }
    }
}
