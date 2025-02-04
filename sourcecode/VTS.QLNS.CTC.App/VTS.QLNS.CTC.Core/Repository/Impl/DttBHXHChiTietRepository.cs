using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DttBHXHChiTietRepository : Repository<BhDttBHXHChiTiet>, IDttBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public DttBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhDttBHXHChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttBHXHChiTiets.Find(id);
            }
        }

        public IEnumerable<BhDttBHXHChiTiet> FindDttBHXHChiTietByIdBhxh(DttBHXHChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                Guid dTTId = searchCondition.DtttBhxhId;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var mucLucNS = GetListBhMucLucNs(namLamViec, searchCondition.LstLns);
                var duToanThuChiTiet = FindDuToanThhuChiTiet(dTTId, namLamViec).ToList();

                var result = from nsMucLuc in mucLucNS
                             join khtBhxhChiTiet in duToanThuChiTiet on nsMucLuc.IIDMLNS equals khtBhxhChiTiet.IIdMlns
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa
                             select new BhDttBHXHChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 DttBHXHId = dTTId,
                                 IIdMlns = nsMucLuc.IIDMLNS,
                                 IIdMlnsCha = nsMucLuc.IIDMLNSCha,
                                 STenBhMLNS = nsMucLuc.SMoTa,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = nsMucLuc.BHangCha,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 FThuBHXHNguoiLaoDong = sub?.FThuBHXHNguoiLaoDong ?? 0,
                                 FThuBHXHNguoiSuDungLaoDong = sub?.FThuBHXHNguoiSuDungLaoDong ?? 0,
                                 FThuBHYTNguoiLaoDong = sub?.FThuBHYTNguoiLaoDong ?? 0,
                                 FThuBHYTNguoiSuDungLaoDong = sub?.FThuBHYTNguoiSuDungLaoDong ?? 0,
                                 FThuBHTNNguoiLaoDong = sub?.FThuBHTNNguoiLaoDong ?? 0,
                                 FThuBHTNNguoiSuDungLaoDong = sub?.FThuBHTNNguoiSuDungLaoDong ?? 0,
                                 FTongThuBHXH = sub?.FTongThuBHXH ?? 0,
                                 FTongThuBHYT = sub?.FTongThuBHYT ?? 0,
                                 FTongThuBHTN = sub?.FTongThuBHTN ?? 0,
                                 FTongCong = sub?.FTongCong ?? 0,
                                 SLns = nsMucLuc.SLNS,
                                 SL = nsMucLuc.SL,
                                 SK = nsMucLuc.SK,
                                 SM = nsMucLuc.SM,
                                 STm = nsMucLuc.STM,
                                 STtm = nsMucLuc.STTM,
                                 SNg = nsMucLuc.SNG,
                                 STng = nsMucLuc.STNG,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? searchCondition.IdDonVi,
                                 INamLamViec = searchCondition.NamLamViec
                             };

                return result;
            }
        }
        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, List<string> lstLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                Func<BhDmMucLucNganSach, bool> defaultKhtBhxhMucLucFilter = x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE && lstLNS.Contains(x.SLNS);
                var mucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultKhtBhxhMucLucFilter).ToList();
                var nsMucLucs = new List<BhDmMucLucNganSach>();
                if (mucLucsChild.Count > 0)
                {
                    var listIdMlKht = mucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = mucLucsChild;
                    while (true)
                    {
                        var listIdParent = mucLucsChild.Where(x => !listIdMlKht.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlKht.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                nsMucLucs.RemoveRange(0, 2);
                return nsMucLucs;
            }
        }
        public IEnumerable<BhDttBHXHChiTiet> FindDuToanThhuChiTiet(Guid khtBHXHId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter khtBHXHIdParam = new SqlParameter("@KhtBHXHId", khtBHXHId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.BhDttBHXHChiTiets.FromSql("EXECUTE dbo.sp_dtt_bhxh_chi_tiet @KhtBHXHId, @NamLamViec",
                    khtBHXHIdParam, namLamViecParam).ToList();
            }
        }

        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttBHXHChiTiets.Any(t => t.DttBHXHId.Equals(bhxhId));
            }
        }

        public IEnumerable<BhDttBHXHChiTiet> FindByParentId(DttBHXHChiTietCriteria searchCondition)
        {
            Guid dttBHXHId = searchCondition.DtttBhxhId;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttBHXHChiTiets.Where(x => x.DttBHXHId == dttBHXHId).ToList();
            }
        }

        public IEnumerable<BhDttBHXHChiTiet> FindByIdDTT(Guid Id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttBHXHChiTiets.Where(x => x.DttBHXHId == Id).ToList();
            }
        }
    }
}
