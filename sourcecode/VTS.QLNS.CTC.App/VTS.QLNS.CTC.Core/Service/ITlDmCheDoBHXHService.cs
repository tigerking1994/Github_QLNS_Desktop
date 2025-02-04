using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCheDoBHXHService
    {
        IEnumerable<TlDmCheDoBHXH> FindAll();
        public TlDmCheDoBHXH GetCheDoBHXHByMaCheDo(string maCheDo);
        IEnumerable<TlDmCheDoBHXH> GetAllCheDoBHXH();
        int UpdateCheDoBHXH(TlDmCheDoBHXH entity);
        IEnumerable<TlDmCheDoBHXHQuery> GetCheDoBHXHMapping();
        IEnumerable<TlDmCheDoBHXH> GetCheDoByParent(string maCheDoCha);
        TlDmCachTinhLuongBaoHiem GetCachTinhLuong(string congThuc);
        TlDmCachTinhLuongBaoHiemNq104 GetCachTinhLuongNq104(string congThuc);
    }
}
