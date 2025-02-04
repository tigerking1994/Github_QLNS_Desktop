using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IPbdttmBHYTRepository : IRepository<BhPbdttmBHYT>
    {
        IEnumerable<BhPbdttmBHYT> FindByCondition(Expression<Func<BhPbdttmBHYT, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhPbdttmBHYT> FindDotNhanByChungTuPhanBo(Guid idPhanBo);
        IEnumerable<BhPbdttmBHYT> FindBySoQuyetDinh(string soQuyetDinh, int nam);
        IEnumerable<BhPbdttmBHYT> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam);
        IEnumerable<BhPbdttmBHYT> FindBySoChungTu(string soChungTu, int nam);
        IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTM(int year, bool isInTheoChungTu);
        IEnumerable<BhPbdttmBHYT> FindByIdNhanDuToan(string id);
    }
}
