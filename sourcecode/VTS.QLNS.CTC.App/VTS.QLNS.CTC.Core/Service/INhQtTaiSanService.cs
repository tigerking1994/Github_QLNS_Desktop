using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtTaiSanService
    {
        IEnumerable<NhQtTaiSan> FindAll();
        IEnumerable<NhQtTaiSanQuery> FindAllThongKeTaiSan();
        NhQtTaiSan FindById(Guid? id);
        void Update(NhQtTaiSan nhQtTaiSan);
        void Add(NhQtTaiSan nhQtTaiSan);
        void Delete(Guid id);
        IEnumerable<NhQtTaiSanQuery> FindByTaiSanByIdChungTu(Guid idChungTuTaiSan);
    }
}
