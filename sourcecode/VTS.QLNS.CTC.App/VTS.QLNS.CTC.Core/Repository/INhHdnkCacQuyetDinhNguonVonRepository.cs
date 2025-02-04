using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhHdnkCacQuyetDinhNguonVonRepository : IRepository<NhHdnkCacQuyetDinhNguonVon>
    {
        
        IEnumerable<NhThongTinNGuonVonQuery> FindByThongTinNguonVon(Guid idQuyetDinh);
        IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindByIdQuyetDinh(Guid? idQuyetDinh);
        IEnumerable<NhThongTinNGuonVonQuery> FindByIdKhttNhiemVuChi(Guid idKhttNhiemVuChi);
    }
}
