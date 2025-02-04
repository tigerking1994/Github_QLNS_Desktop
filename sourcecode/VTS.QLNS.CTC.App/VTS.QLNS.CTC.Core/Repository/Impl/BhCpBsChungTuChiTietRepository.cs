using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhCpBsChungTuChiTietRepository : Repository<BhCpBsChungTuChiTiet>, IBhCpBsChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhCpBsChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool ExistVoucherDetail(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTuChiTiets.Any(t => t.IIDCTCapPhatBS.Equals(chungTuId));
            }
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportKeHoachCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter xauNoiMaParam = new SqlParameter("@XauNoiMa", sXauNoiMa);
                return ctx.FromSqlRaw<BhCpBsChungTuChiTietQuery>("EXECUTE sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt @IdCsYTe, @IQuy, @NamLamViec, @UserName,@Donvitinh,@XauNoiMa",
                              sIdCsYTeParam, iQuyParam, iNamLamViecParam, userNameParam, donViTinhParam, xauNoiMaParam).ToList();
            }
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportThongTriCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, int donViTinh, string sXauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter sXauNoiMaParam = new SqlParameter("@XauNoiMa", sXauNoiMa);

                return ctx.FromSqlRaw<BhCpBsChungTuChiTietQuery>("EXECUTE sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt @IdCsYTe, @IQuy, @NamLamViec, @Donvitinh, @XauNoiMa",
                              sIdCsYTeParam, iQuyParam, iNamLamViecParam, donViTinhParam, sXauNoiMaParam).ToList();
            }
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportTongHopCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter sXauNoiMaParam = new SqlParameter("@XauNoiMa", sXauNoiMa);

                return ctx.FromSqlRaw<BhCpBsChungTuChiTietQuery>("EXECUTE sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt @IdCsYTe, @IQuy, @NamLamViec, @UserName, @Donvitinh, @XauNoiMa",
                              sIdCsYTeParam, iQuyParam, iNamLamViecParam, userNameParam, donViTinhParam, sXauNoiMaParam).ToList();
            }
        }

        public BhCpBsChungTuChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTuChiTiets.Find(id);
            }
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindChungTuChiTietByChungTuId(BhCpBsChungTuChiTietCriteria searchCondition)
        {Guid chungTuId = searchCondition.IIDCTCapPhatBS;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTuChiTiets.Where(x => x.IIDCTCapPhatBS == chungTuId).ToList();
            }
            
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindVoucherDetailByCondition(BhCpBsChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var namLamViec = searchCondition.NamLamViec;
                var iTrangThai = searchCondition.ITrangThai;
                var voucherID = searchCondition.IIDCTCapPhatBS;
                var lstCSYT = searchCondition.LstCSYT;
                var mucLucs = GetListBhMucLucNs(namLamViec, searchCondition.LstLns, lstCSYT);
                var lstChungTuChiTiet = FindVoucherDetail(voucherID).ToList();
                var capPhatTamUng = GetSoTamUng(searchCondition.IQuy, namLamViec);
                var quyetToanTamUng = GetSoDaQuyetToan(searchCondition.IQuy, namLamViec);
                //var soTamUng = capPhatTamUng.Sum(x => x.FTamUngQuyNay);

                var result = from nsMucLuc in mucLucs
                             join chungTuChiTiet in lstChungTuChiTiet
                             on new { A = nsMucLuc.IIDMLNS, B = nsMucLuc.IIDMaCoSoYTe } equals
                                new { A = chungTuChiTiet.IIdMlns, B = chungTuChiTiet.IIDMaCoSoYTe } into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa, nsMucLuc.STenCSYT
                             select new BhCpBsChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IIDCTCapPhatBS = voucherID,
                                 IIdMlns = nsMucLuc.IIDMLNS,
                                 STenMLNS = nsMucLuc.SMoTa,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 SLns = nsMucLuc.SLNS,
                                 SL = nsMucLuc.SL,
                                 SK = nsMucLuc.SK,
                                 SM = nsMucLuc.SM,
                                 STM = nsMucLuc.STM,
                                 STTM = nsMucLuc.STTM,
                                 SNG = nsMucLuc.SNG,
                                 STNG = nsMucLuc.STNG,
                                 IsAdd = sub == null,
                                 DNgayTao = DateTime.Now,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = nsMucLuc.BHangCha,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 SCoSoYTe = sub?.SCoSoYTe ?? null,
                                 FDaQuyetToan = NumberUtils.DoubleIsNullOrZero(sub?.FDaQuyetToan ?? 0) ? quyetToanTamUng.IsEmpty() ? 0 :
                                                (quyetToanTamUng.Any(x => x.IIdMlns.Equals(nsMucLuc.IIDMLNS) &&
                                                nsMucLuc.IIDMaCoSoYTe.Equals(x.sMaCoSoYTe)) ?
                                                quyetToanTamUng.FirstOrDefault(x => x.IIdMlns.Equals(nsMucLuc.IIDMLNS) &&
                                                nsMucLuc.IIDMaCoSoYTe.Equals(x.sMaCoSoYTe)).FQuyetToanQuyNay ?? 0 : 0) : sub?.FDaQuyetToan,
                                 FDaCapUng = NumberUtils.DoubleIsNullOrZero(sub?.FDaCapUng ?? 0) ? capPhatTamUng.IsEmpty() ? 0 :
                                                (capPhatTamUng.Any(x => x.IID_MLNS.Equals(nsMucLuc.IIDMLNS) &&
                                                nsMucLuc.IIDMaCoSoYTe.Equals(x.IID_MaCoSoYTe)) ?
                                                capPhatTamUng.FirstOrDefault(x => x.IID_MLNS.Equals(nsMucLuc.IIDMLNS) &&
                                                nsMucLuc.IIDMaCoSoYTe.Equals(x.IID_MaCoSoYTe)).FTamUngQuyNay ?? 0 : 0) : sub?.FDaCapUng,
                                 FThuaThieu = sub?.FThuaThieu ?? 0,
                                 FSoCapBoSung = sub?.FSoCapBoSung ?? 0,
                                 STenCSYT = nsMucLuc.STenCSYT,
                                 IIDCoSoYTe = nsMucLuc.IIDCoSoYTe,
                                 IIDMaCoSoYTe = nsMucLuc.IIDMaCoSoYTe,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 INamLamViec = namLamViec,
                                 IIdMaDonVi = searchCondition.IdDonVi
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

                bhMucLucs = bhMucLucsChild.GroupBy(x => x.IIDMLNS).Select(x => x.First()).ToList();
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

        private IEnumerable<BhCpBsChungTuChiTiet> FindVoucherDetail(Guid iDChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IDChungTu", iDChungTu);
                return ctx.BhCpBsChungTuChiTiets.FromSql("EXECUTE dbo.sp_bh_cap_phat_bo_sung_chi_tiet @IDChungTu"
                    , idChungTuParam).ToList();
            }
        }

        private IEnumerable<BhDmCoSoYTe> GetCoSoYTe(string iDDsCSYT, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCoSoYTes.Where(x => x.INamLamViec == namLamViec && iDDsCSYT.Contains(x.IIDMaCoSoYTe)).ToList();
            }
        }

        private IEnumerable<BhCptuBHYTChiTiet> GetSoTamUng(int? iQuy, int? iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data =
                    from cptu in ctx.BhCptuBHYTs.Where(x => x.INamLamViec == iNamLamViec && x.IQuy == iQuy)
                    join ct in ctx.BhCptuBHYTChiTiets on cptu.Id equals ct.IID_BH_CP_CapTamUng_KCB_BHYT
                    select ct;
                var dataOut = data.GroupBy(x => new { x.IID_BH_CP_CapTamUng_KCB_BHYT, x.IID_MaCoSoYTe, x.IID_MLNS }).Select(x => new BhCptuBHYTChiTiet
                {
                    IID_BH_CP_CapTamUng_KCB_BHYT = x.Key.IID_BH_CP_CapTamUng_KCB_BHYT,
                    SLNS = x.FirstOrDefault().SLNS,
                    IID_MaCoSoYTe = x.Key.IID_MaCoSoYTe,
                    FTamUngQuyNay = x.Sum(s => s.FTamUngQuyNay) ?? 0,
                    IID_MLNS = x.Key.IID_MLNS
                }).ToList();
                return dataOut;
            }
        }
        private IEnumerable<BhQtCapKinhPhiKcbChiTiet> GetSoDaQuyetToan(int? iQuy, int? iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data =
                    from qtckp in ctx.BhQtCapKinhPhiKcbs.Where(x => x.INamLamViec == iNamLamViec && x.IQuy == iQuy)
                    join ct in ctx.BhQtCapKinhPhiKcbChiTiets on qtckp.Id equals ct.IIDCTCapKinhPhiKCB
                    select ct;
                var dataOut = data.GroupBy(x => new { x.IIDCTCapKinhPhiKCB, x.sMaCoSoYTe, x.IIdMlns }).Select(x => new BhQtCapKinhPhiKcbChiTiet
                {
                    IIDCTCapKinhPhiKCB = x.Key.IIDCTCapKinhPhiKCB,
                    SLns = x.FirstOrDefault().SLns,
                    sMaCoSoYTe = x.Key.sMaCoSoYTe,
                    FQuyetToanQuyNay = x.Sum(s => s.FQuyetToanQuyNay) ?? 0,
                    IIdMlns = x.Key.IIdMlns
                }).ToList();
                return dataOut;
            }
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportTheoCoSoYTe(Guid voucherID, string maCSYT, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIDParam = new SqlParameter("@VoucherId", voucherID);
                SqlParameter maCSYTParam = new SqlParameter("@MaCSYT", maCSYT);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);

                return ctx.FromSqlRaw<BhCpBsChungTuChiTietQuery>("EXECUTE sp_bhxh_export_cap_phat_bo_sung @VoucherId, @MaCSYT, @NamLamViec",
                              voucherIDParam, maCSYTParam, iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<string> GetMaCoSoYTeDetailByCondition(int iQuy, int iNamLamViec, string sXauNoiMa, bool isTongHop, AllocationFunction functionType, bool isShowAllCoSoYTe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIDParam = new SqlParameter("@iQuy", iQuy);
                SqlParameter maCSYTParam = new SqlParameter("@XauNoiMa", sXauNoiMa);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                string sql = string.Empty;
                if (functionType == AllocationFunction.CAP_KINH_PHI)
                {
                    //To do
                }
                else if (functionType == AllocationFunction.CAP_TAM_UNG)
                {
                    if (isShowAllCoSoYTe)
                    {
                        sql = "SELECT DISTINCT ct.sDSID_CoSoYTe as IIdMaCoSoYTe  FROM BH_CP_CapTamUng_KCB_BHYT ct ";
                    }
                    else
                    {
                        sql = "SELECT DISTINCT ctct.iID_MaCoSoYTe as IIdMaCoSoYTe  FROM BH_CP_CapTamUng_KCB_BHYT ct ";
                    }

                    sql += @" INNER JOIN BH_CP_CapTamUng_KCB_BHYT_ChiTiet ctct 
                                       ON ct.iID_BH_CP_CapTamUng_KCB_BHYT =   ctct.iID_BH_CP_CapTamUng_KCB_BHYT
                            WHERE ct.iQuy = @iQuy
                            	  AND ct.iNamLamViec = @NamLamViec
                            	  AND sXauNoiMa = @XauNoiMa
                            	   ";
                    //if (isTongHop)
                    //    sql += " AND ct.bIsTongHop = 1 AND ct.sDSSoChungTuTongHop is not null";
                    //else
                    //    sql += " AND ct.bIsTongHop <> 1 AND ct.sDSSoChungTuTongHop is null";
                }
                else if (functionType == AllocationFunction.CAP_BO_SUNG)
                {   
                    if (isShowAllCoSoYTe)
                    {
                        sql = "SELECT DISTINCT ct.sCoSoYTe FROM BH_CP_CapBoSung_KCB_BHYT ct ";
                    } 
                    else
                    {
                        sql = "SELECT DISTINCT ctct.iID_MaCoSoYTe FROM BH_CP_CapBoSung_KCB_BHYT ct ";
                    }

                    sql += @" INNER JOIN BH_CP_CapBoSung_KCB_BHYT_ChiTiet ctct ON ct.iID_CTCapPhatBS = ctct.iID_CTCapPhatBS
                               WHERE ct.iQuy = @iQuy
	                           AND ct.iNamLamViec = @NamLamViec                            
	                           AND sXauNoiMa = @XauNoiMa ";

                    if (!isShowAllCoSoYTe)
                    {
                        sql += " AND ctct.fSoCapBoSung != 0";
                    }

                    //if (isTongHop)
                    //    sql += " AND ct.iLoaiTongHop = 2 AND ct.sDSSoChungTuTongHop is not null";
                    //else
                    //    sql += " AND ct.iLoaiTongHop <> 2 AND ct.sDSSoChungTuTongHop is null";
                }


                return ctx.FromSqlRaw<string>(sql, voucherIDParam, maCSYTParam, iNamLamViecParam).ToList();
            }
        }
    }
}
