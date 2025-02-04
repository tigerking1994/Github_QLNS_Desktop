using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktChungTuRepository : Repository<NsSktChungTu>, ISktChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private const string _idDonViDuPhong = "999";

        public SktChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsSktChungTu FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktChungTus.FirstOrDefault(x => x.Id == id);
            }
        }

        public int GetSoChungTuIndexByCondition(string loai, int namLamViec, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.NsSktChungTus.Where(x => loai.Contains(x.ILoai.ToString()) && x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach).OrderByDescending(n => n.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    if (loai == DemandCheckType.DEMAND3Y.ToString())
                    {
                        var indexString = result.FirstOrDefault().SSoChungTu.Substring(6, 3);
                        var index = int.Parse(indexString) + 1;
                        return index;
                    } else
                    {
                        var indexString = result.FirstOrDefault().SSoChungTu.Substring(4, 3);
                        var index = int.Parse(indexString) + 1;
                        return index;
                    }
                }
                catch (Exception e)
                {
                    return result.Count + 1;
                }
            }
        }

        private double SumByIdDonVi(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string idDonVi, string currentUserDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsSktChungTuChiTiet> sktChungTuChiTiets;
                if (idDonVi == currentUserDonVi)
                {
                    sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.Where(x =>
                            x.ILoai == iLoai && x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.IIdMaDonVi == idDonVi)
                        .ToList();
                }
                else
                {
                    sktChungTuChiTiets = ctx.NsSktChungTuChiTiets
                        .Where(x => x.ILoai == iLoai && x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.IIdMaDonVi == idDonVi).ToList();
                }

                var temp = from sktChungTuChiTiet in sktChungTuChiTiets
                           group sktChungTuChiTiet by 1
                    into og
                           select new
                           {
                               TongTuChi = og.Sum(x => x.FTuChi)
                           };
                var result = temp.FirstOrDefault();

                return result?.TongTuChi ?? 0;
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> ListChungTuChiTietDistribution(int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string idDonVi,
            Guid idChungTu, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                var isByNhom = 1;
                if (nsDonVi.Loai == "0" || loaiChungTu == 1 || _idDonViDuPhong.Equals(nsDonVi.IIDMaDonVi))
                {
                    isByNhom = 0;
                }

                int iLoaiCt = -1;
                if (nsDonVi.Loai == "0")
                {
                    iLoaiCt = 3;
                }
                else
                {
                    iLoaiCt = 4;
                }

                var sktChungTuChiTiets = GetSktChungTuChiTietByLoaiChungTu(iLoaiCt, namLamViec, namNganSach, nguonNganSach, iTrangThai,
                    idChungTu.ToString(), idDonVi, loaiChungTu, isByNhom);
                return sktChungTuChiTiets.ToList();
            }
        }

        private double SumByIdDonVi(IEnumerable<NsSktChungTuChiTiet> sktChungTuChiTiets, int loaiTongCanLay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var temp = from sktChungTuChiTiet in sktChungTuChiTiets
                           group sktChungTuChiTiet by 1
                into og
                           select new
                           {
                               TongTuChi = og.Sum(x => x.FTuChi),
                               TongHangMuaHienVat = og.Sum(x => x.FMuaHangCapHienVat),
                               TongDacThu = og.Sum(x => x.FPhanCap)
                           };
                var result = temp.FirstOrDefault();
                if (loaiTongCanLay == 1)
                {
                    return result?.TongTuChi ?? 0;
                }
                else if (loaiTongCanLay == 5)
                {
                    return result?.TongHangMuaHienVat ?? 0;
                }
                else
                {
                    return result?.TongDacThu ?? 0;
                }
            }
        }

        public IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentDonVi = ctx.NsDonVis.FirstOrDefault(i =>
                    i.NamLamViec == namLamViec && i.ITrangThai == 1 && i.IIDMaDonVi == currentIdDonVi);
                var nsDonVis = ctx.NsDonVis.Where(x =>
                        x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai &&
                        (x.IIDMaDonVi == currentIdDonVi || (currentDonVi != null && x.IdParent == currentDonVi.Id)))
                    .ToList();
                var validSktChungTus = ctx.NsSktChungTus
                    .Where(i => i.INamLamViec == namLamViec && i.INamNganSach == namNganSach && i.IIdMaNguonNganSach == nguonNganSach && i.ILoai == iLoai).ToList();
                if (nsDonVis != null)
                {
                    var result = nsDonVis
                                    .GroupJoin(validSktChungTus, nsDonVi => nsDonVi.IIDMaDonVi, sktChungTu => sktChungTu.IIdMaDonVi,
                        (nsDonVi, gj) => new { nsDonVi, gj })
                    .SelectMany(@t => @t.gj.DefaultIfEmpty(), (@t, sub) => new NsSktChungTu()
                    {
                        Id = sub?.Id ?? Guid.NewGuid(),
                        STenDonVi = @t.nsDonVi.TenDonVi,
                        IIdMaDonVi = @t.nsDonVi.IIDMaDonVi,
                        ILoai = iLoai,
                        SMoTa = sub != null ? "Phân bổ số kiểm tra" : null,
                        //tính tổng tự chi nếu có chứng từ hợp lệ
                        FTongTuChi = SumByIdDonVi(iLoai, namLamViec, namNganSach, nguonNganSach, iTrangThai, @t.nsDonVi.IIDMaDonVi, currentIdDonVi),
                        SSoChungTu = sub?.SSoChungTu,
                        DNgayChungTu = sub.DNgayChungTu,
                        SSoQuyetDinh = sub?.SSoQuyetDinh,
                        DNgayQuyetDinh = sub?.DNgayQuyetDinh,
                        SNguoiTao = sub?.SNguoiTao,
                        DNgayTao = sub?.DNgayTao,
                        DNgaySua = sub?.DNgaySua,
                        BKhoa = sub is { BKhoa: true },
                        INamLamViec = namLamViec,
                        ILoaiChungTu = sub?.ILoaiChungTu
                    }).OrderBy(x => x.IIdMaDonVi).ThenBy(x => x.STenDonVi).Select((item, index) =>
                                    {
                                        item.Index = index;
                                        return item;
                                    });
                    return result.ToList();
                }
                return new List<NsSktChungTu>();
            }
        }

        public void LockOrUnLock(string id, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTu = ctx.NsSktChungTus.FirstOrDefault(x => x.Id.ToString() == id);
                chungTu.BKhoa = isLock;
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi, string loaiDV, int loaiChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter typeParam = new SqlParameter("@Type", string.Join(",", new List<string> { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT }));
                List<DonVi> userDonVis = ctx.NsDonVis.FromSql("EXECUTE dbo.sp_ns_dv_get_by_user_and_type @UserName, @YearOfWork, @Type", userNameParam, yearOfWorkParam, typeParam).ToList();
                List<DonVi> nsDonVis = new List<DonVi>();

                if (userDonVis.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    if (loaiChungTu == 1)
                    {
                        nsDonVis = ctx.NsDonVis.Where(x =>
                                x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && (x.Loai == loaiDV || x.Loai == "0"))
                            .ToList();
                    }
                    else
                    {
                        nsDonVis = ctx.NsDonVis.Where(x =>
                                x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && ((true.Equals(x.BCoNSNganh) || _idDonViDuPhong.Equals(x.IIDMaDonVi)) && x.Loai == loaiDV || x.Loai == "0"))
                            .ToList();
                    }
                }
                else
                {
                    if (loaiChungTu == 1)
                        nsDonVis = userDonVis.Where(x => x.Loai == loaiDV || x.Loai == "0").ToList();
                    else
                        nsDonVis = userDonVis.Where(x => (true.Equals(x.BCoNSNganh) || _idDonViDuPhong.Equals(x.IIDMaDonVi)) && x.Loai == loaiDV || x.Loai == "0").ToList();
                }

                //var validSktChungTus = ctx.NsSktChungTus
                //    .Where(i => i.INamLamViec == namLamViec && i.INamNganSach == namNganSach && i.IIdMaNguonNganSach == nguonNganSach && i.ILoai == iLoai && i.ILoaiChungTu == loaiChungTu).ToList();
                //if (!nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT))
                //    validSktChungTus = validSktChungTus.Where(x => x.BKhoa).ToList();
                //var idChungTu = validSktChungTus.Count > 0 ? validSktChungTus[0].Id : Guid.Empty;
                //var soChungTu = validSktChungTus.Count > 0 ? validSktChungTus[0].SSoChungTu : "";
                //var ngayChungTu = validSktChungTus.Count > 0 ? validSktChungTus[0].DNgayChungTu : new DateTime();
                //var checkHasRoot = nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT);
                //if (nsDonVis != null)
                //{
                //    int index = nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT) ? -1 : 0;
                //    var result = nsDonVis
                //        .GroupJoin(validSktChungTus, nsDonVi => nsDonVi.IIDMaDonVi, sktChungTu => sktChungTu.IIdMaDonVi,
                //        (nsDonVi, gj) => new { nsDonVi, gj })
                //    .SelectMany(@t => @t.gj.DefaultIfEmpty(), (@t, sub) =>
                //    {
                //        index = index + 1;
                //        var lstChungTuChiTiet = ListChungTuChiTietDistribution(namLamViec, namNganSach, nguonNganSach, iTrangThai, @t.nsDonVi.IIDMaDonVi,
                //            idChungTu, loaiChungTu);
                //        return new NsSktChungTu()
                //        {
                //            Id = idChungTu != Guid.Empty ? idChungTu : Guid.NewGuid(),
                //            SSoChungTu = soChungTu,
                //            STenDonVi = @t.nsDonVi.TenDonVi,
                //            IIdMaDonVi = @t.nsDonVi.IIDMaDonVi,
                //            ILoai = iLoai,
                //            SMoTa = sub != null ? "Phân bổ số kiểm tra" : null,
                //            //tính tổng tự chi nếu có chứng từ hợp lệ
                //            FTongTuChi = SumByIdDonVi(lstChungTuChiTiet, 1),
                //            FTongMuaHangCapHienVat = SumByIdDonVi(lstChungTuChiTiet, 5),
                //            FTongPhanCap = SumByIdDonVi(lstChungTuChiTiet, 6),
                //            DNgayChungTu = ngayChungTu,
                //            SSoQuyetDinh = sub?.SSoQuyetDinh,
                //            DNgayQuyetDinh = sub?.DNgayQuyetDinh,
                //            SNguoiTao = sub?.SNguoiTao,
                //            DNgayTao = sub?.DNgayTao,
                //            DNgaySua = sub?.DNgaySua,
                //            BKhoa = sub is { BKhoa: true },
                //            INamLamViec = namLamViec,
                //            ILoaiChungTu = sub?.ILoaiChungTu,
                //            Index = index,
                //            iLoaiDV = t.nsDonVi.Loai
                //        };
                //    }).OrderBy(t => t.iLoaiDV).ThenBy(x => x.IIdMaDonVi).ThenBy(x => x.STenDonVi).Select((item, idx) =>
                //    {
                //        item.IndexDonVi = checkHasRoot ? idx : -1;
                //        return item;
                //    }); ;
                //    return result.ToList();
                //}

                var checkHasRoot = nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT);
                List<NsSktChungTu> result = new List<NsSktChungTu>();
                if (nsDonVis != null)
                {
                    int index = nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT) ? -1 : 0;
                    //Lấy danh sách các chứng từ
                    var validSktChungTus = ctx.NsSktChungTus
                    .Where(i => i.INamLamViec == namLamViec && i.INamNganSach == namNganSach && i.IIdMaNguonNganSach == nguonNganSach && i.ILoai == iLoai && i.ILoaiChungTu == loaiChungTu).ToList();
                    if (!nsDonVis.Any(x => x.Loai == LoaiDonVi.ROOT))
                        validSktChungTus = validSktChungTus.Where(x => x.BKhoa).ToList();
                    index = 1;
                    if (validSktChungTus != null)
                    {
                        foreach (var item in validSktChungTus)
                        {
                            item.IndexDonVi = index;
                            item.STenDonVi = nsDonVis.Where(x => x.IIDMaDonVi == item.IIdMaDonVi).FirstOrDefault().TenDonVi;
                            result.Add(item);
                            //Add chứng từ chi tiết
                            var lstSKTCTChiTiet = ctx.NsSktChungTuChiTiets.Where(x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai != 3
                            && x.ILoaiChungTu == loaiChungTu && x.IIdCtsoKiemTra == item.Id).ToList();

                            var lstDonVi = lstSKTCTChiTiet.Select(x => x.IIdMaDonVi).Distinct();
                            if (lstDonVi != null)
                            {
                                foreach (var dv in lstDonVi)
                                {
                                    var lstCtDv = lstSKTCTChiTiet.Where(x => x.IIdMaDonVi == dv).ToList();
                                    var nsDV = nsDonVis.Where(x => x.IIDMaDonVi == item.IIdMaDonVi).ToList();
                                    var dm_dv = nsDonVis.Where(x => x.IIDMaDonVi == dv).ToList();

                                    result.Add(new NsSktChungTu()
                                    {
                                        Id = item != null ? item.Id : Guid.NewGuid(),
                                        SSoChungTu = item.SSoChungTu,
                                        ILoaiNguonNganSach = item.ILoaiNguonNganSach,
                                        STenDonVi = dm_dv?.FirstOrDefault()?.TenDonVi ?? "",
                                        IIdMaDonVi = dv,
                                        ILoai = lstCtDv?.FirstOrDefault()?.ILoai ?? iLoai,
                                        SMoTa = "",
                                        FTongTuChi = lstCtDv.Sum(x => x.FTuChi),
                                        FTongMuaHangCapHienVat = lstCtDv.Sum(x => x.FMuaHangCapHienVat),
                                        FTongPhanCap = lstCtDv.Sum(x => x.FPhanCap),
                                        DNgayChungTu = item.DNgayChungTu,
                                        SSoQuyetDinh = "",
                                        SNguoiTao = "",
                                        DNgayTao = null,
                                        DNgaySua = null,
                                        BKhoa = false,
                                        INamLamViec = namLamViec,
                                        ILoaiChungTu = null,
                                        Index = index,
                                        iLoaiDV = nsDV != null ? dm_dv?.FirstOrDefault()?.Loai ?? "" : ""
                                    });
                                }
                            }
                            index++;
                        }
                    }
                }

                return result;
            }
        }

        public IEnumerable<NsSktChungTu> FindChungTuIndexByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string nganSach, string userName, string proc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (iLoai != 99)
                { 
                    SqlParameter loaiParam = new SqlParameter("@Loai", iLoai);
                    SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                    SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                    SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                    SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                    SqlParameter nganSachParam = new SqlParameter("@NganSach", nganSach);
                    nganSachParam.Value = (object)nganSach ?? DBNull.Value;
                    SqlParameter userNameParam = new SqlParameter("@UserName", userName);

                    return ctx.NsSktChungTus.FromSql(string.Format("EXECUTE {0} @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @NganSach, @UserName", proc),
                        loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam, nganSachParam, userNameParam).ToList();
                }
                else
                {
                    var listSktChungtus = ctx.NsSktChungTus.Where(x => x.ILoai == iLoai && x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach== nguonNganSach).ToList();
                    var listNc3YChungTuChiTiet = ctx.NsNc3YChungTuChiTiets.Where(x => x.ILoai == iLoai && x.INamLamViec == namLamViec && x.INamNganSach == namNganSach);

                    var rs = listSktChungtus.Where(t=> listNc3YChungTuChiTiet.Any(m=>m.IIdCtsoKiemTra == t.Id)).ToList();
                    return rs;
                }
            }
        }

        public IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimize(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, string proc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", iLoai);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);

                return ctx.FromSqlRaw<NsSktChungTuQuery>(string.Format("EXECUTE {0} @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @UserName", proc),
                    loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimizeClone(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", iLoai);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter loaiNguonNganSachParam = new SqlParameter("@loaiNguonNganSach", loaiNguonNganSach);


                return ctx.FromSqlRaw<NsSktChungTuQuery>(string.Format("EXECUTE {0} @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @UserName, @loaiNguonNganSach", proc),
                    loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam, loaiNguonNganSachParam, userNameParam).ToList();
            }
        }

        public IEnumerable<NsSktChungTu> FindChungTuIndexByConditionBVTC(string iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", iLoai);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter loaiNguonNganSachParam = new SqlParameter("@loaiNguonNganSach", loaiNguonNganSach);
                if (proc == "sp_skt_nhan_so_kiem_tra")
                {
                    SqlParameter nganSachParam = new SqlParameter("@NganSach", null);
                    nganSachParam.Value = DBNull.Value;
                    return ctx.NsSktChungTus.FromSql(string.Format("EXECUTE {0} @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @NganSach, @UserName", "sp_skt_nhan_so_kiem_tra"),
                        loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam, nganSachParam, userNameParam).ToList();
                }
                else
                {
                    return ctx.NsSktChungTus.FromSql(string.Format("EXECUTE {0} @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @UserName, @loaiNguonNganSach", proc),
                        loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam, userNameParam, loaiNguonNganSachParam).ToList();
                } 

            }
        }

        public IEnumerable<NsSktChungTuChiTiet> GetSktChungTuChiTietByLoaiChungTu(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string idChungTu, string idDonVi, int loaiChungTu, int isByNhom)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_get_chung_tu_chi_tiet_theo_loai_chung_tu @iLoai, @namLamViec, @namNganSach, @nguonNganSach, @iTrangThai, @idChungTu, @idDonVi, @loaiChungTu, @isByNhom";
                var parameters = new[]
                {
                    new SqlParameter("@iLoai", iLoai),
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@namNganSach", namNganSach),
                    new SqlParameter("@nguonNganSach", nguonNganSach),
                    new SqlParameter("@iTrangThai", iTrangThai),
                    new SqlParameter("@idChungTu", idChungTu),
                    new SqlParameter("@idDonVi", idDonVi),
                    new SqlParameter("@loaiChungTu", loaiChungTu),
                    new SqlParameter("@isByNhom", isByNhom)
                };
                return ctx.Set<NsSktChungTuChiTiet>().FromSql(sql, parameters).ToList();
            }
        }

        public bool IsExistChungTuTongHop(int loai, int namLamViec, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.NsSktChungTus.Any(t => t.IIdMaDonVi.Equals(rootDonVi) && t.ILoai == loai && t.INamLamViec == namLamViec && t.INamNganSach == namNganSach && t.IIdMaNguonNganSach == nguonNganSach);
            }
        }

        public void DeletePhanBoDuToan(Guid iID_CTSoKiemTra, string iIDDonVi, int iNamLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var sql = "EXECUTE dbo.sp_ns_ldtdn_delete_chungtu @iID_CTSoKiemTra, @iIDDonVi, @iNamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@iID_CTSoKiemTra", iID_CTSoKiemTra),
                    new SqlParameter("@iIDDonVi", iIDDonVi),
                    new SqlParameter("@iNamLamViec", iNamLamViec)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}