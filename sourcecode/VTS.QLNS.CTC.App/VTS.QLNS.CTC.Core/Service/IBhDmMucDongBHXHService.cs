using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmMucDongBHXHService
    {
        void Add(BhDmMucDongBHXH entity);
        void Update(BhDmMucDongBHXH entity);
        void Delete(BhDmMucDongBHXH entity);
        void AddRange(IEnumerable<BhDmMucDongBHXH> entities);
        void UpdateRange(IEnumerable<BhDmMucDongBHXH> entities);
        IEnumerable<BhDmMucDongBHXH> FindAll();
        BhDmMucDongBHXH FindById(Guid id);
        BhDmMucDongBHXH FindByParentId(Guid id);
    }
}
