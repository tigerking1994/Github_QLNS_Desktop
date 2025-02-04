using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoach5NamRepository : IRepository<VdtKhvKeHoach5Nam>
    {
        public IEnumerable<VdtKhvKeHoach5NamQuery> FindConditionIndex(int yearOfWork);
        public IEnumerable<VdtKhvKeHoach5Nam> FindByDonViQuanLy(MediumTermPlanIndexSearch condition);
        //IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonVi(Guid id);
        IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen);
        public Guid? FindIdKHTHByID(Guid? id);

        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
        public IEnumerable<VdtKhvKeHoach5NamQuery> FindAllDuocDuyet();
    }
}
