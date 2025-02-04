using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtQuyetToanDahtRepository : Repository<NhQtQuyetToanDaht>, INhQtQuyetToanDahtRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtQuyetToanDahtRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<NhQtQuyetToanDaht> FindAllByNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhQtQuyetToanDaht.Where(t => !t.BTongHop.HasValue || !t.BTongHop.Value).ToList();
            }
        }

        public List<NhQtQuyetToanDaht> FindAllNhQtDaht()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhQtQuyetToanDaht.ToList();
            }
        }       

        public List<NhQtQuyetToanDaht> FindAllTongHopByNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhQtQuyetToanDaht.Where(t => t.BTongHop == true).ToList();
            }
        }

        public void Save(NhQtQuyetToanDaht entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhQtQuyetToanDaht.Any(t => t.Id.Equals(entity.Id));
                if (exist)
                    ctx.Update(entity);
                else
                    ctx.Add(entity);
                ctx.SaveChanges();
            }
        }

        public void SaveNhQtQuyetToanDahtChiTiet(List<NhQtQuyetToanDahtChiTiet> entities, Guid nhQtQuyetToanDahtId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var existChiTiet = ctx.NhQtQuyetToanDahtChiTiet.Where(t => t.IIdDeNghiQuyetToanDahtId.Equals(nhQtQuyetToanDahtId)).ToList();
                ctx.NhQtQuyetToanDahtChiTiet.RemoveRange(existChiTiet);
                ctx.NhQtQuyetToanDahtChiTiet.AddRange(entities);
                NhQtQuyetToanDaht nhQtQuyetToanDaht = ctx.NhQtQuyetToanDaht.FirstOrDefault(t => t.Id.Equals(nhQtQuyetToanDahtId));
                nhQtQuyetToanDaht.FDeNghiQuyetToanEur = entities.Where(t => t.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP).Sum(t => t.FDeNghiQuyetToanEur);
                nhQtQuyetToanDaht.FDeNghiQuyetToanUsd = entities.Where(t => t.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP).Sum(t => t.FDeNghiQuyetToanUsd);
                nhQtQuyetToanDaht.FDeNghiQuyetToanVnd = entities.Where(t => t.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP).Sum(t => t.FDeNghiQuyetToanVnd);
                nhQtQuyetToanDaht.FDeNghiQuyetToanNgoaiTeKhac = entities.Where(t => t.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP).Sum(t => t.FDeNghiQuyetToanNgoaiTeKhac);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NHDAQDDauTuChiPhiHangMuc> GetChiPhiHangMucByDuAnId(Guid duAnId, Guid DeNghiQTDAHTId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"WITH tmp AS (
                                SELECT t1.ID, t1.sTenChiPhi AS sTen, t1.sMaOrder AS SOrder, t1.iID_ParentID AS ParentId, null AS ChiPhiId, 1 AS IType,
                                t1.fGiaTriUSD AS USDDT, t1.fGiaTriVND AS VNDDT, t1.fGiaTriEUR AS EURODT, t1.fGiaTriNgoaiTeKhac AS NgoaiTeDT, 
                                CAST(CONCAT('/', replace(dbo.fn_RemoveLeadingZero(t1.sMaOrder, '-'), '-', '/'), '/') as hierarchyid) AS HierarchyStr
                                FROM NH_DA_QDDauTu_ChiPhi t1
                                JOIN NH_DA_QDDauTu t2 ON t1.iID_QDDauTuID = t2.ID
                                WHERE t2.iID_DuAnID = @DuAnId)
	                            SELECT tbl1.*,
	                            tbl2.fGiaTriQuyetToanAB_EUR AS EUROQT,
	                            tbl2.fGiaTriQuyetToanAB_NgoaiTeKhac AS NgoaiTeQT,
	                            tbl2.fGiaTriQuyetToanAB_USD AS USDQT,
	                            tbl2.fGiaTriQuyetToanAB_VND AS VNDQT,
	                            tbl2.fKetQuaKiemToan_EUR AS EUROKT,
	                            tbl2.fKetQuaKiemToan_NgoaiTeKhac AS NgoaiTeKT,
	                            tbl2.fKetQuaKiemToan_USD AS USDKT,
	                            tbl2.fKetQuaKiemToan_VND AS VNDKT,
	                            tbl2.fDeNghiQuyetToan_EUR AS EUROCDT,
	                            tbl2.fDeNghiQuyetToan_NgoaiTeKhac AS NgoaiTeCDT,
	                            tbl2.fDeNghiQuyetToan_USD AS USDCDT,
	                            tbl2.fDeNghiQuyetToan_VND AS VNDCDT
	                            FROM 
	                            (
		                            SELECT * FROM tmp
		                            UNION
		                            SELECT t3.ID, t3.STenHangMuc AS sTen, t3.sMaOrder AS SOrder, t3.iID_ParentID AS ParentId, t3.iID_QDDauTu_ChiPhiID AS ChiPhiId, 2 AS IType,
                                    t3.fGiaTriUSD AS USDDT, t3.fGiaTriVND AS VNDDT, t3.fGiaTriEUR AS EURODT, t3.fGiaTriNgoaiTeKhac AS NgoaiTeDT, 
                                    CAST(CONCAT('/', replace(dbo.fn_RemoveLeadingZero(t3.sMaOrder, '-'), '-', '/'), '/') as hierarchyid) AS HierarchyStr
                                    FROM NH_DA_QDDauTu_HangMuc t3
	                                WHERE iID_QDDauTu_ChiPhiID IN (SELECT ID FROM tmp)
	                            ) tbl1 
                                LEFT JOIN NH_QT_QuyetToanDAHT_ChiTiet tbl2 ON tbl1.id = tbl2.iid_HM_CP ";
                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@DuAnId", duAnId)
                };
                if (DeNghiQTDAHTId != Guid.Empty)
                {
                    sql += "WHERE tbl2.iID_DeNghiQuyetToanDAHT_ID = @DeNghiQTDAHTId ";
                    parameters.Add(new SqlParameter("@DeNghiQTDAHTId", DeNghiQTDAHTId));
                }
                sql += "ORDER BY tbl1.IType, tbl1.HierarchyStr";
                return ctx.FromSqlRaw<NHDAQDDauTuChiPhiHangMuc>(sql, parameters.ToArray()).ToList();
            }
        }

        public void LockUnlockItem(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhQtQuyetToanDaht.FirstOrDefault(t => t.Id.Equals(id));
                exist.BIsKhoa = !exist.BIsKhoa;
                ctx.SaveChanges();
            }
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", voucherIds);
                ctx.Database.ExecuteSqlCommand($"UPDATE NH_QT_QuyetToanDAHT SET bTongHop = 0, iID_Parent = null WHERE ID IN (SELECT * FROM f_split(@VoucherIds)) ", voucherIdParam);
            }
        }

        public void TongHopQTDAHT(NhQtQuyetToanDaht nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhQtQuyetToanDaht.Any(t => t.Id.Equals(nhTtDeNghiThanhToan.Id));
                if (exist)
                    ctx.Update(nhTtDeNghiThanhToan);
                else
                    ctx.Add(nhTtDeNghiThanhToan);
                var children = ctx.NhQtQuyetToanDaht.Where(t => voucherAgregatesIds.Contains(t.Id)).ToList();
                foreach (var child in children)
                {
                    child.ParentId = nhTtDeNghiThanhToan.Id;
                    child.BTongHop = true;
                }
                ctx.SaveChanges();
            }
        }

        public new int Delete(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var e = ctx.NhQtQuyetToanDaht.FirstOrDefault(t => t.Id.Equals(id));
                ctx.Remove(e);
                var children = ctx.NhQtQuyetToanDaht.Where(t => id.Equals(t.ParentId)).ToList();
                foreach (var item in children)
                {
                    item.ParentId = null;
                }
                return ctx.SaveChanges();
            }
        }
    }
}
