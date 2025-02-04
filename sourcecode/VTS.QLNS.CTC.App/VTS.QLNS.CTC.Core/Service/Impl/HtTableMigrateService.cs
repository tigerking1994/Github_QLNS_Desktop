using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class HtTableMigrateService : IHtTableMigrateService
    {
        private readonly IHtTableMigrateRepository _reposirory;

        public HtTableMigrateService(IHtTableMigrateRepository reposirory)
        {
            _reposirory = reposirory;
        }

        public void AddRange(IEnumerable<HtTableMigrate> entities)
        {
            _reposirory.AddRange(entities);
        }

        public void Add(HtTableMigrate entity)
        {
            _reposirory.Add(entity);
        }

        public void Update(HtTableMigrate entity)
        {
            _reposirory.Update(entity);
        }

        public HtTableMigrate FindById(Guid id)
        {
            return _reposirory.Find(id);
        }
        public IEnumerable<HtTableMigrate> FindAll()
        {
            return _reposirory.FindAll();
        }
        public IEnumerable<HtTableMigrate> FindAll(Expression<Func<HtTableMigrate, bool>> predicate)
        {
            return _reposirory.FindAll(predicate);  
        }
        public IEnumerable<HtTableMigrate> FindAllByToday()
        {
            return _reposirory.FindAll(x => x.CreatedAt.Date == DateTime.Now.Date);
        }
        public void RemoveByToday()
        {
            var data = FindAllByToday();
            _reposirory.RemoveRange(data);
        }

        public void RemoveHtTableMigrates(IEnumerable<HtTableMigrate> HtTableMigrates)
        {
            _reposirory.RemoveRange(HtTableMigrates);
        }

    }
}
