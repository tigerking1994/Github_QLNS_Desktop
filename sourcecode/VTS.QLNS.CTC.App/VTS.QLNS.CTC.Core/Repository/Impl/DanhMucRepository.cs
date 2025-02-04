using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DanhMucRepository : Repository<DanhMuc>, IDanhMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DanhMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int countDanhMucByTypeAndNLV(string type, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(t => type.Equals(t.SType) && namLamViec == t.INamLamViec).Count();
            }
        }

        public IEnumerable<DanhMuc> FindDanhMucTheoNganh(string idChungTu, int namLamViec, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter();
                idChungTuParam.ParameterName = "@ChungTuId";
                idChungTuParam.DbType = DbType.String;
                idChungTuParam.Value = idChungTu;
                idChungTuParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDate = new SqlParameter();
                voucherDate.ParameterName = "@NamLamViec";
                voucherDate.DbType = DbType.Int32;
                voucherDate.Value = namLamViec;
                voucherDate.Direction = ParameterDirection.Input;

                SqlParameter typeParam = new SqlParameter();
                typeParam.ParameterName = "@Type";
                typeParam.DbType = DbType.String;
                typeParam.Value = type;
                typeParam.Direction = ParameterDirection.Input;

                return ctx.Set<DanhMuc>().FromSql("EXECUTE sp_dt_rpt_phanbo_theonganh_chon_nganh @ChungTuId, @NamLamViec," +
                    "@Type", idChungTuParam, voucherDate, typeParam).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindByType(string type, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(n => n.SType == type && n.INamLamViec == namLamViec && n.ITrangThai == ItrangThaiStatus.ON).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindChuKyChucDanh()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => DanhMucChuKy.CHUC_DANH.Equals(i.SType)).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindNhomChuKyChucDanh()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => DanhMucChuKy.NHOM_CHUC_DANH.Equals(i.SType)).ToList();
            }
        }

        public string FindDonViQuanLy(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DanhMuc item = ctx.DanhMucs.Where(i => i.IIDMaDanhMuc == DanhMucChuKy.DV_QUANLY && i.INamLamViec == namLamViec).FirstOrDefault();
                return item != null ? item.SGiaTri : string.Empty;
            }
        }

        public IEnumerable<DanhMuc> FindChuKyTen()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => DanhMucChuKy.TEN.Equals(i.SType)).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindNhomChuKyTen()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => DanhMucChuKy.NHOM_TEN.Equals(i.SType)).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindChuKyTieuDe1()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => i.IIDMaDanhMuc.StartsWith(DanhMucChuKy.TIEU_DE_1) && !DanhMucChuKy.TIEU_DE_2.Equals(i.IIDMaDanhMuc)).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindChuKyTieuDe2()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => i.IIDMaDanhMuc.StartsWith(DanhMucChuKy.TIEU_DE_2) && !DanhMucChuKy.TIEU_DE_2.Equals(i.IIDMaDanhMuc)).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindNhomChuKy(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(i => i.SType.Equals(DanhMucChuKy.CHU_KY_GROUP) && year == i.INamLamViec).ToList();
            }
        }

        public DanhMuc FindByCode(string idCode, int? namLamViec = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(x => x.IIDMaDanhMuc == idCode && x.INamLamViec == namLamViec).FirstOrDefault();
            }
        }

        public IEnumerable<DanhMuc> FindByType(string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(n => n.SType == type && n.ITrangThai == ItrangThaiStatus.ON).ToList();
            }
        }

        public IEnumerable<DanhMuc> FindDmChuyenNganhByNsDonvi(IEnumerable<Guid> excludeIds, int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.DanhMucs.Where(dm => dm.SType.Equals("NS_Nganh") && dm.INamLamViec == year && !excludeIds.Contains(dm.Id)).ToList();
                IEnumerable<DonVi> donvis = ctx.NsDonVis.Where(d => year == d.NamLamViec).ToList();
                foreach (DanhMuc n in result)
                {
                    if (n.SGiaTri != null)
                    {
                        DonVi d = donvis.FirstOrDefault(p => p.IIDMaDonVi.Equals(n.SGiaTri));
                        n.TenDonViNoiBo = d == null ? null : d.TenDonVi;
                    }
                }
                return result.ToList();
            }
        }

        public void RemoveDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string donvi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (DanhMuc danhMuc in entities)
                {
                    var listDonVi = danhMuc.SGiaTri.Split(StringUtils.COMMA).Where(x => x != string.Empty).ToHashSet();
                    listDonVi.Remove(donvi);
                    danhMuc.SGiaTri = string.Join(StringUtils.COMMA, listDonVi);
                }
                ctx.UpdateRange(entities);
                ctx.SaveChanges();
            }
        }

        public void AddDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string donvi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (DanhMuc danhMuc in entities)
                {
                    if (danhMuc.SGiaTri is null)
                    {
                        danhMuc.SGiaTri = donvi;
                    } else
                    {
                        var listDonVi = danhMuc.SGiaTri.Split(StringUtils.COMMA).Where(x => x != string.Empty).ToHashSet();
                        listDonVi.Add(donvi);
                        danhMuc.SGiaTri = string.Join(StringUtils.COMMA, listDonVi);
                    }
                }
                ctx.UpdateRange(entities);
                ctx.SaveChanges();
            }
        }

        public void UpdateDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string donvi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (DanhMuc danhMuc in entities)
                {
                    danhMuc.SGiaTri = donvi;
                }
                ctx.UpdateRange(entities);
                ctx.SaveChanges();
            }
        }

        public int CountDmCauHinhHeThongByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DanhMucs.Where(d => "DM_CauHinh".Equals(d.SType) && namLamViec == d.INamLamViec).Count();
            }
        }

        public int CountDanhMucNganhChuyenNganh(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sGiaTri = ctx.DanhMucs.Where(d => "NS_Nganh_Nganh".Equals(d.SType) && year == d.INamLamViec)
                    .Select(c => c.SGiaTri).ToList();
                int sum = 0;
                sGiaTri.ForEach(item =>
                {
                    sum += item.Trim().Split(",").Length;
                });

                return sum;
            }
        }
    }
}
