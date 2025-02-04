using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmXuatXuService
    {
        IEnumerable<NhDmXuatXu> FindAll();
        NhDmXuatXu FindById(Guid? id);
        void Update(NhDmXuatXu nhDmXuatXu);
        void Add(NhDmXuatXu nhDmXuatXu);
    }
}
