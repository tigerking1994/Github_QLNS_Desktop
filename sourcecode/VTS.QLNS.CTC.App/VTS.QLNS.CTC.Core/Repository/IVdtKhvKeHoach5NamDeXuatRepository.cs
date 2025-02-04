using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoach5NamDeXuatRepository : IRepository<VdtKhvKeHoach5NamDeXuat>
    {
        IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionIndex(int yearOfWork);
        IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionAll();
        IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen);
        VdtKhvKeHoach5NamDeXuat FindAggregateVoucher(string sTongHop);
        bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
    }
}
