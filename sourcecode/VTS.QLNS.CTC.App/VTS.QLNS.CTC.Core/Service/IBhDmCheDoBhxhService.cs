using System;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmCheDoBhxhService
    {
        void Add(BhDmCheDoBhxh entity);
        void Update(BhDmCheDoBhxh entity);
        void Delete(BhDmCheDoBhxh entity);
        void AddRange(IEnumerable<BhDmCheDoBhxh> entities);
        void UpdateRange(IEnumerable<BhDmCheDoBhxh> entities);
        IEnumerable<BhDmCheDoBhxh> FindAll();
        BhDmCheDoBhxh FindById(Guid id);
        BhDmCheDoBhxh FindByParentId(Guid id);
    }
}
