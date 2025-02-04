using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBaoCaoService
    {
        IEnumerable<TlBaoCao> FindAll();
        IEnumerable<TlBaoCao> FindByCondition(Expression<Func<TlBaoCao, bool>> predicate);
        TlBaoCao Find(Guid id);
        IEnumerable<ExportLuongNgachCanBoQuery> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongChiTietNgachCanBoQuery> FindLuongChiTiet(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL);
        void UpdateBaoCao(List<TlBaoCao> entities);
        int Add(TlBaoCao entity);
        int Delete(TlBaoCao item);
        int Update(TlBaoCao item);
    }
}
