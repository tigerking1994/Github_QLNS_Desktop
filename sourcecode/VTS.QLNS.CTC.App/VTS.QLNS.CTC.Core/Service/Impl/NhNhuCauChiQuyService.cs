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
    public class NhNhuCauChiQuyService : INhNhuCauChiQuyService
    {
        private INhNhuCauChiQuyRepository _repository;
        private INhNhuCauChiQuyChiTietService _nhNhuCauChiQuyChiTietService;

        public NhNhuCauChiQuyService(INhNhuCauChiQuyRepository repository,
            INhNhuCauChiQuyChiTietService nhNhuCauChiQuyChiTietService)
        {
            _repository = repository;
            _nhNhuCauChiQuyChiTietService = nhNhuCauChiQuyChiTietService;
        }

        public void Add(NhNhuCauChiQuy entity)
        {
            _repository.Add(entity);
            _nhNhuCauChiQuyChiTietService.Save(entity.NhNhuCauChiQuyChiTiets);
        }

        public void Delete(NhNhuCauChiQuy entity)
        {
            _nhNhuCauChiQuyChiTietService.Delete(entity.NhNhuCauChiQuyChiTiets);
            _repository.Delete(entity);
        }

        public NhNhuCauChiQuy FindById(Guid Id)
        {
            return _repository.Find(Id);
        }

        public IEnumerable<NhNhuCauChiQuyQuery> GetAll()
        {
            return _repository.GetAll();
        }

        public void LockOrUnLock(NhNhuCauChiQuy entity)
        {
            entity.BIsKhoa = !entity.BIsKhoa;
            _repository.Update(entity);
        }

        public IEnumerable<NhNhuCauChiQuyBaoCaoQuery> ReportNhuCauChiQuy(Guid idChiQuy)
        {
            return _repository.ReportNhuCauChiQuy(idChiQuy);
        }

        public void Update(NhNhuCauChiQuy entity)
        {
            _repository.Update(entity);
            _nhNhuCauChiQuyChiTietService.Save(entity.NhNhuCauChiQuyChiTiets);
        }

        public void UpDateRange(List<NhNhuCauChiQuy> entities)
        {
            _repository.UpdateRange(entities);
        }
        public string GetSTTLAMA(int STT)
        {
            return _repository.GetSTTLAMA(STT);
        }

        public string UpdateChilrenGeneral(Guid? Id)
        {
            return _repository.UpdateChilrenGeneral(Id);
        }
    }
}
