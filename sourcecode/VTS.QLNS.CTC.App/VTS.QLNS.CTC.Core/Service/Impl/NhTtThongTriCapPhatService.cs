using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhTtThongTriCapPhatService : INhTtThongTriCapPhatService
    {
        private INhTtThongTriCapPhatRepository _repository;
        private INhTtThongTriCapPhatChiTietService _nhTtThongChiCapPhatService;

        public NhTtThongTriCapPhatService(INhTtThongTriCapPhatRepository repository,
            INhTtThongTriCapPhatChiTietService nhTtThongChiCapPhatService)
        {
            _repository = repository;
            _nhTtThongChiCapPhatService = nhTtThongChiCapPhatService;
        }

        public void Add(NhTtThongTriCapPhat entity)
        {
            _repository.Add(entity);
            _nhTtThongChiCapPhatService.Save(entity.NhTtThongTriCapPhatChiTiets, entity);
        }

        public int Delete(NhTtThongTriCapPhat entity)
        {
            var lstThongChiChiTiet = _nhTtThongChiCapPhatService.FindByIdThongTriCapPhat(entity.Id);
            _nhTtThongChiCapPhatService.Delete(lstThongChiChiTiet);
            return _repository.Delete(entity);
        }

        public IEnumerable<NhTtThongTriCapPhatQuery> FindAllThongTri()
        {
            return _repository.FindAllThongTri();
        }

        public NhTtThongTriCapPhat FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public DataTable ReportThongTriCapPhat(Guid idThongTri)
        {
            return _repository.ReportThongTriCapPhat(idThongTri);
        }

        public void Update(NhTtThongTriCapPhat entity)
        {
            _repository.Update(entity);
            _nhTtThongChiCapPhatService.Save(entity.NhTtThongTriCapPhatChiTiets, entity);
        }
    }
}
