using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtChuyenQuyetToanRepository : IRepository<NhQtChuyenQuyetToan>
    {
        void SaveNhQtChuyenQuyetToanChiTiet(List<NhQtChuyenQuyetToanChiTiet> entities, Guid nhQtChuyenQuyetToanId);
        void Save(NhQtChuyenQuyetToan entity);
        IEnumerable<NhQtChuyenQuyetToanQuery> FindIndex();
        bool CheckExistsCQTByTimeAndDonvi(Guid idCQT, Guid iID_DonViID, int loaiThoiGian, int thoiGian);
    }
}
