using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtChuyenQuyetToanService
    {
        void SaveNhQtChuyenQuyetToanChiTiet(List<NhQtChuyenQuyetToanChiTiet> entities, Guid nhQtChuyenQuyetToanId);
        void Save(NhQtChuyenQuyetToan entity);
        void Add(NhQtChuyenQuyetToan nhQtChuyenQuyetToan);
        void Update(NhQtChuyenQuyetToan nhQtChuyenQuyetToan);
        void Delete(Guid id);
        NhQtChuyenQuyetToan FindById(Guid id);
        IEnumerable<NhQtChuyenQuyetToanQuery> FindIndex();
        IEnumerable<NhQtChuyenQuyetToan> FindAll();
        IEnumerable<NhQtChuyenQuyetToan> FindAll(Expression<Func<NhQtChuyenQuyetToan, bool>> predicate);
        bool CheckExistsCQTByTimeAndDonvi(Guid idCQT, Guid iID_DonViID, int loaiThoiGian, int thoiGian);
    }
}
