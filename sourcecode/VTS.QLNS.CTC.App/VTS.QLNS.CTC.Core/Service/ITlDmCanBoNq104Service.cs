using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCanBoNq104Service
    {
        int Add(TlDmCanBoNq104 entity);
        int Update(TlDmCanBoNq104 entity);
        int Delete(Guid id);
        int AddRange(IEnumerable<TlDmCanBoNq104> entities);
        int UpdateRange(List<TlDmCanBoNq104> entities);
        void BulkInsert(IEnumerable<TlDmCanBoNq104> entities);
        void BulkUpdate(List<TlDmCanBoNq104> entities);
        void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps);
        void InsertMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps);
        void Copy(IEnumerable<TlDmCanBoNq104> entities, IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        void InsertMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        void Copy(IEnumerable<TlDmCanBoNq104> entities, IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        TlDmCanBoNq104 FindByMaCanBo(string maCanBo);
        IEnumerable<TlDmCanBoNq104> FindAll(Expression<Func<TlDmCanBoNq104, bool>> predicate);
        IEnumerable<TlDmCanBoNq104> FindAll();
        IEnumerable<TlDmCanBoNq104> FindAllInCludeDelete();
        TlDmCanBoNq104 Find(params object[] keyValues);
        IEnumerable<TlDmCanBoNq104> FindByConditionInsurance(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoNq104> FindByCondition(Expression<Func<TlDmCanBoNq104, bool>> predicate);
        IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam);
        TlDmCanBoNq104 FindById(Guid id);
        IEnumerable<TlDmCanBoNq104> FindAllState();
        IEnumerable<TlDmCanBoNq104> FindLoadIndex();
        IEnumerable<TlDmCanBoNq104> FindAllCanBo();
        IEnumerable<TlDmCanBoNq104> FindCanBoXoa();
        IEnumerable<TlDmCanBoNq104Query> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq);
        IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false);
        IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false);
        DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM);
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi);
        TlDmCanBoNq104 FindByMaHieuCanbo(string maHieuCanBo);
        void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps, List<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges);
    }
}
