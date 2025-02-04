using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQsMucLucService : IService<NsQsMucLuc>, INsQsMucLucService
    {
        private INsQsMucLucRepository _mucLucRepository;
        public NsQsMucLucService(INsQsMucLucRepository mucLucRepository)
        {
            _mucLucRepository = mucLucRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NsQsMucLuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
            }
            _mucLucRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<NsQsMucLuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _mucLucRepository.FindAll(d => authenticationInfo.YearOfWork == d.INamLamViec).OrderBy(d => d.SKyHieu);
        }

        public IEnumerable<NsQsMucLuc> FindByCondition(int yearOfWork)
        {
            return _mucLucRepository.FindByCondition(yearOfWork);
        }

        public override void ImportDataExcel(IEnumerable<NsQsMucLuc> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            var time = DateTime.Now;
            IEnumerable<Guid> modifiedIds = listEntities.Select(i => i.Id);
            List<NsQsMucLuc> entities = listEntities.ToList();
            for (int i = 0; i < entities.Count(); i++)
            {
                NsQsMucLuc item = entities[i];
                NsQsMucLuc track = _mucLucRepository.FirstOrDefault(i => i.Id == item.Id);
                if (track == null)
                {
                    item.IIdMlns = Guid.NewGuid();
                }
                else
                {
                    track.SM = item.SM;
                    track.STm = item.STm;
                    track.SKyHieu = item.SKyHieu;
                    track.SMoTa = item.SMoTa;
                    track.SHienThi = item.SHienThi;
                    track.INamLamViec = item.INamLamViec;
                    item = ObjectCopier.Clone(track);
                }
                item.IsModified = true;
                if (string.IsNullOrEmpty(item.STm))
                {
                    item.BHangCha = true;
                }
                entities[i] = item;
            }
            for (int i = 0; i < entities.Count(); i++)
            {
                NsQsMucLuc item = entities[i];
                NsQsMucLuc parent = FindParent(item, entities, modifiedIds);
                if (parent != null)
                {
                    item.IIdMlnsCha = parent.IIdMlns;
                }
            }
            _mucLucRepository.AddOrUpdateRange(listEntities);
        }

        public override bool ValidateDataExcel(IEnumerable<NsQsMucLuc> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            foreach (var item in listEntities)
            {
                var predicate = PredicateBuilder.True<NsQsMucLuc>();
                predicate = predicate.And(x => x.INamLamViec == item.INamLamViec);
                predicate = predicate.And(x => x.SM == item.SM);
                predicate = predicate.And(x => x.STm == item.STm);
                int countDuplicateIdCodes = listEntities.Where(predicate.Compile()).Count();
                if (countDuplicateIdCodes > 1)
                {
                    throw new ArgumentException("Mục " + item.SM + ", tiểu mục " + item.STm + " bị lặp!");
                }
                if (CheckExistMAndTm(predicate, excludeIds))
                {
                    throw new ArgumentException("Mục " + item.SM + ", tiểu mục " + item.STm + " đã tồn tại!");
                }
            }
            return true;
        }

        private bool CheckExistMAndTm(Expression<Func<NsQsMucLuc, bool>> predicate, IEnumerable<Guid> excludeIds)
        {
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsQsMucLuc> danhMuc = _mucLucRepository.FindAll(predicate).ToList();
            return danhMuc.Count() != 0;
        }

        private NsQsMucLuc FindParent(NsQsMucLuc child, IEnumerable<NsQsMucLuc> modifiedList, IEnumerable<Guid> excludeIds)
        {
            if (string.IsNullOrEmpty(child.STm))
            {
                return null;
            }
            NsQsMucLuc parent = modifiedList.FirstOrDefault(i => child.INamLamViec == i.INamLamViec
                && child.SM == i.SM && string.IsNullOrEmpty(i.STm));
            if (parent != null)
            {
                return parent;
            }
            parent = _mucLucRepository.FirstOrDefault(i => child.INamLamViec == i.INamLamViec
                && child.SM == i.SM && string.IsNullOrEmpty(i.STm));
            return parent;
        }

        public override IEnumerable<NsQsMucLuc> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _mucLucRepository.FindAll(i => i.INamLamViec == authenticationInfo.YearOfWork).OrderBy(d => d.SKyHieu).ToList();
        }

        public int countMLQSByNamLamViec(int yearOfWork)
        {
            return _mucLucRepository.countMLQSByNamLamViec(yearOfWork);
        }

        public IEnumerable<NsQsMucLuc> FindAll()
        {
            var data = _mucLucRepository.FindAll();
            return data;
        }

        public NsQsMucLuc FindMaMLNS(string MLNS)
        {
            return _mucLucRepository.FindMaMLNS(Guid.Parse(MLNS));
        }

        public IEnumerable<NsQsMucLuc> FindAll(Expression<Func<NsQsMucLuc, bool>> predicate)
        {
            return _mucLucRepository.FindAll(predicate);
        }

        public NsQsMucLuc FirstOrDefault(Expression<Func<NsQsMucLuc, bool>> predicate)
        {
            return _mucLucRepository.FirstOrDefault(predicate);
        }
    }
}
