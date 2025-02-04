using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmDonViRepository : IRepository<TlDmDonVi>
    {
        TlDmDonVi FindByMaDonVi(string maDonVi);
        IEnumerable<TlDmDonVi> FindAllDonVi();
        IEnumerable<TlDmDonVi> FindAllDonViBaoCao();
        IEnumerable<TlDmDonVi> FindAllDonViNq104();
        IEnumerable<TlDmDonVi> FindAllDonViBaoCaoNq104();
        IEnumerable<TlDmDonVi> FindDonViBaoCaoQuanSo(int nam);
        IEnumerable<TlDmDonVi> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonVi> FindDonViTaoBangLuongBHXH(int nam, int thang, string cachTinhLuong);
        IEnumerable<TlDmDonVi> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh, bool isNew);
        IEnumerable<TlDmDonVi> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew);
        IEnumerable<TlDmDonVi> FindAllDonViQuanSo(int? thang, int nam);
        IEnumerable<TlDmDonVi> FindAllDonViQuanSoNam(int nam);
    }
}
