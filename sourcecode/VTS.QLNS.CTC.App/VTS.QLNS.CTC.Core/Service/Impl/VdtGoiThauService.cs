using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtGoiThauService : IVdtGoiThauService
    {
        private readonly IVdtGoiThauRepository _vdtGoiThauRepository;

        public VdtGoiThauService(IVdtGoiThauRepository vdtGoiThauRepository)
        {
            _vdtGoiThauRepository = vdtGoiThauRepository;
        }

        public IEnumerable<GoiThauQuery> FindGoiThauByDuAnId(string idDuAn, Guid? iIdHopDong)
        {
            return _vdtGoiThauRepository.FindGoiThauByDuAnId(idDuAn, iIdHopDong);
        }

        public IEnumerable<GoiThauQuery> FindGoiThauByHopDong(Guid? iIdHopDong)
        {
            return _vdtGoiThauRepository.FindGoiThauByHopDong(iIdHopDong);
        }

        public IEnumerable<VdtDaGoiThau> FindGoiThauDieuChinh(Guid khLuaChonNhaThauId)
        {
            return _vdtGoiThauRepository.FindGoiThauDieuChinh(khLuaChonNhaThauId);
        }
    }
}
