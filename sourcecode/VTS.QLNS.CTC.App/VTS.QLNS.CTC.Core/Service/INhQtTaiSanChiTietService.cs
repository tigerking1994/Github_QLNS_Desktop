using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtTaiSanChiTietService
    {
        IEnumerable<NhQtTaiSanChiTiet> FindAll();
        NhQtTaiSanChiTiet FindById(Guid? id);
        void Update(NhQtTaiSanChiTiet nhQtTaiSanChiTietService);
        void Add(NhQtTaiSanChiTiet nhQtTaiSanChiTietService);
        void Delete(Guid id);
        IEnumerable<NhQtTaiSanChiTiet> FindByTaiSanChiTietId(Guid idTaiSan);
    }
}
