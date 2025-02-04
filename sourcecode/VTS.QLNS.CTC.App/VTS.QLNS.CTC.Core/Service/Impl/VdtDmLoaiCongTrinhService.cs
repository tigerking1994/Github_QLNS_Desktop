using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDmLoaiCongTrinhService : IVdtDmLoaiCongTrinhService
    {
        private readonly IVdtDmLoaiCongTrinhRepository _vdtDmLoaiCongTrinhRepository;
        
        public VdtDmLoaiCongTrinhService(IVdtDmLoaiCongTrinhRepository vdtDmLoaiCongTrinhRepository)
        {
            _vdtDmLoaiCongTrinhRepository = vdtDmLoaiCongTrinhRepository;
        }

        public IEnumerable<VdtDmLoaiCongTrinh> FindAll()
        {
            return _vdtDmLoaiCongTrinhRepository.FindAll();
        }

        public VdtDmLoaiCongTrinh FindById(Guid id)
        {
            return _vdtDmLoaiCongTrinhRepository.FindById(id);
        }

        public IEnumerable<VdtDmLoaiCongTrinh> FindByListId(List<Guid?> lstId)
        {
            return _vdtDmLoaiCongTrinhRepository.FindByListId(lstId);
        }
    }
}
