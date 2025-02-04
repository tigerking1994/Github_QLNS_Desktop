using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmMucLucQuyetToanService : IService<NsMucLucQuyetToanNam>, IDmMucLucQuyetToanService
    {
        private readonly IDmMucLucQuyetToanRepository _repository;

        public DmMucLucQuyetToanService(IDmMucLucQuyetToanRepository repository)
        {
            _repository = repository;
        }
        public void Add(NsMucLucQuyetToanNam entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<NsMucLucQuyetToanNam> entities)
        {
            _repository.AddRange(entities);
        }

        public override void AddOrUpdateRange(IEnumerable<NsMucLucQuyetToanNam> listEntities, AuthenticationInfo authenticationInfo)
        {
            var entities = listEntities.ToList();
            var childs = new List<NsMucLucQuyetToanNamMLNS>();
            List<string> lstIdDeleteMlns = new List<string>();
            foreach (var entity in entities)
            {
                entity.NamLamViec = authenticationInfo.YearOfWork;
                entity.NsMucLucQuyetToanNamMLNS.ForAll(c => c.NamLamViec = authenticationInfo.YearOfWork);
                entity.NsMucLucQuyetToanNamMLNS.ForAll(x =>
                {
                    x.IsDeleted = entity.IsDeleted;
                    x.IsModified = entity.IsModified;
                });
                childs.AddRange(entity.NsMucLucQuyetToanNamMLNS);
                lstIdDeleteMlns.Add(entity.Ma);
            }
            entities.ForAll(c => c.NsMucLucQuyetToanNamMLNS = new List<NsMucLucQuyetToanNamMLNS>());
            _repository.AddOrUpdateRange(entities, authenticationInfo);
            if (lstIdDeleteMlns.Count != 0)
            {
                _repository.ClearMlnsByIdMlqt(lstIdDeleteMlns);
            }
            _repository.AddRangeMlns(childs, authenticationInfo);
            _repository.UpdateIsHangCha();

        }

        public void Delete(NsMucLucQuyetToanNam entity)
        {
            _repository.Delete(entity);
        }

        public void Update(NsMucLucQuyetToanNam entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<NsMucLucQuyetToanNam> entities)
        {
            _repository.UpdateRange(entities);
        }

        public override IEnumerable<NsMucLucQuyetToanNam> FindAll(AuthenticationInfo authenticationInfo)
        {
            var entities = _repository.FindAll(c => c.NamLamViec == authenticationInfo.YearOfWork).OrderBy(x => x.Ma).ToList();
            var ids = entities.Select(c => c.Ma);
            var mnls = _repository.GetAllMlns(ids, authenticationInfo.YearOfWork);
            foreach (var entity in entities)
            {
                entity.NsMucLucQuyetToanNamMLNS = mnls.Where(c => c.MaMLQT.Equals(entity.Ma)).ToList();
            }
            return entities;
        }

        public IEnumerable<NsMucLucQuyetToanNam> FindByCondition(Expression<Func<NsMucLucQuyetToanNam, bool>> predicate)
        {
            return _repository.FindByCondition(predicate);
        }

        public IEnumerable<NsMucLucQuyetToanNamMLNS> GetAllMlns(IEnumerable<string> ids, int yearOfWork)
        {
            return _repository.GetAllMlns(ids, yearOfWork);
        }

    }
}
