using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDtTmBHYTTNChiTietRepository : Repository<BhDtTmBHYTTNChiTiet>, IBhDtTmBHYTTNChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDtTmBHYTTNChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool ExistBHXHChiTiet(Guid bhytId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtTmBHYTTNChiTiets.Any(t => t.IID_DTTM_BHYT_ThanNhan.Equals(bhytId));
            }
        }

        public BhDtTmBHYTTNChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtTmBHYTTNChiTiets.Find(id);
            }
        }

        public IEnumerable<BhDtTmBHYTTNChiTiet> FindByParentId(DtTmBHYTTNChiTietCriteria searchCondition)
        {
            Guid dttBHXHId = searchCondition.DttmBhytId;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtTmBHYTTNChiTiets.Where(x => x.IID_DTTM_BHYT_ThanNhan == dttBHXHId).ToList();
            }
        }

        public IEnumerable<BhDtTmBHYTTNChiTiet> FindDttmBHYTChiTietById(DtTmBHYTTNChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                Guid dtTmBHYTId = searchCondition.DttmBhytId;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var mucLucNS = GetListBhMucLucNs(namLamViec, searchCondition.LstLns);
                var duToanThuChiTiet = FindDuToanThuChiTiet(dtTmBHYTId, namLamViec).ToList();

                var result = from nsMucLuc in mucLucNS
                             join dtTmBhYTChiTiet in duToanThuChiTiet on nsMucLuc.IIDMLNS equals dtTmBhYTChiTiet.IID_MLNS
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa
                             select new BhDtTmBHYTTNChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_DTTM_BHYT_ThanNhan = dtTmBHYTId,
                                 IID_MLNS = nsMucLuc.IIDMLNS,
                                 SNoiDung = nsMucLuc.SMoTa,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = nsMucLuc.BHangCha,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 FDuToan = sub?.FDuToan ?? 0,
                                 SLNS = nsMucLuc.SLNS,
                                 SL = nsMucLuc.SL,
                                 SK = nsMucLuc.SK,
                                 IIdMaDonVi = searchCondition.IdDonVi,
                                 SGhiChu = sub == null ? string.Empty : sub.SGhiChu,
                                 INamLamViec = namLamViec
                             };

                return result.ToList();
            }
        }

        private IEnumerable<BhDtTmBHYTTNChiTiet> FindDuToanThuChiTiet(Guid dtTmBHYTId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter dtTmBHYTIdParam = new SqlParameter("@dttmBHYTId", dtTmBHYTId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.Set<BhDtTmBHYTTNChiTiet>().FromSql("EXECUTE dbo.sp_dttm_bhyt_tn_chi_tiet @dttmBHYTId, @NamLamViec",
                  dtTmBHYTIdParam, namLamViecParam).ToList();
            }
        }

        private List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, List<string> lstLns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                Func<BhDmMucLucNganSach, bool> defaultKhtBhxhMucLucFilter = x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE && lstLns.Contains(x.SLNS);
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
    }
}
