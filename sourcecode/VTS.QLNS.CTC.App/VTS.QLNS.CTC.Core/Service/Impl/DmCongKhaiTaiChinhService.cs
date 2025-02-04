using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmCongKhaiTaiChinhService : IService<NsDanhMucCongKhai>, IDmCongKhaiTaiChinhService
    {
        private readonly IDmCongKhaiTaiChinhRepository _repository;

        public DmCongKhaiTaiChinhService(IDmCongKhaiTaiChinhRepository repository)
        {
            _repository = repository;
        }
        public void Add(NsDanhMucCongKhai entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<NsDanhMucCongKhai> entities)
        {
            _repository.AddRange(entities);
        }

        public override void AddOrUpdateRange(IEnumerable<NsDanhMucCongKhai> listEntities, AuthenticationInfo authenticationInfo)
        {
            var entities = listEntities.ToList();
            var childs = new List<NsDmCongKhaiMlns>();
            List<Guid> lstIdDeleteMlns = new List<Guid>();
            foreach (var entity in entities)
            {
                entity.iNamLamViec = authenticationInfo.YearOfWork;
                entity.NsDmCongKhaiMlns.ForAll(c => c.iNamLamViec = authenticationInfo.YearOfWork);
                entity.NsDmCongKhaiMlns.ForAll(x =>
                {
                    x.IsDeleted = entity.IsDeleted;
                    x.IsModified = entity.IsModified;
                });
                childs.AddRange(entity.NsDmCongKhaiMlns);
                lstIdDeleteMlns.Add(entity.Id);
                /*
                if (entity.NsDmCongKhaiMlns == null || !entity.NsDmCongKhaiMlns.Any(n => !string.IsNullOrEmpty(n.sNS_XauNoiMa)))
                {
                    lstIdDeleteMlns.Add(entity.Id);
                }
                */
            }
            entities.ForAll(c => c.NsDmCongKhaiMlns = new List<NsDmCongKhaiMlns>());
            _repository.AddOrUpdateRange(entities, authenticationInfo);
            if (lstIdDeleteMlns.Count != 0)
            {
                _repository.ClearMlnsByiIdDmCongKhai(lstIdDeleteMlns);
            }
            _repository.AddRangeMlns(childs, authenticationInfo);
            _repository.UpdateIsHangCha();

        }

        public void Delete(NsDanhMucCongKhai entity)
        {
            _repository.Delete(entity);
        }

        public void Update(NsDanhMucCongKhai entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<NsDanhMucCongKhai> entities)
        {
            _repository.UpdateRange(entities);
        }

        public override IEnumerable<NsDanhMucCongKhai> FindAll(AuthenticationInfo authenticationInfo)
        {
            var entities = _repository.FindAll(c => c.iNamLamViec == authenticationInfo.YearOfWork).ToList();
            var ids = entities.Select(c => c.Id);
            var mnls = _repository.GetAllMlns(ids);
            foreach (var entity in entities)
            {
                entity.NsDmCongKhaiMlns = mnls.Where(c => c.iID_DMCongKhai.Equals(entity.Id)).ToList();
            }
            return entities;
        }

        public IEnumerable<NsDanhMucCongKhai> FindByCondition(Expression<Func<NsDanhMucCongKhai, bool>> predicate)
        {
            return _repository.FindByCondition(predicate);
        }

        public IEnumerable<NsDmCongKhaiMlns> FindByCondition(Expression<Func<NsDmCongKhaiMlns, bool>> predicate)
        {
            return _repository.FindByCondition(predicate);
        }

        public IEnumerable<NsDmCongKhaiMlns> FindByConditionMLNS(Expression<Func<NsDmCongKhaiMlns, bool>> predicate)
        {
            return _repository.FindByCondition(predicate);
        }

        public IEnumerable<PrintPublicFinanceQuery> ReportPublicFinance(int yearOfWork, string ids, int yearOfBudget, int budgetSource)
        {
            return _repository.ReportPublicFinance(yearOfWork, ids, yearOfBudget, budgetSource);
        }

        public IEnumerable<NsDmCongKhaiMlns> GetAllMlns(IEnumerable<Guid> ids)
        {
            return _repository.GetAllMlns(ids);
        }

        public IEnumerable<NsDanhMucCongKhai> FindByXauNoiMa(string xauNoiMa, int namLamViec)
        {
            var mlCongKhai = _repository.GetByXauNoiMaMlns(xauNoiMa, namLamViec);
            var dict = mlCongKhai.Select(x => x.iID_DMCongKhai_Cha).ToHashSet();
            foreach (var item in mlCongKhai)
            {
                item.IsParent = dict.Contains(item.Id);
            }
            return mlCongKhai;
        }
    }
}
