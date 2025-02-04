using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtThongTriQuyetToanService : INhQtThongTriQuyetToanService
    {
        private readonly INhQtThongTriQuyetToanRepository _repository;

        public NhQtThongTriQuyetToanService(INhQtThongTriQuyetToanRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhQtThongTriQuyetToanQuery> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChi()
        {
            return _repository.GetLookupNhiemVuChi();
        }

        public IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChiByDonVi(Guid iID_DonViID)
        {
            return _repository.GetLookupNhiemVuChiByDonVi(iID_DonViID);
        }

        public void DeleteThongTriById(Guid Id)
        {
            var tt = _repository.FirstOrDefault(x => x.Id == Id);
            _repository.Delete(tt);
        }

        public Guid SaveAndGetIdThongTriQuyetToan(NhQtThongTriQuyetToan input)
        {
            return _repository.SaveAndGetIdThongTriQuyetToan(input);
        }

        public NhQtThongTriQuyetToanQuery GetThongTriById(Guid id)
        {
            return _repository.GetThongTriById(id);
        }

        public void UpdateThongTriQuyetToan(NhQtThongTriQuyetToan input)
        {
            _repository.Update(input);
        }

        public NhQtThongTriQuyetToan GetThongTriUpdateById(Guid id)
        {
            return _repository.FirstOrDefault(x => x.Id == id);
        }
    }
}
