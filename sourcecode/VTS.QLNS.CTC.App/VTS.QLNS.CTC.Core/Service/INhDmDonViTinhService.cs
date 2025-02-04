using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmDonViTinhService
    {
        IEnumerable<NhDmDonViTinh> FindAll();
        NhDmDonViTinh FindById(Guid? id);
        void Update(NhDmDonViTinh nhDmDonViTinh);
        void Add(NhDmDonViTinh nhDmDonViTinh);
    }
}
