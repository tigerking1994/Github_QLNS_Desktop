using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlNguoiDungPhanHoRepository : IRepository<NguoiDungPhanHo>
    {
        bool CheckParentAgencyByUser(string userName, int yearOfWork);
        void RemoveListNguoiDungPhanHo(IEnumerable<NguoiDungPhanHo> entities);
    }
}