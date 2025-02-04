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
    public class NdtctgBHXHChiTietRepository : Repository<BhDtctgBHXHChiTiet>, INdtctgBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NdtctgBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<BhDtctgBHXHChiTiet> FindByCondition(Guid Id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtctgBHXHChiTiets.Where(x => x.IID_DTC_DuToanChiTrenGiao == Id).ToList();
                return lst;
            }
        }

        public IEnumerable<BhDtctgBHXHChiTietQuery> GetListNhanDuToanChiTrenGiaoChiTiet(Guid idNdtctg, string sLNs, int iNamLamViec, string IIDDonVi, int loaiDotNhanPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idNdtctgs = new SqlParameter("@iDNdtctg", idNdtctg);
                SqlParameter lNs = new SqlParameter("@sLNs", sLNs);
                SqlParameter NamLamViec = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter IIDDonViPara = new SqlParameter("@IIDDonVi", IIDDonVi);
                SqlParameter DotPhanBoPara = new SqlParameter("@DotPb", loaiDotNhanPhanBo);
                var lstChungTuChiTiet = ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>("EXECUTE dbo.sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone @iDNdtctg,@sLNs,@NamLamViec,@IIDDonVi,@DotPb ", idNdtctgs, lNs, NamLamViec, IIDDonViPara, DotPhanBoPara).ToList();
                lstChungTuChiTiet.ForEach(x =>
                {
                    x.SMaLoaiChi = GetMaLoaiChi(x.SLNS);
                });
                return lstChungTuChiTiet;
            }
        }

        public IEnumerable<BhDtctgBHXHChiTietQuery> GetBaoCaoChiTieuNganSach(string idDonVi, int iNamLamViec, string sLNS, int dotNhan, string SMaLoaiChi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter DonVisPara = new SqlParameter("@idDonvi", idDonVi);
                SqlParameter LNSPara = new SqlParameter("@sLns", sLNS);
                SqlParameter NamLamViecPara = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter DotNhanPara = new SqlParameter("@DotNhan", dotNhan);
                SqlParameter MaLoaiChiPara = new SqlParameter("@MaLoaichi", SMaLoaiChi);
                SqlParameter DonViTinhPara = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>("EXECUTE dbo.sp_bhxh_baocao_dutoan_thongbaocaochitieungansach @idDonvi, @sLns,@NamLamViec,@DotNhan,@MaLoaichi,@DVT", DonVisPara, LNSPara, NamLamViecPara, DotNhanPara, MaLoaiChiPara, DonViTinhPara).ToList();
            }
        }

        public List<BhDtctgBHXHChiTietQuery> GetListDataAgregateChiTiet(Guid idChungTu, string sLNS, int yearOfWork, string sMaDonVi, Guid? IDLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {

                SqlParameter idNdtctgs = new SqlParameter("@iDNdtctg", idChungTu);
                SqlParameter lNs = new SqlParameter("@sLNs", sLNS);
                SqlParameter NamLamViec = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter IIDDonViPara = new SqlParameter("@IIDDonVi", sMaDonVi);
                string strQuery = string.Empty;

                strQuery = "EXECUTE dbo.sp_bhxh_ndt_ctg_get_khc_slns @iDNdtctg,@sLNs,@NamLamViec,@IIDDonVi ";
                var lstChungTuChiTiet = ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>(strQuery, idNdtctgs, lNs, NamLamViec, IIDDonViPara).ToList();
                lstChungTuChiTiet.ForEach(x =>
                {
                    x.SMaLoaiChi = GetMaLoaiChi(x.SLNS);
                });
                return lstChungTuChiTiet;
            }
        }

        public bool ExistChungTu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtctgBHXHChiTiets.Where(x => x.IID_DTC_DuToanChiTrenGiao == id && x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustKPQL(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAKPQL).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustBHXH(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMABHXH).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustKCBQYDV(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustKCBTSDK(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBTS).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustKCBBHYT(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBBHYT).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustHSSVNLD(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAHSSVNLD).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustBHTN(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMABHTN).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        private bool ExistAgregateAdjustSTTBYT(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtcDcdToanChis.Where(x => x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.INamLamViec == namLamViec && x.SMaLoaiChi == MaLoaiChiBHXH.SMAMSTTBYT).ToList();
                if (lst.Count() > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        //private List<BhDtcDcdToanChiChiTietQuery> GetDataCheckAgregateAdjustExist(List<BhDtcDcdToanChiChiTietQuery> lstNotYetCalculateAgregate, int namLamViec)
        //{
        //    if (!ExistAgregateAdjustBHXH(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010001_9010002.Split(',').Contains(x.SLNS)).ForAll(x =>
        //        {
        //            x.FTienDuToanDuocGiao = 0;
        //        });
        //    }
        //    if (!ExistAgregateAdjustKPQL(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010003.Split(',').Contains(x.SLNS)).ForAll(x =>
        //        {
        //            x.FTienDuToanDuocGiao = 0;
        //        });
        //    }
        //    if (!ExistAgregateAdjustKCBQYDV(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010004.Split(',').Contains(x.SLNS)).ForAll(x =>
        //        {
        //            x.FTienDuToanDuocGiao = 0;
        //        });
        //    }

        //    if (!ExistAgregateAdjustKCBTSDK(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010006_9010007.Split(',').Contains(x.SLNS)).ForAll(x => { x.FTienDuToanDuocGiao = 0; });
        //    }
        //    if (!ExistAgregateAdjustKCBBHYT(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010008.Split(',').Contains(x.SLNS)).ForAll(x => { x.FTienDuToanDuocGiao = 0; });
        //    }
        //    if (!ExistAgregateAdjustHSSVNLD(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9050001_9050002.Split(',').Contains(x.SLNS)).ForAll(x => { x.FTienDuToanDuocGiao = 0; });
        //    }
        //    if (!ExistAgregateAdjustBHTN(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010010.Split(',').Contains(x.SLNS)).ForAll(x => { x.FTienDuToanDuocGiao = 0; });
        //    }
        //    if (!ExistAgregateAdjustSTTBYT(namLamViec))
        //    {
        //        lstNotYetCalculateAgregate.Where(x => LNSValue.LNS_9010009.Split(',').Contains(x.SLNS)).ForAll(x => { x.FTienDuToanDuocGiao = 0; });
        //    }
        //    return lstNotYetCalculateAgregate;
        //}
        public List<BhDtctgBHXHChiTietQuery> GetListDataAgregateAdjustChiTiet(Guid idChungTu, int namLamViec, string sMaDonVi, DateTime? dNgayChungTu, string sLNS)
        {
            var lstNotYetCalculateAgregate = GetDataAgregateAdjustChiTiet(namLamViec, sMaDonVi, dNgayChungTu, sLNS);
            //lstNotYetCalculateAgregate = GetDataCheckAgregateAdjustExist(lstNotYetCalculateAgregate, namLamViec);
            CalculateDataDieuChinh(lstNotYetCalculateAgregate);
            lstNotYetCalculateAgregate = lstNotYetCalculateAgregate.Where(x => x.BHangChaDuToan != null).ToList();
            lstNotYetCalculateAgregate.ForEach(x =>
            {
                x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam.GetValueOrDefault(0) + x.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
                x.FTienSoSanhGiam = (x.FTienDuToanDuocGiao.GetValueOrDefault(0) - x.FTienUocThucHienCaNam.GetValueOrDefault(0)) > 0 ? x.FTienDuToanDuocGiao.GetValueOrDefault(0) - x.FTienUocThucHienCaNam.GetValueOrDefault(0) : 0;
                x.FTienSoSanhTang = (x.FTienUocThucHienCaNam.GetValueOrDefault(0) - x.FTienDuToanDuocGiao.GetValueOrDefault(0)) > 0 ? x.FTienUocThucHienCaNam.GetValueOrDefault(0) - x.FTienDuToanDuocGiao.GetValueOrDefault(0) : 0;
                x.FTienTangGiam = (x.FTienSoSanhTang.GetValueOrDefault(0) - x.FTienSoSanhGiam.GetValueOrDefault(0) > 0) ? x.FTienSoSanhTang.GetValueOrDefault(0) : (-x.FTienSoSanhGiam.GetValueOrDefault(0));

            });

            List<BhDtctgBHXHChiTietQuery> lstDtctg = new List<BhDtctgBHXHChiTietQuery>();
            lstDtctg = lstNotYetCalculateAgregate.Select(x => new BhDtctgBHXHChiTietQuery
            {
                IID_DTC_DuToanChiTrenGiao = idChungTu,
                IID_MucLucNganSach = x.IID_MucLucNganSach,
                FTienTuChi = x.FTienTangGiam.GetValueOrDefault(0),
                SLNS = x.SLNS,
                STM = x.STM,
                STTM = x.STTM,
                SM = x.SM,
                SNG = x.SNG,
                SNoiDung = x.SNoiDung,
                SXauNoiMa = x.SXauNoiMa,
                IID_MLNS = x.IID_MucLucNganSach,
                IID_MLNS_Cha = x.IdParent,
                BHangCha = x.BHangChaDuToan.Value,
                IsHangCha = x.BHangChaDuToan.Value,
                IIDMaDonVi = x.IIdMaDonVi,
                INamLamViec = x.INamLamViec,
                SDuToanChiTietToi = x.SDuToanChiTietToi,
                SMaLoaiChi = GetMaLoaiChi(x.SLNS)
            }).ToList();
            return lstDtctg;
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
        public List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int namLamViec, DateTime dNgayChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone @NamLamViec, @IdDonVi,@DNgayChungTu";
                var parameters = new[] {
                    new SqlParameter("@IdDonVi", iID_MaDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@DNgayChungTu", dNgayChungTu)

                };
                return ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string iID_MaDonVi, int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2 @NamLamViec, @IdDonVi";
                var parameters = new[] {
                    new SqlParameter("@IdDonVi", iID_MaDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                };
                return ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public List<BhDtcDcdToanChiChiTietQuery> GetDataAgregateAdjustChiTiet(int namLamViec, string sMaDonVi, DateTime? dNgayChungTu, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter LNsPara = new SqlParameter("@sLNs", sLNS);
                SqlParameter NamLamViecPara = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter MaDonViPara = new SqlParameter("@IIDDonVi", sMaDonVi);
                SqlParameter DNgayChungTuPara = new SqlParameter("@DNgayChungTu", dNgayChungTu);
                string strQuery = string.Empty;
                strQuery = "EXECUTE dbo.sp_bhxh_nhandutoanchitheodieuchinh_clone @sLNs,@NamLamViec,@IIDDonVi,@DNgayChungTu ";
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>(strQuery, LNsPara, NamLamViecPara, MaDonViPara, DNgayChungTuPara).ToList();
            }
        }

        private void CalculateDataDieuChinh(List<BhDtcDcdToanChiChiTietQuery> lstGetData6ThangQTCQuyChungTu)
        {
            lstGetData6ThangQTCQuyChungTu.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienThucHien06ThangDauNam = 0;
                   x.FTienUocThucHien06ThangCuoiNam = 0;
               });

            var temp = lstGetData6ThangQTCQuyChungTu.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = lstGetData6ThangQTCQuyChungTu.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentDieuChinh(item.IdParent, item, dictByMlns);
            }

        }

        private void CalculateParentDieuChinh(Guid idParent, BhDtcDcdToanChiChiTietQuery item, Dictionary<Guid, BhDtcDcdToanChiChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam.GetValueOrDefault(0);
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
            CalculateParentDieuChinh(model.IdParent, item, dictByMlns);
        }
    }
}
