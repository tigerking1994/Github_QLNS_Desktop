using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcCapKinhPhiKcbChiTietRepository : Repository<BhQtCapKinhPhiKcbChiTiet>, IQtcCapKinhPhiKcbChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcCapKinhPhiKcbChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool ExistVoucherDetail(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtCapKinhPhiKcbChiTiets.Any(t => t.IIDCTCapKinhPhiKCB.Equals(chungTuId));
            }
        }

        public BhQtCapKinhPhiKcbChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtCapKinhPhiKcbChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindChungTuChiTietByChungTuId(BhQtCapKinhPhiKcbChiTietCriteria searchCondition)
        {
            Guid chungTuId = searchCondition.IIDCTCapKinhPhiKCB;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtCapKinhPhiKcbChiTiets.Where(x => x.IIDCTCapKinhPhiKCB == chungTuId).ToList();
            }
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindVoucherDetailByCondition(BhQtCapKinhPhiKcbChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var namLamViec = searchCondition.NamLamViec;
                var voucherID = searchCondition.IIDCTCapKinhPhiKCB;
                var maCSYT = searchCondition.SMaCSYT;
                var quy = searchCondition.IQuy;
                var loaiKinhPhi = searchCondition.ILoaiKinhPhi;

                var mucLucs = GetListBhMucLucNs(namLamViec, searchCondition.LstLns, maCSYT);
                var lstChungTuChiTiets = FindVoucherDetail(voucherID).ToList();
                var dataQuyetToans = GetDataDaQuyetToan(voucherID, namLamViec, maCSYT);
                var dataKeHoachCaps = GetDataKeHoachCap(namLamViec, quy, maCSYT);
                var dataCapPhatBoSungs = GetDataCapPhatBoSung(namLamViec, quy, maCSYT, loaiKinhPhi);

                var result = from mucLuc in mucLucs
                             join chungTuChiTiet in lstChungTuChiTiets
                             on new { A = mucLuc.IIDMLNS, B = mucLuc.IIDMaCoSoYTe } equals
                                new { A = chungTuChiTiet.IIdMlns, B = chungTuChiTiet.sMaCoSoYTe } into gj
                             from sub in gj.DefaultIfEmpty()
                             join dataKeHoachCap in dataKeHoachCaps
                             on new { A = mucLuc.IIDMLNS, B = mucLuc.IIDMaCoSoYTe } equals
                                new { A = dataKeHoachCap.IIDMLNS, B = dataKeHoachCap.sMaCoSoYTe } into dkh
                             from kh in dkh.DefaultIfEmpty()
                             join dataCapPhatBoSung in dataCapPhatBoSungs
                             on new { A = mucLuc.IIDMLNS, B = mucLuc.IIDMaCoSoYTe } equals
                                new { A = dataCapPhatBoSung.IIDMLNS, B = dataCapPhatBoSung.sMaCoSoYTe } into cpbs
                             from cpb in cpbs.DefaultIfEmpty()
                             join dataQuyetToan in dataQuyetToans
                             on new { A = mucLuc.IIDMLNS, B = mucLuc.IIDMaCoSoYTe } equals
                                new { A = dataQuyetToan.IIDMLNS, B = dataQuyetToan.sMaCoSoYTe } into dqt
                             from qt in dqt.DefaultIfEmpty()

                             orderby mucLuc.SXauNoiMa, mucLuc.IIDMaCoSoYTe
                             select new BhQtCapKinhPhiKcbChiTiet
                             {
                                 Id = sub?.Id ?? Guid.NewGuid(),
                                 IIDCTCapKinhPhiKCB = voucherID,
                                 IIdMlns = mucLuc.IIDMLNS,
                                 IIdMlnsCha = mucLuc.IIDMLNSCha.GetValueOrDefault(),
                                 SXauNoiMa = mucLuc.SXauNoiMa,
                                 SLns = mucLuc.SLNS,
                                 STenMLNS = mucLuc.SMoTa,
                                 IsAdd = sub == null,
                                 IsHangCha = mucLuc.BHangCha,
                                 STenCoSoYTe = mucLuc.STenCSYT,
                                 IIDCoSoYTe = mucLuc.IIDCoSoYTe,
                                 sMaCoSoYTe = mucLuc.IIDMaCoSoYTe,
                                 FKeHoachCap = kh?.FTamUngQuyNay ?? 0,
                                 FDaQuyetToan = qt?.FDaQuyetToan ?? 0,
                                 FConLai = sub?.FConLai ?? 0,
                                 FQuyetToanQuyNay = sub?.FQuyetToanQuyNay ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 FQuyetToan4Quy = cpb?.FQuyetToan4Quy ?? 0
                             };

                return result;
            }
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int? namLamViec, List<string> lstLNS, string lstCSYT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai;
                List<BhDmMucLucNganSach> bhMucLucsChild = new List<BhDmMucLucNganSach>();
                bhMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => lstLNS.Contains(x.SLNS)).ToList();

                List<BhDmMucLucNganSach> bhMucLucs = new List<BhDmMucLucNganSach>();
                if (bhMucLucsChild.Count > 0)
                {
                    var listIdMlskt = bhMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    bhMucLucs = bhMucLucsChild;
                    while (true)
                    {
                        var test = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlskt.AddRange(lstId);
                            bhMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                bhMucLucs = bhMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).ToList();
                var coSoyTe = GetCoSoYTe(lstCSYT, namLamViec);
                var mlnsChild = bhMucLucs.Where(x => !x.BHangCha);
                var mlnsParent = bhMucLucs.Where(x => x.BHangCha);
                var mlnsChildCSYT = from coSoYTes in coSoyTe
                                    from mlnsChilds in mlnsChild
                                    select new BhDmMucLucNganSach
                                    {
                                        Id = mlnsChilds.Id,
                                        SXauNoiMa = mlnsChilds.SXauNoiMa,
                                        SLNS = mlnsChilds.SLNS,
                                        SL = mlnsChilds.SL,
                                        SK = mlnsChilds.SK,
                                        SM = mlnsChilds.SM,
                                        STM = mlnsChilds.STM,
                                        STTM = mlnsChilds.STTM,
                                        SNG = mlnsChilds.SNG,
                                        STNG = mlnsChilds.STNG,
                                        SMoTa = mlnsChilds.SMoTa,
                                        BHangCha = mlnsChilds.BHangCha,
                                        ITrangThai = mlnsChilds.ITrangThai,
                                        DNgayTao = mlnsChilds.DNgayTao,
                                        DNgaySua = mlnsChilds.DNgaySua,
                                        ILoai = mlnsChilds.ILoai,
                                        ILock = mlnsChilds.ILock,
                                        IIDMLNS = mlnsChilds.IIDMLNS,
                                        IIDMLNSCha = mlnsChilds.IIDMLNSCha,
                                        INamLamViec = mlnsChilds.INamLamViec,
                                        STenCSYT = coSoYTes.STenCoSoYTe,
                                        IIDCoSoYTe = coSoYTes.Id,
                                        IIDMaCoSoYTe = coSoYTes.IIDMaCoSoYTe
                                    };
                var results = mlnsParent.Concat(mlnsChildCSYT).ToList();
                return results;
            }
        }

        private IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindVoucherDetail(Guid iDChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtCapKinhPhiKcbChiTiets.Where(x => x.IIDCTCapKinhPhiKCB == iDChungTu).ToList();
            }
        }

        private IEnumerable<BhDmCoSoYTe> GetCoSoYTe(string maCSYT, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstmaCsyt = maCSYT.Contains(",") ? maCSYT.Split(',').ToList() : new List<string> { maCSYT };
                return ctx.BhDmCoSoYTes.Where(x => x.INamLamViec == namLamViec && lstmaCsyt.Contains(x.IIDMaCoSoYTe)).ToList();
            }
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTietQuery> GetDataDaQuyetToan(Guid chungTuId, int? namLamViec, string maCSYT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan @ChungTuId, @NamLamViec, @sMaCSYT ";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", chungTuId),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@sMaCSYT", maCSYT)
                };
                return ctx.FromSqlRaw<BhQtCapKinhPhiKcbChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTietQuery> GetDataKeHoachCap(int? namLamViec, int? quy, string maCSYT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap @NamLamViec, @Quy, @MaCSYT";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@Quy", quy),
                    new SqlParameter("@MaCSYT", maCSYT)
                };
                return ctx.FromSqlRaw<BhQtCapKinhPhiKcbChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTietQuery> GetDataCapPhatBoSung(int? namLamViec, int? quy, string maCSYT, int? loaiKinhPhi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtc_cap_kp_kcb_get_cap_phat_bo_sung @NamLamViec, @Quy, @MaCSYT, @LoaiKinhPhi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@Quy", quy),
                    new SqlParameter("@MaCSYT", maCSYT),
                    new SqlParameter("@LoaiKinhPhi", loaiKinhPhi)
                };
                return ctx.FromSqlRaw<BhQtCapKinhPhiKcbChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
