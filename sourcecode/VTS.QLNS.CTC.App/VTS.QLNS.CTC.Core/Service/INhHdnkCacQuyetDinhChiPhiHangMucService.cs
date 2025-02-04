using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhHdnkCacQuyetDinhChiPhiHangMucService
    {
        void AddRange(List<NhHdnkCacQuyetDinhChiPhiHangMuc> listQuyetDinhChiPhiHangMuc);
        void Delete(Guid id);
        void Update(NhHdnkCacQuyetDinhChiPhiHangMuc QuyetDinhChiPhiHangMuc);
        NhHdnkCacQuyetDinhChiPhiHangMuc FindById(Guid Id);
        IEnumerable<NhHdnkCacQuyetDinhChiPhiHangMuc> FindByIdQuyetDinhChiPhi(Guid IdQuyetDinhChiPhi);
        void DeleteByQuyetDinhChiPhi(Guid IdQuyetDinhChiPhi);
    }
}
