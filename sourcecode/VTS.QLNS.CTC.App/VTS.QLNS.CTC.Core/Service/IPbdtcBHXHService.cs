using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IPbdtcBHXHService
    {
        IEnumerable<BhPbdtcBHXH> FindByCondition(Expression<Func<BhPbdtcBHXH, bool>> predicate);
        BhPbdtcBHXH FindById(Guid Id);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int Add(BhPbdtcBHXH item);
        int Update(BhPbdtcBHXH item);
        int Delete(BhPbdtcBHXH item);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhPbdtcBHXH> FindDotNhanByChungTuPhanBo(Guid idPhanBo);
        IEnumerable<BhPbdtcBHXH> FindBySoQuyetDinh(string soQuyetDinh, int nam);
        IEnumerable<BhPbdtcBHXH> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam);
        List<DonVi> FindByDonViForNamLamViec(int namLamViec, Guid? IDLoaiChi, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh, int dotNhan);
        List<DonVi> FindByDonViForInLuyKeNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi,string sSoQuyetDinh, string sNgayQuyetDinh,int dotNhan);
        IEnumerable<BhPbdtcBHXhQuery> GetDanhSachPhanBoDuToanChi(int iNamLamViec);
        List<DonVi> FindByDonViForNamLamViecNormal(int yearOfWork, Guid id, string sMaLoaiChi, string lstIUChungTu, int dotNhan);
        List<DonVi> FindByDonViForInDotNgayNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh,int dotNhan);

        IEnumerable<BhPbdtcBHXH> FindByConditionLoaiChi(int yearOfWork,int dotNhan,string sMaLoaiChi);
    }
}
