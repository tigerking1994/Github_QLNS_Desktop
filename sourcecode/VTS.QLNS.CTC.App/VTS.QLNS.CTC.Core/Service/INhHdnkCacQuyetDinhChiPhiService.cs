using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhHdnkCacQuyetDinhChiPhiService
    {
        void AddRange(List<NhHdnkCacQuyetDinhChiPhi> listQuyetDinhChiPhi);
        void Delete(Guid id);
        void Update(NhHdnkCacQuyetDinhChiPhi quyetDinhChiPhi);
        NhHdnkCacQuyetDinhChiPhi FindById(Guid Id);
        IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindByIdQuyetDinh(Guid? IdQuyetDinh);
        IEnumerable<NhHdnkCacQuyetDinhChiPhiQuery> FindByIdKhttNhiemVuChi(Guid IdKhttNhiemVuChi);
        IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindAll();
        IEnumerable<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery> FindByIdQuyetDinhGoiThau(Guid? idQuyetDinh);
    }
}
