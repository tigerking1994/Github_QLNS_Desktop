using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhKhChiTietService
    {
        IEnumerable<NhKhChiTiet> FindAll();

        IEnumerable<NhKhChiTietQuery> FindAllNhkHChiTietHasSoKeHoachTTBQP();
        IEnumerable<NhKhChiTietQuery> FindByCondition(int namLamViec);
        void LockOrUnlock(Guid id, bool isKhoa);

        void Add(NhKhChiTiet nhKhChiTiet);
        void Update(NhKhChiTiet nhKhChiTiet);
        NhKhChiTiet FindById(Guid id);
        void Delete(Guid id);
        int Adjust(NhKhChiTiet entity);
    }
}
