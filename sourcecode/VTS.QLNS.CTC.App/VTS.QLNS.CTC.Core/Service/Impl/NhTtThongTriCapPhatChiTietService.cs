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
    public class NhTtThongTriCapPhatChiTietService : INhTtThongTriCapPhatChiTietService
    {
        private INhTtThongTriCapPhatChiTietRepository _repository;

        public NhTtThongTriCapPhatChiTietService(INhTtThongTriCapPhatChiTietRepository repository)
        {
            _repository = repository;
        }

        public int Delete(IEnumerable<NhTtThongTriCapPhatChiTiet> entities)
        {
            return _repository.RemoveRange(entities);
        }

        public IEnumerable<NhTtThongTriCapPhatChiTietQuery> FindAllChiTiet()
        {
            return _repository.FindAllChiTiet();
        }
        public IEnumerable<NhTtThongTriCapPhatChiTiet> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhTtThongTriCapPhatChiTiet> FindByIdThongTriCapPhat(Guid Id)
        {
            return _repository.FindAll().Where(x => x.IIdThongTriCapPhatId == Id);
        }

        public void Save(IEnumerable<NhTtThongTriCapPhatChiTiet> entities, NhTtThongTriCapPhat entity)
        {
            var listAdd = entities.Where(x => x.IsAdded).ToList();
            var listDelete = entities.Where(x => x.IsDeleted).ToList();
            if (listAdd != null && listAdd.Count() > 0)
            {
                listAdd.Select(x =>
                {
                    x.IIdThongTriCapPhatId = entity.Id;
                    return x;
                }).ToList();
                _repository.AddRange(listAdd);
            }
            if (listDelete != null && listDelete.Count() > 0)
            {
                _repository.RemoveRange(listDelete);
            }
        }
    }
}
