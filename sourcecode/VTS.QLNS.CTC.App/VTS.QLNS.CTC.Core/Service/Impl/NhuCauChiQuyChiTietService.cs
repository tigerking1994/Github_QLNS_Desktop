using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhuCauChiQuyChiTietService : INhNhuCauChiQuyChiTietService
    {
        private INhNhuCauChiQuyChiTietRepository _repository;

        public NhuCauChiQuyChiTietService(INhNhuCauChiQuyChiTietRepository repository)
        {
            _repository = repository;
        }

        public void Save(IEnumerable<NhNhuCauChiQuyChiTiet> entities)
        {
            var listAdd = entities.Where(x => (x.IsAdded || x.IsModified) && !x.IsDeleted);
            //var listEdit = entities.Where(x => !x.IsAdded && !x.IsDeleted && x.IsModified);
            var listDelete = entities.Where(x => x.IsDeleted && x.Id != Guid.Empty);

            if (listAdd != null && listAdd.Count() > 0)
            {
                _repository.AddOrUpdateRange(listAdd);
            }
            if (listDelete != null && listDelete.Count() > 0)
            {
                _repository.RemoveRange(listDelete);
            }
        }

        public IEnumerable<NhuCauChiQuyNhiemVuChiQuery> FindNhiemVuChiByIdDonVi(Guid? idDonVi)
        {
            return _repository.FindNhiemVuChiByIdDonVi(idDonVi);
        }

        public NhNhuCauChiQuyChiTiet FindByIdHopDong(Guid? idHopDong)
        {
            return _repository.FindByIdHopDong(idHopDong);
        }
        

        public void Delete(IEnumerable<NhNhuCauChiQuyChiTiet> entities)
        {
            _repository.RemoveRange(entities);
        }

        public IEnumerable<NhNhuCauChiQuyChiTiet> FindByIdChiQuy(Guid idChiQuy)
        {
            return _repository.FindAll().Where(x => x.IIdNhuCauChiQuyId == idChiQuy);
        }

        public void AddRange(List<NhNhuCauChiQuyChiTiet> entities)
        {
            _repository.AddRange(entities);
        }

        public IEnumerable<NhNhuCauChiQuyKinhPhiDaChiQuery> KinhPhiDaChi(Guid idHopDong, int nam)
        {
            return _repository.KinhPhiDaChi(idHopDong, nam);
        }
    }
}
