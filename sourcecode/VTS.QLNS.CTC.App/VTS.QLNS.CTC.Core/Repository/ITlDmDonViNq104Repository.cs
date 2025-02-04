using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmDonViNq104Repository : IRepository<TlDmDonViNq104>
    {
        TlDmDonViNq104 FindByMaDonVi(string maDonVi);
        IEnumerable<TlDmDonViNq104> FindAllDonVi();
        IEnumerable<TlDmDonViNq104> FindAllDonViBaoCao();
        IEnumerable<TlDmDonViNq104> FindAllDonViNq104();
        IEnumerable<TlDmDonViNq104> FindAllDonViBaoCaoNq104();
        IEnumerable<TlDmDonViNq104> FindDonViBaoCaoQuanSo(int nam);
        IEnumerable<TlDmDonViNq104> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonViNq104> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh, bool isNew);
        IEnumerable<TlDmDonViNq104> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew);
        IEnumerable<TlDmDonViNq104> FindAllDonViQuanSo(int? thang, int nam);
        IEnumerable<TlDmDonViNq104> FindAllDonViQuanSoNam(int nam);
    }
}
