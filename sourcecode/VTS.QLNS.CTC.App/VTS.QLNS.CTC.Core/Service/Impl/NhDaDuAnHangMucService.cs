using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuAnHangMucService : INhDaDuAnHangMucService
    {
        private readonly INhDaDuAnHangMucRepository _repository;

        public NhDaDuAnHangMucService(INhDaDuAnHangMucRepository nhDaDuAnHangMucRepository)
        {
            _repository = nhDaDuAnHangMucRepository;
        }

        public IEnumerable<NhDaDuAnHangMuc> FindAll(Expression<Func<NhDaDuAnHangMuc, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
        public int AddRange(IEnumerable<NhDaDuAnHangMuc> entities)
        {
            return _repository.AddRange(entities);
        }

        public int Delete(NhDaDuAnHangMuc entity)
        {
            return _repository.Delete(entity);
        }

        public int Update(NhDaDuAnHangMuc entity)
        {
            return _repository.Update(entity);
        }

        public int UpdateRange(IEnumerable<NhDaDuAnHangMuc> entities)
        {
            return _repository.UpdateRange(entities);
        }

        public IEnumerable<NhDaDuAnHangMuc> FindByDuAnId(Guid duAnId)
        {
            return _repository.FindAll(x => x.IIdDuAnId == duAnId).OrderBy(x => x.SMaOrder);
        }

        public NhDaDuAnHangMuc FindDuAnHangMuc(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }
    }
}
