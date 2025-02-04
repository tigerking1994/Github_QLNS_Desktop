using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdtcBHXHRepository : Repository<BhPbdtcBHXH>, IPbdtcBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdtcBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhPbdtcBHXH> FindByCondition(Expression<Func<BhPbdtcBHXH, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdtcBHXHs.Where(predicate).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhPbdtcBHXHs.Where(x => x.INamChungTu == iNamLamViec).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var sSoChungTuMax = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu;
                    var indexString = sSoChungTuMax.Substring(4, sSoChungTuMax.Length - 4);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhPbdtcBHXH entity = ctx.BhPbdtcBHXHs.Find(id);
                if (entity != null) entity.BIsKhoa = lockStatus;
                return Update(entity);
            }
        }

        public IEnumerable<BhPbdtcBHXhQuery> GetDanhSachPhanBoDuToanChi(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@YearOfWork", iNamLamViec);
                return ctx.FromSqlRaw<BhPbdtcBHXhQuery>("EXECUTE sp_bhxh_phanbodutoanchi_index_clone @YearOfWork", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhPbdtcBHXH> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoParam = new SqlParameter("@IdPhanBo", idPhanBo.ToString());
                return ctx.BhPbdtcBHXHs.FromSql("EXECUTE sp_bh_dt_danhsach_dotnhan @IdPhanBo", idPhanBoParam).ToList();
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int namLamViec, Guid? IDLoaiChi, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh, int dotNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter IDLoaiChiParam = new SqlParameter("@IDLoaiChi", IDLoaiChi);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaichi", sMaLoaiChi);
                SqlParameter sSoQuyetDinhParam = new SqlParameter("@SoQuyetDinh", sSoQuyetDinh);
                SqlParameter sNgayQuyetDinhParam = new SqlParameter("@DNgayQuyetDinh", sNgayQuyetDinh);
                SqlParameter dotNhanPara = new SqlParameter("@DotNhan", dotNhan);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_phanbo_get_donvi @NamLamViec, @IDLoaiChi, @MaLoaichi, @SoQuyetDinh,@DNgayQuyetDinh,@DotNhan", namLamViecParam, IDLoaiChiParam, sMaLoaiChiParam, sSoQuyetDinhParam, sNgayQuyetDinhParam, dotNhan).ToList();
            }
        }
        public IEnumerable<BhPbdtcBHXH> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdtcBHXHs.Where(y => y.INamChungTu == nam && y.SSoQuyetDinh == soQuyetDinh).ToList();
            }
        }

        public IEnumerable<BhPbdtcBHXH> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdtcBHXHs.Where(y => y.INamChungTu == nam && y.DNgayQuyetDinh < ngayQuyetDinh).ToList();
            }
        }

        public List<DonVi> FindByDonViForInLuyKeNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh, int dotNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter IDLoaiChiParam = new SqlParameter("@IDLoaiChi", id);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaichi", sMaLoaiChi);
                SqlParameter sSoQuyetDinhParam = new SqlParameter("@SoQuyetDinh", sSoQuyetDinh);
                SqlParameter sNgayQuyetDinhParam = new SqlParameter("@SNgayQuyetDinh", sNgayQuyetDinh);
                SqlParameter dotNhanPara = new SqlParameter("@DotNhan", dotNhan);

                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_phanbochi_inluykechecked_get_donvi @NamLamViec, @IDLoaiChi, @MaLoaichi,@SoQuyetDinh, @SNgayQuyetDinh,@DotNhan", namLamViecParam, IDLoaiChiParam, sMaLoaiChiParam, sSoQuyetDinhParam, sNgayQuyetDinhParam, dotNhanPara).ToList();

            }
        }

        public List<DonVi> FindByDonViForNamLamViecNormal(int yearOfWork, Guid idLoaiChi, string sMaLoaiChi, string idChungTu, int dotNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter IDLoaiChiParam = new SqlParameter("@IDLoaiChi", idLoaiChi);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaichi", sMaLoaiChi);
                SqlParameter sIdChungTu = new SqlParameter("@IdChungTu", idChungTu);
                if (dotNhan == 0)
                {
                    
                    return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_phanbo_get_donvi @NamLamViec, @IDLoaiChi, @MaLoaichi, @IdChungTu"
                                                , namLamViecParam, IDLoaiChiParam, sMaLoaiChiParam, sIdChungTu).ToList();
                }
                else
                {
                    SqlParameter dotNhanPara = new SqlParameter("@DotNhan", dotNhan);
                    return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_phanbo_get_donvi_bandau_or_bosung @NamLamViec, @IDLoaiChi, @MaLoaichi, @IdChungTu,@DotNhan",
                                                namLamViecParam, IDLoaiChiParam, sMaLoaiChiParam, sIdChungTu, dotNhanPara).ToList();
                }
            }
        }

        public List<DonVi> FindByDonViForInDotNgayNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh, int dotNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter IDLoaiChiParam = new SqlParameter("@IDLoaiChi", id);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaichi", sMaLoaiChi);
                SqlParameter sSoQuyetDinhParam = new SqlParameter("@SoQuyetDinh", sSoQuyetDinh);
                SqlParameter sNgayQuyetDinhParam = new SqlParameter("@SNgayQuyetDinh", sNgayQuyetDinh);
                SqlParameter dotNhanPara = new SqlParameter("@DotNhan", dotNhan);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_phanbochi_intheodotngaychecked_get_donvi @NamLamViec, @IDLoaiChi, @MaLoaichi,@SoQuyetDinh, @SNgayQuyetDinh,@DotNhan", namLamViecParam, IDLoaiChiParam, sMaLoaiChiParam, sSoQuyetDinhParam, sNgayQuyetDinhParam, dotNhanPara).ToList();

            }
        }

        public IEnumerable<BhPbdtcBHXH> FindByConditionLoaiChi(int yearOfWork, int dotNhan, string sMaLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter iDotNhatParam = new SqlParameter("@DotNhat", dotNhan);
                SqlParameter sMaLaiChiParam = new SqlParameter("@MaLoaiChi", sMaLoaiChi);
                return ctx.FromSqlRaw<BhPbdtcBHXH>("EXECUTE sp_bhxh_getdata_chungtu_theoloaichi @YearOfWork,@DotNhat,@MaLoaiChi", iNamLamViecParam, iDotNhatParam, sMaLaiChiParam).ToList();
            }
        }
    }
}
