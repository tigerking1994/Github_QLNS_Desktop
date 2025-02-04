using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhKhChiTietHopDongService
    {
        void AddRange(List<NhKhChiTietHopDong> listChungTuChiTiet);

        IEnumerable<NhKhChiTietHopDong> FindChiTietHopDongByKHCT(Guid IdKHChiTiet);

        void Delete(Guid id);

        void Update(NhKhChiTietHopDong nhKhChiTietHopDong);

        NhKhChiTietHopDong FindById(Guid Id);

        void DeleteByIdKhChiTiet(Guid idKhChiTiet);
    }
}
