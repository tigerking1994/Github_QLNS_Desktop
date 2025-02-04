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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDtCtctKPQLRepostiory : Repository<BhDtCtctKPQL>, IBhDtCtctKPQLRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDtCtctKPQLRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDtCtctKPQLQuery> FindIndex(int iNamChungTu, Guid? iDChungTuChiTiet, Guid? iDChungTu, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var NsMucLucs = GetListMucLucBhxhByLoaiChungtu(iNamChungTu);
                NsMucLucs = NsMucLucs.Where(x => x.SLNS != LNSValue.LNS_9).ToList();
                List<BhDtCtctKPQLQuery> lstData = new List<BhDtCtctKPQLQuery>();
                lstData = FindDuToanChiTietKPQL(iDChungTu, sMaDonVi);
                var result = from nsMucLuc in NsMucLucs
                             join Data in lstData on nsMucLuc.SXauNoiMa equals Data.SXauNoiMa into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa

                             select new BhDtCtctKPQLQuery
                             {
                                 ID = sub == null ? Guid.Empty : sub.ID,
                                 IIDChungTu = sub?.IIDChungTu,
                                 IIDChungTuChiTiet = sub?.IIDChungTuChiTiet,
                                 SXauNoiMa = nsMucLuc?.SXauNoiMa,
                                 SNoiDung = nsMucLuc.SMoTa,
                                 SM = nsMucLuc?.SM,
                                 STM = nsMucLuc?.STM,
                                 STMM = nsMucLuc?.STTM,
                                 SNG = nsMucLuc?.SNG,
                                 IID_MLNS = nsMucLuc?.IIDMLNS,
                                 IID_MLNS_Cha = nsMucLuc?.IIDMLNSCha,
                                 FSoTien = sub?.FSoTien,
                                 INamLamViec = iNamChungTu,
                                 IIDMaDonVi = sMaDonVi,
                                 BHangCha = nsMucLuc.BHangCha,
                                 SNguoiTao = sub?.SNguoiTao,
                                 SNguoiSua = sub?.SNguoiSua,
                                 DNgaySua = sub?.DNgaySua,
                                 DNgayTao = sub?.DNgayTao,
                             };
                return result;
            }
        }

        public List<BhDtCtctKPQLQuery> FindDuToanChiTietKPQL(Guid? iDChungTu, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iDChungTuParam = new SqlParameter("@iDChungTu", iDChungTu);
                SqlParameter sMaDonViPara = new SqlParameter("@IIDDonVi", sMaDonVi);
                return ctx.FromSqlRaw<BhDtCtctKPQLQuery>("EXECUTE dbo.sp_bhxh_nhandutoanchitiet_kpql @iDChungTu,@IIDDonVi",
                    iDChungTuParam, sMaDonViPara).ToList();
            }
        }

        public List<BhDtCtctKPQLQuery> FindDuToanTrenGiaoKPQL(Guid? iDChungTu, string sMaDonVi, int NamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iDChungTuParam = new SqlParameter("@iDChungTu", iDChungTu);
                SqlParameter sMaDonViPara = new SqlParameter("@IIDDonVi", sMaDonVi);
                SqlParameter sNamLamViecPara = new SqlParameter("@NamLamViec", NamLamViec);
                return ctx.FromSqlRaw<BhDtCtctKPQLQuery>("EXECUTE dbo.sp_bhxh_finddutoan_trengiaochitiet_kpql @iDChungTu,@IIDDonVi,@NamLamViec",
                    iDChungTuParam, sMaDonViPara, sNamLamViecPara).ToList();
            }
        }
        public List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var loaiChungTuNSSD = int.Parse(VoucherType.NSSD_Key);
                var loaiChungTuNSBD = int.Parse(VoucherType.NSBD_Key);
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                List<BhDmMucLucNganSach> NsMucLucsChild = new List<BhDmMucLucNganSach>();

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => x.SXauNoiMa.StartsWith("9010011")).ToList();

                List<BhDmMucLucNganSach> nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlskt = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var test = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlskt.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).ToList();
                return nsMucLucs.Where(x => x.ITrangThai == iTrangThai).ToList();
            }
        }

        public List<BhDtCtctKPQLQuery> FindPhanBoDuToanTrenGiaoKPQL(Guid id, string sMaDonVi, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iDChungTuParam = new SqlParameter("@iDChungTu", id);
                SqlParameter sMaDonViPara = new SqlParameter("@IIDDonVi", sMaDonVi);
                SqlParameter sNamLamViecPara = new SqlParameter("@NamLamViec", yearOfWork);
                return ctx.FromSqlRaw<BhDtCtctKPQLQuery>("EXECUTE dbo.sp_bhxh_finphanbodutoan_chitiet_kpql @iDChungTu,@IIDDonVi,@NamLamViec",
                    iDChungTuParam, sMaDonViPara, sNamLamViecPara).ToList();
            }
        }

        public IEnumerable<BhDtCtctKPQL> FindByCondition(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.BhDtCtctKPQLs.Where(x => x.IIDChungTu == id).ToList();
                return lst;
            }
        }

        public IEnumerable<ReportBhDtCtctKPQLQuery> ExportBaoCaoChiTietDonViKQPL(string maDonVi, int yearOfWork, Guid id, int donViTinh, bool IsMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iDChungTuParam = new SqlParameter("@iDChungTu", id);
                SqlParameter sMaDonViPara = new SqlParameter("@IIDDonVi", maDonVi);
                SqlParameter sNamLamViecPara = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter sDonViTinhPara = new SqlParameter("@DonViTinh", donViTinh);
                SqlParameter isMillionRoundPara = new SqlParameter("@IsMillionRound", IsMillionRound);
                return ctx.FromSqlRaw<ReportBhDtCtctKPQLQuery>("EXECUTE dbo.sp_rpt_bhxh_tonghop_noidung_kpql @iDChungTu,@IIDDonVi,@NamLamViec,@DonViTinh,@IsMillionRound",
                    iDChungTuParam, sMaDonViPara, sNamLamViecPara, sDonViTinhPara, isMillionRoundPara).ToList();
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCKPQL(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);

                return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_dtc_get_so_quyet_dinh_kpql @NamLamViec", yearOfWorkParam).ToList();

            }
        }
    }
}
