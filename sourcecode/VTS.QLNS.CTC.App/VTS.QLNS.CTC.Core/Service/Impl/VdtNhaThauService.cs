using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtNhaThauService : IVdtNhaThauService
    {
        private readonly IVdtNhaThauRepository _vdtNhaThauRepository;

        public VdtNhaThauService(IVdtNhaThauRepository vdtNhaThauRepository)
        {
            _vdtNhaThauRepository = vdtNhaThauRepository;
        }

        public IEnumerable<VdtDmNhaThau> FindAll()
        {
            return _vdtNhaThauRepository.FindAll();
        }

        public VdtDmNhaThau FindById(Guid iIdNhaThau)
        {
            return _vdtNhaThauRepository.Find(iIdNhaThau);
        }

        public IEnumerable<string> GetListSTKByNhaThau(Guid iIdNhaThau)
        {
            return _vdtNhaThauRepository.GetListSTKByNhaThau(iIdNhaThau);
        }

        public IEnumerable<VdtDmNhaThau> GetNhaThauByHopDong(Guid iIdHopDongId)
        {
            return _vdtNhaThauRepository.GetNhaThauByHopDong(iIdHopDongId);
        }
    }
}
