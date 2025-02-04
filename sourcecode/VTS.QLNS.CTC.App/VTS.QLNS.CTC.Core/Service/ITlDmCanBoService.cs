using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCanBoService
    {
        int Add(TlDmCanBo entity);
        int Update(TlDmCanBo entity);
        int Delete(Guid id);
        int AddRange(IEnumerable<TlDmCanBo> entities);
        int UpdateRange(List<TlDmCanBo> entities);
        void BulkInsert(IEnumerable<TlDmCanBo> entities);
        void BulkUpdate(List<TlDmCanBo> entities);
        void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps);
        void InsertMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps);
        void Copy(IEnumerable<TlDmCanBo> entities, IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps);
        void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        void InsertMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        void Copy(IEnumerable<TlDmCanBo> entities, IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        TlDmCanBo FindByMaCanBo(string maCanBo);
        IEnumerable<TlDmCanBo> FindAll(Expression<Func<TlDmCanBo, bool>> predicate);
        IEnumerable<TlDmCanBo> FindAll();
        IEnumerable<TlDmCanBo> FindAllInCludeDelete();
        TlDmCanBo Find(params object[] keyValues);
        IEnumerable<TlDmCanBo> FindByConditionInsurance(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBo> FindByCondition(Expression<Func<TlDmCanBo, bool>> predicate);
        IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam);
        IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam);
        TlDmCanBo FindById(Guid id);
        IEnumerable<TlDmCanBo> FindAllState();
        IEnumerable<TlDmCanBo> FindLoadIndex();
        IEnumerable<TlDmCanBo> FindAllCanBo();
        IEnumerable<TlDmCanBo> FindCanBoXoa();
        IEnumerable<TlDmCanBoQuery> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq);
        IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false);
        IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false);
        DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM);
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104();
        IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi);
        TlDmCanBo FindByMaHieuCanbo(string maHieuCanBo, string maCb);
        void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps, List<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges);
    }
}
