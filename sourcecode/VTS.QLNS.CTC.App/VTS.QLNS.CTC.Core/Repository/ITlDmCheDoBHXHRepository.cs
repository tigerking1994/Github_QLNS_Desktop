using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCheDoBHXHRepository : IRepository<TlDmCheDoBHXH>
    {
        bool CheckPhuCapExist(string maCheDo, Guid iId);
        public TlDmCheDoBHXH GetCheDoBHXHByMaCheDo(string maCheDo);
        IEnumerable<TlDmCheDoBHXH> GetAllCheDoBHXH();
        IEnumerable<TlDmCheDoBHXHQuery> GetCheDoBHXHMapping();
        IEnumerable<TlDmCheDoBHXH> GetCheDoByParent(string maCheDoCha);
        TlDmCachTinhLuongBaoHiem GetCachTinhLuong(string congThuc);
        TlDmCachTinhLuongBaoHiemNq104 GetCachTinhLuongNq104(string congThuc);
    }
}
