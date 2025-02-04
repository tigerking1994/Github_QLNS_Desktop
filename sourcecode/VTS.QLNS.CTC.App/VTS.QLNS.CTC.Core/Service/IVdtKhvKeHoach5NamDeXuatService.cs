using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoach5NamDeXuatService
    {
        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionIndex(int yearOfWork);
        int Delete(Guid id);
        int Update(VdtKhvKeHoach5NamDeXuat item);
        VdtKhvKeHoach5NamDeXuat Add(VdtKhvKeHoach5NamDeXuat entity);
        VdtKhvKeHoach5NamDeXuat FindById(Guid id);
        IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByCondition(Expression<Func<VdtKhvKeHoach5NamDeXuat, bool>> predicate);
        IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen);
        int Adjust(VdtKhvKeHoach5NamDeXuat entity, List<VdtKhvKeHoach5NamDeXuatChiTiet> details);
        void LockOrUnlock(Guid id, bool isLock);
        VdtKhvKeHoach5NamDeXuat FindAggregateVoucher(string sTongHop);
        int Agregate(VdtKhvKeHoach5NamDeXuat entity, List<VdtKhvKeHoach5NamDeXuatChiTiet> details, List<Guid> lstVoucher);
        int FindCurrentPeriod(int year);

        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionAll();
    }
}
