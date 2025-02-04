using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuAnNguonVonService : INhDaDuAnNguonVonService
    {
        private readonly INhDaDuAnNguonVonRepository _repository;

        public NhDaDuAnNguonVonService(INhDaDuAnNguonVonRepository nhDaDuAnNguonVonRepository)
        {
            _repository = nhDaDuAnNguonVonRepository;
        }
        public IEnumerable<NhDaDuAnNguonVon> FindAll(Expression<Func<NhDaDuAnNguonVon, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
        public int InsertDuAnNguonVon(IEnumerable<NhDaDuAnNguonVon> entities)
        {
            return _repository.AddRange(entities);
        }
        public int AddRange(List<NhDaDuAnNguonVon> entities)
        {
            return _repository.AddRange(entities);
        }

        public int Delete(NhDaDuAnNguonVon entity)
        {
            return _repository.Delete(entity);
        }

        public NhDaDuAnNguonVon FindById(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public IEnumerable<NhDaDuAnNguonVon> FindByDuAnId(Guid duAnId)
        {
            return _repository.FindAll(x => x.IIdDuAnId == duAnId);
        }

        public int Update(NhDaDuAnNguonVon entity)
        {
            return _repository.Update(entity);
        }
    }
}
