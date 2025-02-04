using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhHdnkCacQuyetDinhService
    {
        IEnumerable<NhQuyetDinhDamPhamQuery> FindByCondition(int iLoai);
        IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChiILoaiQuyetDinh(Guid idNhiemVuChi, int iLoaiQuyetDinh);
        IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChi(Guid idNhiemVuChi);
        void LockOrUnlock(Guid id, bool BIsKhoa);
        NhHdnkCacQuyetDinh FindById(Guid? id);
        void Update(NhHdnkCacQuyetDinh entity);
        void DeleteQuyetDinh(Guid id, Guid? idParentId);
        void Add(NhHdnkCacQuyetDinh entity);
        void Delete(Guid id);
        int Adjust(NhHdnkCacQuyetDinh entity);
        IEnumerable<NhCacQuyetDinhNhiemVuChiQuery> FindByNhiemVuChi(Guid idNhiemVuChi, int iLoaiQuyetDinh, Guid idKhTongThe);
    }
}
