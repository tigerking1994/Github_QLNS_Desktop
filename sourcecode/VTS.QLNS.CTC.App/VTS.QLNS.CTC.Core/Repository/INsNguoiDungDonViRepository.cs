using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsNguoiDungDonViRepository : IRepository<NguoiDungDonVi>
    {
        bool CheckParentAgencyByUser(string userName, int yearOfWork);
        void RemoveListNguoiDungDonvi(IEnumerable<NguoiDungDonVi> entities);
    }
}