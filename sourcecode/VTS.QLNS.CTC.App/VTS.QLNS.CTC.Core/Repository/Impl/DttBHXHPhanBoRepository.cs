using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DttBHXHPhanBoRepository : Repository<BhDtPhanBoChungTu>, IDttBHXHPhanBoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DttBHXHPhanBoRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDttChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter dateParam = new SqlParameter("@Date", condition.Date);
                SqlParameter loaiDTParam = new SqlParameter("@LoaiDuToan", condition.EstimationType);
                return ctx.FromSqlRaw<BhDttChungTuDotNhanQuery>("EXECUTE dbo.sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo @YearOfWork, @Date, @LoaiDuToan",
                    yearOfWorkParam, dateParam, loaiDTParam).ToList();

            }
        }

        public Dictionary<string, string> FindAllDict(int namLamViec, int? loaiDuToan)
        {
            if (loaiDuToan == (int)EstimateTypeNum.ADJUSTED)
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.BhDtPhanBoChungTus.Where(y => y.INamLamViec == namLamViec).ToDictionary(x => x.Id.ToString(), x => x.SSoChungTu);
                }
            }
            else
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.BhDttBHXHs.Where(y => y.INamLamViec == namLamViec).ToDictionary(x => x.Id.ToString(), x => x.SSoChungTu);
                }
            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByCondition(DuToanThuChungTuCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter userNameParam = new SqlParameter("@UserName", condition.UserName);
                return ctx.BhDtPhanBoChungTus.FromSql("EXECUTE dbo.sp_bhxh_dtt_phan_bo_chungtu @YearOfWork, @UserName",
                    yearOfWorkParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDttChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                if (isCreate)
                {
                    SqlParameter voucherTypeParam = new SqlParameter("@VoucherType", condition.VoucherType);
                    SqlParameter dateParam = new SqlParameter("@Date", condition.Date);
                    return ctx.FromSqlRaw<BhDttChungTuDotNhanQuery>("EXECUTE dbo.sp_dtt_bhxh_dotnhan_phanbo @YearOfWork, @VoucherType, @Date",
                        yearOfWorkParam, voucherTypeParam, dateParam).ToList();
                }
                else
                {
                    SqlParameter idNhanPhanBosParam = new SqlParameter("@IdNhanPhanBos", condition.IdNhanPhanBos);
                    return ctx.FromSqlRaw<BhDttChungTuDotNhanQuery>("EXECUTE dbo.sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo @YearOfWork, @IdNhanPhanBos",
                        yearOfWorkParam,idNhanPhanBosParam).ToList();
                }
            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoParam = new SqlParameter("@IdPhanBo", idPhanBo.ToString());
                return ctx.BhDtPhanBoChungTus.FromSql("EXECUTE sp_bh_dtt_danhsach_dotnhan @IdPhanBo", idPhanBoParam).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<BhDtPhanBoChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.BhDtPhanBoChungTus.Where(predicate).Max(x => x.ISoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhDtPhanBoChungTu entity = ctx.BhDtPhanBoChungTus.Find(id);
                if (entity != null) entity.BKhoa = lockStatus;
                return Update(entity);
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinh(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_thtc_get_so_quyet_dinh @NamLamViec", yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTT(int year, bool isInTheoChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                if (isInTheoChungTu)
                {
                    return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_get_so_quyet_dinh_dtt_theo_ct @NamLamViec", yearOfWorkParam).ToList();
                }
                else
                {
                    return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_get_so_quyet_dinh_dtt @NamLamViec", yearOfWorkParam).ToList();
                }
            }
        }


        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTBHXHBHYTBHTN(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
               
                    return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn @NamLamViec", yearOfWorkParam).ToList();
               
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCBHXHBHYTBHTN(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);

                return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn @NamLamViec", yearOfWorkParam).ToList();

            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindBySoChungTu(string soChungTu, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTus.Where(y => y.INamLamViec == nam && y.SSoChungTu == soChungTu).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTus.Where(y => y.INamLamViec == nam && y.SSoQuyetDinh == soQuyetDinh).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", nam);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var ngayQDParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                return ctx.FromSqlRaw<BhDtPhanBoChungTuQuery>("EXECUTE dbo.sp_bhxh_get_chung_tu_duoc_xem_luy_ke @NamLamViec, @SoQuyetDinh, @NgayQuyetDinh",
                    yearOfWorkParam, soQuyetDinhParam, ngayQDParam).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKeDttmBHYT(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", nam);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var ngayQDParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                return ctx.FromSqlRaw<BhPbdttmBHYTQuery>("EXECUTE dbo.sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt @NamLamViec, @SoQuyetDinh, @NgayQuyetDinh",
                    yearOfWorkParam, soQuyetDinhParam, ngayQDParam).ToList();
            }
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                var unitParam = new SqlParameter("@DonVis", donVis);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var donViTinhParam = new SqlParameter("@DVT", donViTinh);
                var ngayQDParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                var isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<BhDuToanTongHopThuChiQuery>("EXECUTE dbo.sp_rpt_bhxh_tong_hop_thu_chi_thu @NamLamViec, @DonVis, @SoQuyetDinh, @DVT, @NgayQuyetDinh, @IsMillionRound", 
                    yearOfWorkParam, unitParam, soQuyetDinhParam, donViTinhParam, ngayQDParam, isMillionRoundParam).ToList();
            }
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                var unitParam = new SqlParameter("@DonVis", donVis);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var donViTinhParam = new SqlParameter("@DVT", donViTinh);
                var ngayQDParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                var isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<BhDuToanTongHopThuChiQuery>("EXECUTE dbo.sp_rpt_bhxh_tong_hop_thu_chi_chi @NamLamViec, @DonVis, @SoQuyetDinh, @DVT, @NgayQuyetDinh, @IsMillionRound",
                    yearOfWorkParam, unitParam, soQuyetDinhParam, donViTinhParam, ngayQDParam, isMillionRoundParam).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTus.Where(y => y.INamLamViec == nam && y.DNgayQuyetDinh < ngayQuyetDinh).ToList();
            }
        }
        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                var unitParam = new SqlParameter("@DonVis", donVis);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                var isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<BhDuToanTongHopThuChiQuery>("EXECUTE dbo.sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu @NamLamViec, @DonVis, @Dvt, @SoQuyetDinh, @IsMillionRound",
                    yearOfWorkParam, unitParam, donViTinhParam, soQuyetDinhParam, isMillionRoundParam).ToList();
            }
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                var unitParam = new SqlParameter("@DonVis", donVis);
                var donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<BhDuToanTongHopThuChiQuery>("EXECUTE dbo.sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi @NamLamViec, @DonVis, @Dvt, @SoQuyetDinh, @IsMillionRound",
                    yearOfWorkParam, unitParam, donViTinhParam, soQuyetDinhParam, isMillionRoundParam).ToList();
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> FindDotDuToan()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //SqlParameter param = new SqlParameter("@Id", Id);
                return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bh_getalldotdutoanbhxh").ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByIdNhanDuToan(string idNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTus.Where(n => n.IIdDotNhan.Contains(idNhanDuToan)).ToList();
            }
        }

        public List<string> GetDonViDttDttnDtc(int namLamViec, string soQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", namLamViec);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                return ctx.FromSqlRaw<string>("EXECUTE dbo.sp_bh_dtt_get_donvi_dtt_dttm_dtc @NamLamViec, @SoQuyetDinh",
                    yearOfWorkParam, soQuyetDinhParam).ToList();
            }
        }
    }
}
