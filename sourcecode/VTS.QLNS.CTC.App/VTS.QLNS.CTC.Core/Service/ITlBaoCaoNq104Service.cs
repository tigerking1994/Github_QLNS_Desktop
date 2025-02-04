using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBaoCaoNq104Service
    {
        IEnumerable<TlBaoCao> FindAll();
        IEnumerable<TlBaoCao> FindByCondition(Expression<Func<TlBaoCao, bool>> predicate);
        TlBaoCao Find(Guid id);
        IEnumerable<ExportLuongNgachCanBoNq104Query> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongChiTietNgachCanBoNq104Query> FindLuongChiTiet(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL);
        IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL);
        void UpdateBaoCao(List<TlBaoCao> entities);
        int Add(TlBaoCao entity);
        int Delete(TlBaoCao item);
        int Update(TlBaoCao item);
    }
}
