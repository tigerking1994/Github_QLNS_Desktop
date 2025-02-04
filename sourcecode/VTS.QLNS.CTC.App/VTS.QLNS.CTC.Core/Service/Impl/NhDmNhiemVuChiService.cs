using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmNhiemVuChiService : IService<NhDmNhiemVuChi>, INhDmNhiemVuChiService
    {
        private readonly INhDmNhiemVuChiRepository _repository;

        public NhDmNhiemVuChiService(INhDmNhiemVuChiRepository nhDmNhiemVuChiRepository)
        {
            _repository = nhDmNhiemVuChiRepository;
        }

        public IEnumerable<NhDmNhiemVuChi> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindAllFillter(Guid donViId)
        {
            return _repository.FindAllFillter(donViId);
        }

        public override IEnumerable<NhDmNhiemVuChi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll();
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmNhiemVuChi> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(x => x.Id.IsNullOrEmpty()).ToList();
            var lstUpdate = listEntities.Where(x => !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            var lstDelete = listEntities.Where(x => x.IsDeleted).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _repository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                _repository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                _repository.RemoveRange(lstDelete);
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindByKhTongTheIdAndDonViId(Guid idNhKhTongThe, Guid idDonVi)
        {
            return _repository.FindByKhTongTheIdAndDonViId(idNhKhTongThe, idDonVi);
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindByDonViId(Guid idDonVi)
        {
            return _repository.FindByDonViId(idDonVi);
        }

        public IEnumerable<NhDmNhiemVuChi> FindByCondition(Expression<Func<NhDmNhiemVuChi, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDonViId(Guid idDonVi)
        {
            return _repository.FindNhiemVuChiDuToanByDonViId(idDonVi);
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDuToanId(Guid idDuToan)
        {
            return _repository.FindNhiemVuChiDuToanByDuToanId(idDuToan);
        }
        
        public int Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
