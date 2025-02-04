using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhHdnkCacQuyetDinhNguonVonService
    {
        void AddRange(List<NhHdnkCacQuyetDinhNguonVon> listQuyetDinhNguonVon);
        void Delete(Guid id);
        void Update(NhHdnkCacQuyetDinhNguonVon quyetDinhNguonVon);
        NhHdnkCacQuyetDinhNguonVon FindById(Guid Id);
        IEnumerable<NhThongTinNGuonVonQuery> FindByThongTinNguonVon(Guid idQuyetDinh);
        IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindByIdQuyetDinh(Guid? IdQuyetDinh);
        IEnumerable<NhThongTinNGuonVonQuery> FindByIdKhttNhiemVuChi(Guid idKhttNhiemVuChi);
        IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindAll();
    }
}
