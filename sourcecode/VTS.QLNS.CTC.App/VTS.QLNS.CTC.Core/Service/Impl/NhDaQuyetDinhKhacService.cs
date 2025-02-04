using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaQuyetDinhKhacService : IService<NhDaQuyetDinhKhac>, INhDaQuyetDinhKhacService
    {
        private readonly INhDaQuyetDinhKhacRepository _repository;
        private readonly INhDaQuyetDinhKhacChiPhiRepository _quyetDinhKhacChiPhiRepository;


        public NhDaQuyetDinhKhacService(
            INhDaQuyetDinhKhacRepository repository,
            INhDaQuyetDinhKhacChiPhiRepository quyetDinhKhacChiPhiRepository)
        {
            _repository = repository;
            _quyetDinhKhacChiPhiRepository = quyetDinhKhacChiPhiRepository;

        }
        public void Add(NhDaQuyetDinhKhac entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<NhDaQuyetDinhKhac> data)
        {
            _repository.AddRange(data);
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public void Delete(NhDaQuyetDinhKhac entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<NhDaQuyetDinhKhac> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhDaQuyetDinhKhac> FindAll(Expression<Func<NhDaQuyetDinhKhac, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhDaQuyetDinhKhac FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDaQuyetDinhKhacQuery> FindIndex(int iMenu)
        {
            return _repository.FindIndex(iMenu);
        }

        public void Update(NhDaQuyetDinhKhac entity)
        {
            _repository.Update(entity);
        }

        public IEnumerable<NhDaQuyetDinhKhac> FindByCondition(Expression<Func<NhDaQuyetDinhKhac, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
    }
}
