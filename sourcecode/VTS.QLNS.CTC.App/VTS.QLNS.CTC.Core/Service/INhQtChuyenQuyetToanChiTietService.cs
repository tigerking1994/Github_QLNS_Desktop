using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtChuyenQuyetToanChiTietService
    {
        void Save(NhQtChuyenQuyetToanChiTiet entity);
        void Add(NhQtChuyenQuyetToanChiTiet nhQtChuyenQuyetToanChiTiet);
        void Update(NhQtChuyenQuyetToanChiTiet nhQtChuyenQuyetToanChiTiet);
        void Delete(Guid id);
        void DeleteByChuyenQuyetToanId(Guid id);
        NhQtChuyenQuyetToanChiTiet FindById(Guid id);
        IEnumerable<NhQtChuyenQuyetToanChiTiet> FindAll();
        IEnumerable<NhQtChuyenQuyetToanChiTiet> FindAll(Expression<Func<NhQtChuyenQuyetToanChiTiet, bool>> predicate);
    }
}
