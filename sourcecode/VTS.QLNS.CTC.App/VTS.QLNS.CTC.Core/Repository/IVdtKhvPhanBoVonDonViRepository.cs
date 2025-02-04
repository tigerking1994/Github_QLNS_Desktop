using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvPhanBoVonDonViRepository : IRepository<VdtKhvPhanBoVonDonVi>
    {
        IEnumerable<VdtKhvPhanBoVonDonViQuery> GetDataPhanBoVonDonViIndexView();
        bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonVi objPhanBoVon);
        bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonVi objPhanBoVon);
        IEnumerable<VdtKhvPhanBoVonDonVi> GetPhanBoVonByListId(List<Guid> lstId, int yearPlan);
        VdtKhvPhanBoVonDonVi FindAggregateVoucher(string sTongHop);
    }
}
