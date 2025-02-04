using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhHdnkCacQuyetDinhRepository : IRepository<NhHdnkCacQuyetDinh>
    {
        public IEnumerable<NhQuyetDinhDamPhamQuery> FindByCondition(int namLamViec);
        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChiILoaiQuyetDinh(Guid idNhiemVuChi, int iLoaiQuyetDinh);
        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChi(Guid idNhiemVuChi);
        void DeleteQuyetDinh(Guid id, Guid? idParentId);
        IEnumerable<NhCacQuyetDinhNhiemVuChiQuery> FindByNhiemVuChi(Guid idNhiemVuChi, int iLoaiQuyetDinh, Guid idKhTongThe);
    }
}
