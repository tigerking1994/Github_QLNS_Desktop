using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IHtTableMigrateService
    {
        IEnumerable<HtTableMigrate> FindAllByToday();
        void RemoveByToday();
        void AddRange(IEnumerable<HtTableMigrate> entities);
        IEnumerable<HtTableMigrate> FindAll(Expression<Func<HtTableMigrate, bool>> predicate);
        IEnumerable<HtTableMigrate> FindAll();
        void Add(HtTableMigrate entity);
        void Update(HtTableMigrate entity);
        HtTableMigrate FindById(Guid id);
    }
}
