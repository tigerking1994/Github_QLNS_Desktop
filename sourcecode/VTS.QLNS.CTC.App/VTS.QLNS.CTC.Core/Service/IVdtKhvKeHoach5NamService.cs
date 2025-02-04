using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoach5NamService
    {
        IEnumerable<VdtKhvKeHoach5Nam> FindByCondition(Expression<Func<VdtKhvKeHoach5Nam, bool>> predicate);
        IEnumerable<VdtKhvKeHoach5NamQuery> FindConditionIndex(int yearOfWork);
        IEnumerable<VdtKhvKeHoach5NamQuery> FindAllDuocDuyet();
        VdtKhvKeHoach5Nam Add(VdtKhvKeHoach5Nam entity);
        int Adjust(VdtKhvKeHoach5Nam entity);
        int Delete(Guid id);
        VdtKhvKeHoach5Nam FindById(Guid id);
        void LockOrUnlock(Guid id, bool isLock);
        int Update(VdtKhvKeHoach5Nam item);
        IEnumerable<VdtKhvKeHoach5Nam> FindByDonViQuanLy(MediumTermPlanIndexSearch condition);
        IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen);
        Guid? FindIdKHTHByID(Guid? id);

        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
    }
}
