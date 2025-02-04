using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoCheDoBHXHChiTietService
    {
        IEnumerable<TlCanBoCheDoBHXHChiTietQuery> GetCanBoCheDoChiTietIndex(string maCanBo, string maCheDo, int thang, int nam);
        int AddRangeCBCDChiTiet(IEnumerable<TlCanBoCheDoBHXHChiTiet> entities);
        TlCanBoCheDoBHXHChiTiet FindCBCDChiTiet(params object[] keyValues);
        int UpdateCBCDChiTiet(TlCanBoCheDoBHXHChiTiet entity);
        int DeleteCBCDChiTiet(Guid id);
        TlCanBoCheDoBHXHChiTietQuery GetTongSoNgayHuong(string maCanBo, string maCheDo, int thang, int nam);
        IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTiet(string maCanBo, string maCheDo, int thang, int nam);
        IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTietInactive(int thang, int nam);
        int RemoveRange(IEnumerable<TlCanBoCheDoBHXHChiTiet> items);
        int ExistSoNgayHuong(string sMaCanBo, string sMaCheDo, int? iThang, int? iNam);
    }
}
