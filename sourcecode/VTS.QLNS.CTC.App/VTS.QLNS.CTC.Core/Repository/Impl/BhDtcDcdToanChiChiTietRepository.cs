using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDtcDcdToanChiChiTietRepository : Repository<BhDtcDcdToanChiChiTiet>, IBhDtcDcdToanChiChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDtcDcdToanChiChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(BhDtcDcdToanChiChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                SqlParameter sMaDonVi = new SqlParameter("@MaDonVi", creation.IdDonVi);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@Nguoitao ,@IdChungTu, @NamLamViec,@MaDonVi",
                    listIdChungTuTongHop, nguoiTao, idChungTu, namLamViec, sMaDonVi);
            }
        }

        public bool ExistDTChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstDetail = ctx.BhDtcDcdToanChiChiTiets.Where(t => t.IID_BH_DTC.Equals(bhxhId) && t.FTienThucHien06ThangDauNam.GetValueOrDefault(0) != 0).ToList();
                if (lstDetail.Count > 0)
                    return true;
                return false;
            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> FindByConditionForChildUnit(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            return FindByConditionForChildUnitSummary(searchCondition);
        }

        private IEnumerable<BhDtcDcdToanChiChiTietQuery> FindByConditionForChildUnitSummary(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? iIDTongHop = searchCondition.ILoaiTongHop;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == searchCondition.NamLamViec;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == searchCondition.IdDonVi && x.NamLamViec == searchCondition.NamLamViec);

                var bhMucLucs = GetFineBHMLNSDuToanDieuChinhForLNS(searchCondition.NamLamViec, searchCondition.LNS);

                List<BhDtcDcdToanChiChiTiet> lsthDtcDcdToanChiChiTiets = new List<BhDtcDcdToanChiChiTiet>();
                List<BhDtcDcdToanChiChiTietQuery> lstGetData6ThangQTCQuyChungTu = new List<BhDtcDcdToanChiChiTietQuery>();
                List<BhPbdtcBHXHChiTietQuery> lstBhPbdtcBHXHChiTiet = new List<BhPbdtcBHXHChiTietQuery>();
                List<BhDtctgBHXHChiTietQuery> lstNhanPbDuToanChiTiet = new List<BhDtctgBHXHChiTietQuery>();
                lsthDtcDcdToanChiChiTiets = FindDTDieuChinhDuToanChiChiTiet(searchCondition.NamLamViec, searchCondition.IdDonVi).Where(x => x.IID_BH_DTC == searchCondition.DtcDcdToanChiId).ToList();
                lstGetData6ThangQTCQuyChungTu = FindData6ThangDauNamChiTiet(searchCondition.NamLamViec, searchCondition.IdDonVi, searchCondition.ILoaiDanhMucChi, searchCondition.MaLoaiChi, searchCondition.LNS, searchCondition.NgayChungTu);
                CalculateData(lstGetData6ThangQTCQuyChungTu);
                lstBhPbdtcBHXHChiTiet = FindPbDtChiChiTiet(searchCondition.NamLamViec, searchCondition.IdDonVi, searchCondition.ILoaiDanhMucChi, searchCondition.NgayChungTu, searchCondition.LNS).ToList();
                lstNhanPbDuToanChiTiet = FindNPBChiChiTiet(searchCondition).ToList();
                //CalculateDataPhanBoDuToanChi(lstBhPbdtcBHXHChiTiet);
                // bhMucLucs = bhMucLucs.Where(x => !string.IsNullOrEmpty(x.SCPChiTietToi) && !string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)).ToList();

                var result = from bhMucLuc in bhMucLucs
                             join DtcDcdToanChiChiTiet in lsthDtcDcdToanChiChiTiets on bhMucLuc.IIDMLNS equals DtcDcdToanChiChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()

                             join NhanPbDuToanChiTiet in lstNhanPbDuToanChiTiet on bhMucLuc.SXauNoiMa equals NhanPbDuToanChiTiet.SXauNoiMa
                             into npbct
                             from lstNpb in npbct.DefaultIfEmpty()

                             join GetData6ThangQTCQuyChungTu in lstGetData6ThangQTCQuyChungTu on bhMucLuc.IIDMLNS equals GetData6ThangQTCQuyChungTu.IID_MucLucNganSach
                                             into qtc
                             from data in qtc.DefaultIfEmpty()

                             join PbdtcBHXHChiTiet in lstBhPbdtcBHXHChiTiet on bhMucLuc.SXauNoiMa equals PbdtcBHXHChiTiet.SXauNoiMa
                              into pbd
                             from pbdt in pbd.DefaultIfEmpty()

                             orderby bhMucLuc.SXauNoiMa
                             select new BhDtcDcdToanChiChiTietQuery
                             {
                                 ID = sub == null ? Guid.Empty : sub.Id,
                                 IID_BH_DTC = searchCondition.DtcDcdToanChiId,
                                 SNoiDung = bhMucLuc.SMoTa,
                                 SMoTa = bhMucLuc.SMoTa,
                                 SLNS = bhMucLuc.SLNS,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 Nganh = bhMucLuc.SNG,
                                 INamLamViec = sub?.INamLamViec ?? searchCondition.NamLamViec,
                                 IIdMaDonVi = nsDonVi?.IIDMaDonVi ?? string.Empty,
                                 IdParent = bhMucLuc.IIDMLNSCha.Value,
                                 STenDonVi = nsDonVi?.TenDonVi ?? string.Empty,
                                 IID_MucLucNganSach = bhMucLuc.IIDMLNS,
                                 IsHangCha = bhMucLuc.BHangChaDuToanDieuChinh.GetValueOrDefault(),
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 SDuToanChiTietToi = bhMucLuc.SDuToanChiTietToi ?? string.Empty,
                                 FTienDuToanDuocGiao = nsDonVi.Loai == LoaiDonVi.ROOT ? (lstNpb?.FTienTuChi ?? 0) : (pbdt?.FTienTuChi ?? 0),
                                 //FTienDuToanDuocGiao = iIDTongHop.Equals(DtDcDtBhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FTienTuChi ?? 0) : (sub?.FTienDuToanDuocGiao ?? 0),
                                 FTienThucHien06ThangDauNam = sub != null ? (sub?.FTienThucHien06ThangDauNam ?? 0) : (data?.FTienThucHien06ThangDauNam ?? 0),
                                 FTienUocThucHien06ThangCuoiNam = sub?.FTienUocThucHien06ThangCuoiNam ?? 0,
                                 FTienUocThucHienCaNam = sub?.FTienUocThucHienCaNam ?? ((data?.FTienThucHien06ThangDauNam ?? 0) + (sub?.FTienUocThucHien06ThangCuoiNam ?? 0)),
                                 FTienSoSanhGiam = sub?.FTienSoSanhGiam ?? 0,
                                 FTienSoSanhTang = sub?.FTienSoSanhTang ?? 0,
                                 SGhiChu = sub == null ? string.Empty : sub.SGhiChu,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa,
                                 BHangCha = bhMucLuc.BHangChaDuToanDieuChinh.GetValueOrDefault(),
                                 BHangChaDuToan = bhMucLuc.BHangChaDuToan,
                             };
                return result;
            }
        }

        private void CalculateData(List<BhDtcDcdToanChiChiTietQuery> lstGetData6ThangQTCQuyChungTu)
        {

            lstGetData6ThangQTCQuyChungTu.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienThucHien06ThangDauNam = 0;

               });

            var temp = lstGetData6ThangQTCQuyChungTu.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = lstGetData6ThangQTCQuyChungTu.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

        }

        private void CalculateParent(Guid idParent, BhDtcDcdToanChiChiTietQuery item, Dictionary<Guid, BhDtcDcdToanChiChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam.GetValueOrDefault(0);
            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void CalculateDataPhanBoDuToanChi(List<BhPbdtcBHXHChiTietQuery> lstGetData6ThangQTCQuyChungTu)
        {

            lstGetData6ThangQTCQuyChungTu.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienTuChi = 0;

               });

            var temp = lstGetData6ThangQTCQuyChungTu.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = lstGetData6ThangQTCQuyChungTu.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentPhanBoDuToanChi(item.IdParent, item, dictByMlns);
            }

        }

        private void CalculateParentPhanBoDuToanChi(Guid idParent, BhPbdtcBHXHChiTietQuery item, Dictionary<Guid?, BhPbdtcBHXHChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienTuChi += item.FTienTuChi.GetValueOrDefault(0);
            CalculateParentPhanBoDuToanChi(model.IdParent, item, dictByMlns);
        }

        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindPbDtChiChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, DateTime? ngayChungTu, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@loaiDanhMucCapChi", loaiDanhMucCapChi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);


                return ctx.FromSqlRaw<BhPbdtcBHXHChiTietQuery>("EXECUTE dbo.sp_dt_phanbo_dieuchinh_chi_tiet_clone @NamLamViec, @IdDonVi,@loaiDanhMucCapChi,@SLNS,@ngayChungTu",
                    namLamViecParam, idDonViParam, loaiDanhMucCapChiParam, sLNSParam, ngayChungTuParam).ToList();
            }
        }


        public IEnumerable<BhDtctgBHXHChiTietQuery> FindNPBChiChiTiet(BhDtcDcdToanChiChiTietCriteria chiTietCriteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", chiTietCriteria.NamLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", chiTietCriteria.IdDonVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@loaiDanhMucCapChi", chiTietCriteria.ILoaiDanhMucChi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", chiTietCriteria.LNS);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", chiTietCriteria.NgayChungTu);


                return ctx.FromSqlRaw<BhDtctgBHXHChiTietQuery>("EXECUTE dbo.sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone @NamLamViec, @IdDonVi,@loaiDanhMucCapChi,@SLNS,@ngayChungTu",
                    namLamViecParam, idDonViParam, loaiDanhMucCapChiParam, sLNSParam, ngayChungTuParam).ToList();
            }
        }


        public List<BhDtcDcdToanChiChiTietQuery> FindData6ThangDauNamChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, string sMaLoaiChi, string sLNS, DateTime? dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@IID_LoaiChi", loaiDanhMucCapChi);
                SqlParameter sMaLoaiChiTuParam = new SqlParameter("@SMaLoaiChi", sMaLoaiChi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu", dNgayChungTu);

                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>("EXECUTE dbo.sp_dt_dieuchinh_getTienthuchien6thang @NamLamViec, @IdDonVi,@IID_LoaiChi,@SMaLoaiChi,@SLNS,@DNgayChungTu",
                    namLamViecParam, idDonViParam, loaiDanhMucCapChiParam, sMaLoaiChiTuParam, sLNSParam, dNgayChungTuParam).ToList();
            }
        }

        private IEnumerable<BhDtcDcdToanChiChiTiet> FindDTDieuChinhDuToanChiChiTiet(int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);

                return ctx.Set<BhDtcDcdToanChiChiTiet>().FromSql("EXECUTE dbo.sp_dt_dieuchinh_chi_tiet @NamLamViec, @IdDonVi",
                    namLamViecParam, idDonViParam).ToList();
            }
        }

        private List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec, string idDonVi, string SLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dataSLNS = SLNS.Split(',');
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> bhMucLucsChild = new List<BhDmMucLucNganSach>();
                bhMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => dataSLNS.Contains(x.SLNS)).ToList();

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
                bhMucLucs = bhMucLucs.Where(x => x.SLNS != LNSValue.LNS_901 && x.SLNS != LNSValue.LNS_9).GroupBy(x => x.IIDMLNS).Select(x => x.First()).ToList();
                return bhMucLucs;
            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtcDcdToanChiChiTiets.Where(x => x.IID_BH_DTC == id).ToList();
            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@IDLoaiChi", searchCondition.ILoaiDanhMucChi),
                    new SqlParameter("@SLNS", searchCondition.LNS),
                    new SqlParameter("@ngayChungTu", searchCondition.NgayChungTu),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh),
                    new SqlParameter("@Loai",searchCondition.LoaiChungTu),
                };
                string executeSql = "EXECUTE dbo.sp_rpt_dtc_dieuchinh_chitiet_donvi @NamLamViec,@IdDonVi, @IDLoaiChi,@SLNS,@ngayChungTu,@Dvt,@Loai";
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>(executeSql, parameters).ToList();

            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@IDLoaiChi", searchCondition.ILoaiDanhMucChi),
                    new SqlParameter("@SLNS", searchCondition.LNS),
                    new SqlParameter("@ngayChungTu", searchCondition.NgayChungTu),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh),
                    new SqlParameter("@Loai",searchCondition.ILoaiTongHop),
                    new SqlParameter("@MaLoaiChi",searchCondition.MaLoaiChi),
                };
                string executeSql = "EXECUTE dbo.sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone @NamLamViec,@IdDonVi, @IDLoaiChi,@SLNS,@ngayChungTu,@Dvt,@Loai,@MaLoaiChi";
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>(executeSql, parameters).ToList();

            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@IDLoaiChi", searchCondition.ILoaiDanhMucChi),
                    new SqlParameter("@SLNS", searchCondition.LNS),
                    new SqlParameter("@ngayChungTu", searchCondition.NgayChungTu),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi @NamLamViec,@IdDonVi, @IDLoaiChi,@SLNS,@ngayChungTu,@Dvt";
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>(executeSql, parameters).ToList();

            }
        }

        public List<BhDtcDcdToanChiChiTietQuery> ExportDataChiTiet(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@ChungTuId", searchCondition.DtcDcdToanChiId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", searchCondition.NamLamViec);
                SqlParameter sMaLoaiChiTuParam = new SqlParameter("@MaLoaiChi", searchCondition.MaLoaiChi);
                SqlParameter sMaDonViParam = new SqlParameter("@MaDonVi", searchCondition.IdDonVi);
                SqlParameter sLoaiViParam = new SqlParameter("@Loai", searchCondition.LoaiChungTu);

                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>("EXECUTE dbo.sp_bhxh_dt_export_dieuchinh_chi_tiet @ChungTuId, @LNS,@NamLamViec,@MaLoaiChi,@MaDonVi,@Loai",
                    idChungTuParam, sLNSParam, namLamViecParam, sMaLoaiChiTuParam, sMaDonViParam, sLoaiViParam).ToList();
            }
        }
        public List<BhDmMucLucNganSach> GetFineBHMLNSDuToanDieuChinhForLNS(int yearOfWork, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_dt_muclucngansach_theodieuchinh @namLamViec, @SLNS",
                    namLamViecParam, sLNSParam).ToList();
            }
        }

        public List<BhDtcDcdToanChiChiTietQuery> GetAdjustData(int namLamViec, string vouchers, string sLNS, string sMaLoaiChi, string sID_MaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter vouchersParam = new SqlParameter("@SoChungTu", vouchers);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter iIDDonViTuParam = new SqlParameter("@IIDonVi", sID_MaDonVi);
                SqlParameter maLoaiChiParam = new SqlParameter("@IDLoaiChi", sMaLoaiChi);
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>("EXECUTE dbo.sp_bh_get_data_dtdc_clone @NamLamViec, @SoChungTu,@SLNS,@IDLoaiChi,@IIDonVi",
                    namLamViecParam, vouchersParam, sLNSParam, maLoaiChiParam, iIDDonViTuParam).ToList();
            }
        }



        public List<BhDtcDcdToanChiChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int? namLamViec, DateTime dNgayChungTu, int iLoaiTongHop)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Empty;
                if (iLoaiTongHop == 2)
                    sql = "EXECUTE dbo.sp_dt_dieuchinh_getdieuchinhthu_BHYTQN_DonViCha @NamLamViec, @IdDonVi,@DNgayChungTu";
                else sql = "EXECUTE dbo.sp_dt_dieuchinh_getdieuchinhthu_BHYTQN @NamLamViec, @IdDonVi,@DNgayChungTu";
                var parameters = new[] {
                    new SqlParameter("@IdDonVi", iID_MaDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                       new SqlParameter("@DNgayChungTu", dNgayChungTu)

                };
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetSettlementData(int namLamViec, string idDonVi, int quy, string sLns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", idDonVi);
                SqlParameter quyParam = new SqlParameter("@Quy", quy);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLns);
                return ctx.FromSqlRaw<BhDtcDcdToanChiChiTietQuery>("EXECUTE dbo.sp_bh_dctc_get_data_quyet_toan @NamLamViec, @MaDonVi, @Quy, @SLNS"
                    , namLamViecParam, maDonViParam, quyParam, sLNSParam).ToList();
            }
        }
    }
}
