using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmDonViNq104Service
    {
        IEnumerable<TlDmDonViNq104> FindAll();
        TlDmDonViNq104 FindByMaDonVi(string maDonVi);
        IEnumerable<TlDmDonViNq104> FindByCondition(Expression<Func<TlDmDonViNq104, bool>> predicate);
        IEnumerable<TlDmDonViNq104> FindByDonViCon(string maDonVi);
        IEnumerable<TlDmDonViNq104> FindAllDonVi();
        IEnumerable<TlDmDonViNq104> FindAllDonViNq104();
        IEnumerable<TlDmDonViNq104> FindDonViBaoCaoQuanSo(int nam);
        int AddRange(IEnumerable<TlDmDonViNq104> TlDmDonViNq104s);
        IEnumerable<TlDmDonViNq104> FindAllDonViBaoCao();
        IEnumerable<TlDmDonViNq104> FindAllDonViBaoCaoNq104();
        int UpdateRange(List<TlDmDonViNq104> entities);
        IEnumerable<TlDmDonViNq104> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonViNq104> FindAllDonViQuanSo(int? thang, int nam);
        IEnumerable<TlDmDonViNq104> FindAllDonViQuanSoNam(int nam);
        IEnumerable<TlDmDonViNq104> FindDonViBangLuongThang(int thang, int nam, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonViNq104> FindDonViPhuCap(int thang, int nam, string cachTinhLuong, bool isNew = false);
        TlDmDonViNq104 FirstOrDefault(Expression<Func<TlDmDonViNq104, bool>> predicate);
    }
}
