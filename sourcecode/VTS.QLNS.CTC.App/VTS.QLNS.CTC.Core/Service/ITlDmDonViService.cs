using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmDonViService
    {
        IEnumerable<TlDmDonVi> FindAll();
        TlDmDonVi FindByMaDonVi(string maDonVi);
        IEnumerable<TlDmDonVi> FindByCondition(Expression<Func<TlDmDonVi, bool>> predicate);
        IEnumerable<TlDmDonVi> FindByDonViCon(string maDonVi);
        IEnumerable<TlDmDonVi> FindAllDonVi();
        IEnumerable<TlDmDonVi> FindAllDonViNq104();
        IEnumerable<TlDmDonVi> FindDonViBaoCaoQuanSo(int nam);
        int AddRange(IEnumerable<TlDmDonVi> tlDmDonVis);
        IEnumerable<TlDmDonVi> FindAllDonViBaoCao();
        IEnumerable<TlDmDonVi> FindAllDonViBaoCaoNq104();
        int UpdateRange(List<TlDmDonVi> entities);
        IEnumerable<TlDmDonVi> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonVi> FindDonViTaoBangLuongBHXH(int nam, int thang, string cachTinhLuong);
        IEnumerable<TlDmDonVi> FindAllDonViQuanSo(int? thang, int nam);
        IEnumerable<TlDmDonVi> FindAllDonViQuanSoNam(int nam);
        IEnumerable<TlDmDonVi> FindDonViBangLuongThang(int thang, int nam, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false);
        IEnumerable<TlDmDonVi> FindDonViPhuCap(int thang, int nam, string cachTinhLuong, bool isNew = false);
        TlDmDonVi FirstOrDefault(Expression<Func<TlDmDonVi, bool>> predicate);
    }
}
