using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdtcBHXHChiTietRepository : Repository<BhPbdtcBHXHChiTiet>, IPbdtcBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdtcBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhPbdtcBHXHChiTiet> FindByCondition(Expression<Func<BhPbdtcBHXHChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdtcBHXHChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTiet(Guid chungTuPhanBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName, int? loaiDotNhanPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuPhaBoIdParam = new SqlParameter("@ChungTuId", chungTuPhanBoId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter DotPBParam = new SqlParameter("@DotPB", loaiDotNhanPhanBo);
                var lstData = ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>("EXECUTE sp_bh_dt_danhsach_pbdtc_chitiet @ChungTuId, @LNS, @IdDonVi, @NamLamViec, @UserName,@DotPB",
                              chungTuPhaBoIdParam, sLNSParam, sIdDonViParam, iNamLamViecParam, userNameParam, DotPBParam).ToList();
                lstData.ForEach(x =>
                {
                    x.BHangCha = x.BHangChaDuToan.Value;
                    x.IsHangCha = x.BHangChaDuToan.Value;
                    x.SMaLoaiChi = GetMaLoaiChi(x.SLNS);
                });
                CalculateData(lstData);
                return lstData;
            }
        }


        private void CalculateData(List<BhPbdtcBHXHChiTietQuery> lstData)
        {

            lstData.Where(x => x.BHangCha && x.Type != (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO)
                .ForAll(x =>
                {
                    x.FTienTuChi = 0;
                    x.FTienTuChiTruocDieuChinh = 0;
                    x.FTienTuChiSauDieuChinh = 0;
                });
            var dictByMlns = lstData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstData.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

        }

        private void CalculateParent(Guid? idParent, BhPbdtcBHXHChiTietQuery item, Dictionary<Guid?, BhPbdtcBHXHChiTietQuery> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //Trước điều chỉnh
            model.FTienTuChiTruocDieuChinh = (model.FTienTuChiTruocDieuChinh == null ? 0 : model.FTienTuChiTruocDieuChinh) + (item.FTienTuChiTruocDieuChinh == null ? 0 : item.FTienTuChiTruocDieuChinh);

            model.FTienTuChi = model.FTienTuChi.GetValueOrDefault(0) + item.FTienTuChi.GetValueOrDefault(0);

            //Sau điều chinh
            model.FTienTuChiSauDieuChinh = (model.FTienTuChiTruocDieuChinh ?? 0) + (model.FTienTuChi ?? 0);


            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }


        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuPhaBoIdParam = new SqlParameter("@ChungTuId", chungTuPhanBoId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                var lstDataDieuChinh = ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>("EXECUTE sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh @ChungTuId, @LNS, @NamLamViec, @UserName",
                              chungTuPhaBoIdParam, sLNSParam, iNamLamViecParam, userNameParam).ToList();

                if (sLNS == LNSValue.LNS_9010001_9010002|| sLNS == LNSValue.LNS_901_9010001_9010002)
                {
                    lstDataDieuChinh.ForAll(
                        x =>
                        {
                            if (!string.IsNullOrEmpty(x.SM))
                            {
                                x.IsHangCha = false;
                                x.BHangCha = false;
                            }
                        });
                }
                else
                {
                    lstDataDieuChinh.ForAll(x =>
                    {
                        x.IsHangCha = false;
                        x.BHangCha = false;
                    });
                }

                CalculateData(lstDataDieuChinh);

                return lstDataDieuChinh;

            }
        }

        private string GetMaLoaiChi(string sLNS)
        {
            switch (sLNS)
            {
                case LNSValue.LNS_9010001:
                case LNSValue.LNS_9010002:
                    return MaLoaiChiBHXH.SMABHXH;
                case LNSValue.LNS_9010003:
                    return MaLoaiChiBHXH.SMAKPQL;
                case LNSValue.LNS_9010004_9010005:
                    return MaLoaiChiBHXH.SMAKCBQYDV;
                case LNSValue.LNS_9010006_9010007:
                    return MaLoaiChiBHXH.SMAKCBTS;
                case LNSValue.LNS_9010008:
                    return MaLoaiChiBHXH.SMAKCBBHYT;
                case LNSValue.LNS_9010009:
                    return MaLoaiChiBHXH.SMAMSTTBYT;
                case LNSValue.LNS_9010010:
                    return MaLoaiChiBHXH.SMABHTN;
                case LNSValue.LNS_905:
                case LNSValue.LNS_9050001:
                case LNSValue.LNS_9050002:
                    return MaLoaiChiBHXH.SMAHSSVNLD;
                default:
                    return string.Empty;
            }

        }

        public IEnumerable<BhPbdtcBHXHChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sMaDonViParam = new SqlParameter("@MaDonVi", sMaDonVi);

                return ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>("EXECUTE sp_bh_export_phan_bo_du_toan_chi_tiet @ChungTuId, @LNS, @NamLamViec,@MaDonVi",
                              chungTuIdParam, sLNSParam, iNamLamViecParam, sMaDonViParam).ToList();
            }
        }

        public IEnumerable<BhPbdtcBHXHChiTietQuery> GetSoChuaPhanBo(Guid iD_Ndtctg, Guid iD_Mlns, Guid? idChungTuEdit)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iD_NdtctgParam = new SqlParameter("@ID_Ndtctg", iD_Ndtctg);
                SqlParameter iD_MlnsParam = new SqlParameter("@ID_Mlns", iD_Mlns);
                SqlParameter idChungTuEditParam = new SqlParameter("@ID_pbdtct_ct", idChungTuEdit);

                return ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>("EXECUTE sp_bh_pbdtc_tinh_sochuaphanbo @ID_Ndtctg, @ID_Mlns, @ID_pbdtct_ct",
                              iD_NdtctgParam, iD_MlnsParam, idChungTuEditParam).ToList();
            }
        }

        public List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiBHXH(int yearOfWork, string selectedUnits, Guid? IDLoaiChi, string sMaLoaiChi
                                                , string lstIdChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@INamLamViec", yearOfWork);
                SqlParameter selectedUnitsParam = new SqlParameter("@IdMaDonVi", selectedUnits);
                SqlParameter iDLoaiChiParam = new SqlParameter("@IDLoaiChi", IDLoaiChi);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaiChi", sMaLoaiChi);
                SqlParameter sIdChungTu = new SqlParameter("@IdChungTu ", lstIdChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);

                if (!IsTongHopDonViKhoi)
                {
                    if (dotNhan == 0)
                    {
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_donvi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh, @IsMillionRound",
                             yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIdChungTu, donViTinhParam, isMillionRoundParam).ToList();
                    }
                    else
                    {
                        SqlParameter dotNhanParam = new SqlParameter("@DotNhan", dotNhan);
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh,@DotNhan, @IsMillionRound",
                             yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIdChungTu, donViTinhParam, dotNhanParam, isMillionRoundParam).ToList();
                    }
                }
                else
                {
                    if (dotNhan == 0)
                    {
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu,@Donvitinh, @IsMillionRound",
                                  yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIdChungTu, donViTinhParam, isMillionRoundParam).ToList();
                    }
                    else
                    {
                        SqlParameter dotNhanParam = new SqlParameter("@DotNhan", dotNhan);
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh,@DotNhan, @IsMillionRound",
                             yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIdChungTu, donViTinhParam, dotNhanParam, isMillionRoundParam).ToList();
                    }
                }
            }
        }

        public List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiKPQLKCBKHAC(int yearOfWork, string selectedUnits, Guid? idLoaiChi, string sMaLoaiChi, string lstIDChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@INamLamViec", yearOfWork);
                SqlParameter selectedUnitsParam = new SqlParameter("@IdMaDonVi", selectedUnits);
                SqlParameter iDLoaiChiParam = new SqlParameter("@IDLoaiChi", idLoaiChi);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaiChi", sMaLoaiChi);
                SqlParameter sIDChungTuParam = new SqlParameter("@IdChungTu", lstIDChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);

                if (!IsTongHopDonViKhoi)
                {
                    if (dotNhan == 0)
                    {
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh, @IsMillionRound",
                                  yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIDChungTuParam, donViTinhParam, isMillionRoundParam).ToList();
                    }
                    else
                    {
                        SqlParameter dotNhanParam = new SqlParameter("@DotNhan", dotNhan);
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh,@DotNhan, @IsMillionRound",
                                  yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIDChungTuParam, donViTinhParam, dotNhanParam, isMillionRoundParam).ToList();
                    }
                }

                else
                {
                    if (dotNhan == 0)
                    {
                        SqlParameter dotNhanParam = new SqlParameter("@DotNhan", dotNhan);
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh,@DotNhan, @IsMillionRound",
                                       yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIDChungTuParam, donViTinhParam, dotNhanParam,isMillionRoundParam).ToList();
                    }

                    else
                    {
                        SqlParameter dotNhanParam = new SqlParameter("@DotNhan", dotNhan);
                        return ctx.FromSqlRaw<BhPbdtcBHXHChiBHXHReportQuery>("EXECUTE sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi @INamLamViec, @IdMaDonVi,@IDLoaiChi,@MaLoaiChi,@IdChungTu, @Donvitinh,@DotNhan, @IsMillionRound",
                                           yearOfWorkParam, selectedUnitsParam, iDLoaiChiParam, sMaLoaiChiParam, sIDChungTuParam, donViTinhParam, dotNhanParam, isMillionRoundParam).ToList();

                    }
                }

            }
        }

        public List<BhPbdtcBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string sIdDonVi, int iNamLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone @NamLamViec, @IdDonVi";
                var parameters = new[] {
                    new SqlParameter("@IdDonVi", sIdDonVi),
                    new SqlParameter("@NamLamViec", iNamLamViec),
                };
                return ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoGopChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound, string lstMaLoaiChi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_phuongan_pbdtc @NamLamViec, @SoQuyetDinh,@NgayQuyetDinh,@MaDonVi,@DonViTinh,@IsMillionRound,@MaLoaiChi";
                var parameters = new[] {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@SoQuyetDinh", soQuyetDinh),
                    new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@MaDonVi", selectedUnits),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsMillionRound", isMillionRound),
                    new SqlParameter("@MaLoaiChi", lstMaLoaiChi),
                };
                return ctx.FromSqlRaw<ReportDuToanChiBHXHBHYTBHTNQuery>(sql, parameters).ToList();
            }
        }

        public List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoTachChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound , string lstMaLoaiChi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql @NamLamViec, @SoQuyetDinh,@NgayQuyetDinh,@MaDonVi,@DonViTinh,@IsMillionRound,@MaLoaiChi";
                var parameters = new[] {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@SoQuyetDinh", soQuyetDinh),
                    new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@MaDonVi", selectedUnits),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsMillionRound", isMillionRound),
                    new SqlParameter("@MaLoaiChi", lstMaLoaiChi),
                };
                return ctx.FromSqlRaw<ReportDuToanChiBHXHBHYTBHTNQuery>(sql, parameters).ToList();
            }
        }
    }
}